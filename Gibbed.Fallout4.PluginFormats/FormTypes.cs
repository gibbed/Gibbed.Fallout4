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

namespace Gibbed.Fallout4.PluginFormats
{
    internal static class FormTypes
    {
        private static readonly FormType[] _FormTypes;

        static FormTypes()
        {
            _FormTypes = new[]
            {
                FormType.NONE,
                FormType.TES4,
                FormType.GRUP,
                FormType.GMST,
                FormType.KYWD,
                FormType.LCRT,
                FormType.AACT,
                FormType.TRNS,
                FormType.CMPO,
                FormType.TXST,
                FormType.MICN,
                FormType.GLOB,
                FormType.DMGT,
                FormType.CLAS,
                FormType.FACT,
                FormType.HDPT,
                FormType.EYES,
                FormType.RACE,
                FormType.SOUN,
                FormType.ASPC,
                FormType.SKIL,
                FormType.MGEF,
                FormType.SCPT,
                FormType.LTEX,
                FormType.ENCH,
                FormType.SPEL,
                FormType.SCRL,
                FormType.ACTI,
                FormType.TACT,
                FormType.ARMO,
                FormType.BOOK,
                FormType.CONT,
                FormType.DOOR,
                FormType.INGR,
                FormType.LIGH,
                FormType.MISC,
                FormType.STAT,
                FormType.SCOL,
                FormType.MSTT,
                FormType.GRAS,
                FormType.TREE,
                FormType.FLOR,
                FormType.FURN,
                FormType.WEAP,
                FormType.AMMO,
                FormType.NPC_,
                FormType.LVLN,
                FormType.KEYM,
                FormType.ALCH,
                FormType.IDLM,
                FormType.NOTE,
                FormType.PROJ,
                FormType.HAZD,
                FormType.BNDS,
                FormType.SLGM,
                FormType.TERM,
                FormType.LVLI,
                FormType.WTHR,
                FormType.CLMT,
                FormType.SPGD,
                FormType.RFCT,
                FormType.REGN,
                FormType.NAVI,
                FormType.CELL,
                FormType.REFR,
                FormType.ACHR,
                FormType.PMIS,
                FormType.PARW,
                FormType.PGRE,
                FormType.PBEA,
                FormType.PFLA,
                FormType.PCON,
                FormType.PBAR,
                FormType.PHZD,
                FormType.WRLD,
                FormType.LAND,
                FormType.NAVM,
                FormType.TLOD,
                FormType.DIAL,
                FormType.INFO,
                FormType.QUST,
                FormType.IDLE,
                FormType.PACK,
                FormType.CSTY,
                FormType.LSCR,
                FormType.LVSP,
                FormType.ANIO,
                FormType.WATR,
                FormType.EFSH,
                FormType.TOFT,
                FormType.EXPL,
                FormType.DEBR,
                FormType.IMGS,
                FormType.IMAD,
                FormType.FLST,
                FormType.PERK,
                FormType.BPTD,
                FormType.ADDN,
                FormType.AVIF,
                FormType.CAMS,
                FormType.CPTH,
                FormType.VTYP,
                FormType.MATT,
                FormType.IPCT,
                FormType.IPDS,
                FormType.ARMA,
                FormType.ECZN,
                FormType.LCTN,
                FormType.MESG,
                FormType.RGDL,
                FormType.DOBJ,
                FormType.DFOB,
                FormType.LGTM,
                FormType.MUSC,
                FormType.FSTP,
                FormType.FSTS,
                FormType.SMBN,
                FormType.SMQN,
                FormType.SMEN,
                FormType.DLBR,
                FormType.MUST,
                FormType.DLVW,
                FormType.WOOP,
                FormType.SHOU,
                FormType.EQUP,
                FormType.RELA,
                FormType.SCEN,
                FormType.ASTP,
                FormType.OTFT,
                FormType.ARTO,
                FormType.MATO,
                FormType.MOVT,
                FormType.SNDR,
                FormType.DUAL,
                FormType.SNCT,
                FormType.SOPM,
                FormType.COLL,
                FormType.CLFM,
                FormType.REVB,
                FormType.PKIN,
                FormType.RFGP,
                FormType.AMDL,
                FormType.LAYR,
                FormType.COBJ,
                FormType.OMOD,
                FormType.MSWP,
                FormType.ZOOM,
                FormType.INNR,
                FormType.KSSM,
                FormType.AECH,
                FormType.SCCO,
                FormType.AORU,
                FormType.SCSN,
                FormType.STAG,
                FormType.NOCM,
                FormType.LENS,
                FormType.LSPR,
                FormType.GDRY,
                FormType.OVIS,
            };
        }

        public static FormType GetTypeFromIndex(int index)
        {
            if (index < 0 || index >= _FormTypes.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return _FormTypes[index];
        }
    }
}
