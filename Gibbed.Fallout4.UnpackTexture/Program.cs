/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Gibbed.Fallout4.FileFormats;
using Gibbed.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NDesk.Options;

namespace Gibbed.Fallout4.UnpackTexture
{
    public class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void Main(string[] args)
        {
            bool showHelp = false;
            string filterPattern = null;
            bool overwriteFiles = false;
            bool verbose = false;

            var options = new OptionSet()
            {
                { "o|overwrite", "overwrite existing files", v => overwriteFiles = v != null },
                { "f|filter=", "only extract files using pattern", v => filterPattern = v },
                { "v|verbose", "be verbose", v => verbose = v != null },
                { "h|help", "show this message and exit", v => showHelp = v != null },
            };

            List<string> extras;

            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || extras.Count > 2 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_ba2 [output_dir]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            string inputPath = Path.GetFullPath(extras[0]);
            string outputPath = extras.Count > 1
                                    ? Path.GetFullPath(extras[1])
                                    : Path.ChangeExtension(inputPath, null) + "_unpack";

            Regex filter = null;
            if (string.IsNullOrEmpty(filterPattern) == false)
            {
                filter = new Regex(filterPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            using (var input = File.OpenRead(inputPath))
            {
                Unpack(input, filter, outputPath, overwriteFiles, verbose);
            }
        }

        public static void Unpack(Stream input, Regex filter, string outputPath, bool overwriteFiles, bool verbose)
        {
            var archive = new TextureDX10ArchiveFile();
            archive.Deserialize(input);

            var entries = archive.Entries.ToArray();

            long current = 0;
            long total = entries.Length;
            var padding = total.ToString(CultureInfo.InvariantCulture).Length;

            foreach (var entry in entries)
            {
                current++;

                var entryName = entry.Name;
                if (filter != null && filter.IsMatch(entryName) == false)
                {
                    continue;
                }

                if (entryName.StartsWith("/") == true)
                {
                    entryName = entryName.Substring(1);
                }
                entryName = entryName.Replace('/', Path.DirectorySeparatorChar);

                var entryPath = Path.Combine(outputPath, entryName);
                if (overwriteFiles == false && File.Exists(entryPath) == true)
                {
                    continue;
                }

                if (verbose == true)
                {
                    Console.WriteLine(
                        "[{0}/{1}] {2}",
                        current.ToString(CultureInfo.InvariantCulture).PadLeft(padding),
                        total,
                        entryName);
                }

                // TODO(rick): validation of entry hashes (name & directory)

                var entryDirectory = Path.GetDirectoryName(entryPath);
                if (entryDirectory != null)
                {
                    Directory.CreateDirectory(entryDirectory);
                }

                using (var output = File.Create(entryPath))
                {
                    ConvertTexture(input, entry, output);
                }
            }
        }

        private static Squish.DDS.PixelFormat GetPixelFormat(TextureDX10ArchiveFile.Entry entry)
        {
            switch (entry.Format)
            {
                case 71: // DXGI_FORMAT_BC1_UNORM
                {
                    var pixelFormat = new Squish.DDS.PixelFormat();
                    pixelFormat.Initialise(Squish.DDS.FileFormat.DXT1);
                    return pixelFormat;
                }

                case 77: // DXGI_FORMAT_BC3_UNORM
                {
                    var pixelFormat = new Squish.DDS.PixelFormat();
                    pixelFormat.Initialise(Squish.DDS.FileFormat.DXT5);
                    return pixelFormat;
                }

                case 83: // DXGI_FORMAT_BC5_UNORM
                {
                    var pixelFormat = new Squish.DDS.PixelFormat();
                    pixelFormat.Size = pixelFormat.GetSize();
                    pixelFormat.FourCC = 0x30315844; // 'DX10'
                    return pixelFormat;
                }

                case 87: // DXGI_FORMAT_B8G8R8A8_UNORM
                {
                    var pixelFormat = new Squish.DDS.PixelFormat();
                    pixelFormat.Initialise(Squish.DDS.FileFormat.A8R8G8B8);
                    return pixelFormat;
                }
            }

            throw new NotSupportedException();
        }

        private static void ConvertTexture(Stream input, TextureDX10ArchiveFile.Entry entry, Stream output)
        {
            const Endian endian = Endian.Little;

            var header = new Squish.DDS.Header();
            header.Size = header.GetSize();
            header.Flags = Squish.DDS.HeaderFlags.Texture | Squish.DDS.HeaderFlags.Mipmap;
            header.Width = entry.Width;
            header.Height = entry.Height;
            header.PitchOrLinearSize = 0;
            header.Depth = 0;
            header.MipMapCount = entry.MipMapCount;
            header.PixelFormat = GetPixelFormat(entry);
            header.SurfaceFlags = 8;
            header.CubemapFlags = 0;

            output.WriteValueU32(0x20534444, endian);
            header.Serialize(output, endian);

            if (header.PixelFormat.FourCC == 0x30315844)
            {
                output.WriteValueU32(entry.Format, endian);
                output.WriteValueU32(2, endian);
                output.WriteValueU32(0, endian);
                output.WriteValueU32(1, endian);
                output.WriteValueU32(0, endian);
            }

            foreach (var mipMap in entry.MipMaps)
            {
                if (mipMap.DataCompressedSize == 0)
                {
                    input.Seek(mipMap.DataOffset, SeekOrigin.Begin);
                    output.WriteFromStream(input, mipMap.DataUncompressedSize);
                }
                else
                {
                    input.Seek(mipMap.DataOffset, SeekOrigin.Begin);
                    var zlib = new InflaterInputStream(input);
                    output.WriteFromStream(zlib, mipMap.DataUncompressedSize);
                }
            }
        }
    }
}
