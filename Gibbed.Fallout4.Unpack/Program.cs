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

namespace Gibbed.Fallout4.Unpack
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static void Main(string[] args)
        {
            bool showHelp = false;
            bool extractUnknowns = true;
            string filterPattern = null;
            bool overwriteFiles = false;
            bool verbose = false;
            string currentProject = null;

            var options = new OptionSet()
            {
                { "o|overwrite", "overwrite existing files", v => overwriteFiles = v != null },
                { "nu|no-unknowns", "don't extract unknown files", v => extractUnknowns = v == null },
                { "f|filter=", "only extract files using pattern", v => filterPattern = v },
                { "v|verbose", "be verbose", v => verbose = v != null },
                { "h|help", "show this message and exit", v => showHelp = v != null },
                { "p|project=", "override current project", v => currentProject = v },
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

            string tabPath = Path.GetFullPath(extras[0]);
            string outputPath = extras.Count > 1
                                    ? Path.GetFullPath(extras[1])
                                    : Path.ChangeExtension(tabPath, null) + "_unpack";

            Regex filter = null;
            if (string.IsNullOrEmpty(filterPattern) == false)
            {
                filter = new Regex(filterPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            var archive = new ArchiveFile();
            using (var input = File.OpenRead(tabPath))
            {
                archive.Deserialize(input);

                long current = 0;
                long total = archive.Entries.Count;
                var padding = total.ToString(CultureInfo.InvariantCulture).Length;

                foreach (var entry in archive.Entries)
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
                        if (entry.DataCompressedSize == 0)
                        {
                            input.Seek(entry.DataOffset, SeekOrigin.Begin);
                            output.WriteFromStream(input, entry.DataUncompressedSize);
                        }
                        else
                        {
                            input.Seek(entry.DataOffset, SeekOrigin.Begin);
                            var zlib = new InflaterInputStream(input);
                            output.WriteFromStream(zlib, entry.DataUncompressedSize);
                        }
                    }
                }
            }
        }
    }
}
