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

namespace Gibbed.Fallout4.PluginFormats
{
    public enum FormType : uint
    {
        // ReSharper disable InconsistentNaming
        NONE = 0x454E4F4E, // 0
        TES4 = 0x34534554, // 1
        GRUP = 0x50555247, // 2
        GMST = 0x54534D47, // 3
        KYWD = 0x4457594B, // 4
        LCRT = 0x5452434C, // 5
        AACT = 0x54434141, // 6
        TRNS = 0x534E5254, // 7
        CMPO = 0x4F504D43, // 8
        TXST = 0x54535854, // 9
        MICN = 0x4E43494D, // 10
        GLOB = 0x424F4C47, // 11
        DMGT = 0x54474D44, // 12
        CLAS = 0x53414C43, // 13
        FACT = 0x54434146, // 14
        HDPT = 0x54504448, // 15
        EYES = 0x53455945, // 16
        RACE = 0x45434152, // 17
        SOUN = 0x4E554F53, // 18
        ASPC = 0x43505341, // 19
        SKIL = 0x4C494B53, // 20
        MGEF = 0x4645474D, // 21
        SCPT = 0x54504353, // 22
        LTEX = 0x5845544C, // 23
        ENCH = 0x48434E45, // 24
        SPEL = 0x4C455053, // 25
        SCRL = 0x4C524353, // 26
        ACTI = 0x49544341, // 27
        TACT = 0x54434154, // 28
        ARMO = 0x4F4D5241, // 29
        BOOK = 0x4B4F4F42, // 30
        CONT = 0x544E4F43, // 31
        DOOR = 0x524F4F44, // 32
        INGR = 0x52474E49, // 33
        LIGH = 0x4847494C, // 34
        MISC = 0x4353494D, // 35
        STAT = 0x54415453, // 36
        SCOL = 0x4C4F4353, // 37
        MSTT = 0x5454534D, // 38
        GRAS = 0x53415247, // 39
        TREE = 0x45455254, // 40
        FLOR = 0x524F4C46, // 41
        FURN = 0x4E525546, // 42
        WEAP = 0x50414557, // 43
        AMMO = 0x4F4D4D41, // 44
        NPC_ = 0x5F43504E, // 45
        LVLN = 0x4E4C564C, // 46
        KEYM = 0x4D59454B, // 47
        ALCH = 0x48434C41, // 48
        IDLM = 0x4D4C4449, // 49
        NOTE = 0x45544F4E, // 50
        PROJ = 0x4A4F5250, // 51
        HAZD = 0x445A4148, // 52
        BNDS = 0x53444E42, // 53
        SLGM = 0x4D474C53, // 54
        TERM = 0x4D524554, // 55
        LVLI = 0x494C564C, // 56
        WTHR = 0x52485457, // 57
        CLMT = 0x544D4C43, // 58
        SPGD = 0x44475053, // 59
        RFCT = 0x54434652, // 60
        REGN = 0x4E474552, // 61
        NAVI = 0x4956414E, // 62
        CELL = 0x4C4C4543, // 63
        REFR = 0x52464552, // 64
        ACHR = 0x52484341, // 65
        PMIS = 0x53494D50, // 66
        PARW = 0x57524150, // 67
        PGRE = 0x45524750, // 68
        PBEA = 0x41454250, // 69
        PFLA = 0x414C4650, // 70
        PCON = 0x4E4F4350, // 71
        PBAR = 0x52414250, // 72
        PHZD = 0x445A4850, // 73
        WRLD = 0x444C5257, // 74
        LAND = 0x444E414C, // 75
        NAVM = 0x4D56414E, // 76
        TLOD = 0x444F4C54, // 77
        DIAL = 0x4C414944, // 78
        INFO = 0x4F464E49, // 79
        QUST = 0x54535551, // 80
        IDLE = 0x454C4449, // 81
        PACK = 0x4B434150, // 82
        CSTY = 0x59545343, // 83
        LSCR = 0x5243534C, // 84
        LVSP = 0x5053564C, // 85
        ANIO = 0x4F494E41, // 86
        WATR = 0x52544157, // 87
        EFSH = 0x48534645, // 88
        TOFT = 0x54464F54, // 89
        EXPL = 0x4C505845, // 90
        DEBR = 0x52424544, // 91
        IMGS = 0x53474D49, // 92
        IMAD = 0x44414D49, // 93
        FLST = 0x54534C46, // 94
        PERK = 0x4B524550, // 95
        BPTD = 0x44545042, // 96
        ADDN = 0x4E444441, // 97
        AVIF = 0x46495641, // 98
        CAMS = 0x534D4143, // 99
        CPTH = 0x48545043, // 100
        VTYP = 0x50595456, // 101
        MATT = 0x5454414D, // 102
        IPCT = 0x54435049, // 103
        IPDS = 0x53445049, // 104
        ARMA = 0x414D5241, // 105
        ECZN = 0x4E5A4345, // 106
        LCTN = 0x4E54434C, // 107
        MESG = 0x4753454D, // 108
        RGDL = 0x4C444752, // 109
        DOBJ = 0x4A424F44, // 110
        DFOB = 0x424F4644, // 111
        LGTM = 0x4D54474C, // 112
        MUSC = 0x4353554D, // 113
        FSTP = 0x50545346, // 114
        FSTS = 0x53545346, // 115
        SMBN = 0x4E424D53, // 116
        SMQN = 0x4E514D53, // 117
        SMEN = 0x4E454D53, // 118
        DLBR = 0x52424C44, // 119
        MUST = 0x5453554D, // 120
        DLVW = 0x57564C44, // 121
        WOOP = 0x504F4F57, // 122
        SHOU = 0x554F4853, // 123
        EQUP = 0x50555145, // 124
        RELA = 0x414C4552, // 125
        SCEN = 0x4E454353, // 126
        ASTP = 0x50545341, // 127
        OTFT = 0x5446544F, // 128
        ARTO = 0x4F545241, // 129
        MATO = 0x4F54414D, // 130
        MOVT = 0x54564F4D, // 131
        SNDR = 0x52444E53, // 132
        DUAL = 0x4C415544, // 133
        SNCT = 0x54434E53, // 134
        SOPM = 0x4D504F53, // 135
        COLL = 0x4C4C4F43, // 136
        CLFM = 0x4D464C43, // 137
        REVB = 0x42564552, // 138
        PKIN = 0x4E494B50, // 139
        RFGP = 0x50474652, // 140
        AMDL = 0x4C444D41, // 141
        LAYR = 0x5259414C, // 142
        COBJ = 0x4A424F43, // 143
        OMOD = 0x444F4D4F, // 144
        MSWP = 0x5057534D, // 145
        ZOOM = 0x4D4F4F5A, // 146
        INNR = 0x524E4E49, // 147
        KSSM = 0x4D53534B, // 148
        AECH = 0x48434541, // 149
        SCCO = 0x4F434353, // 150
        AORU = 0x55524F41, // 151
        SCSN = 0x4E534353, // 152
        STAG = 0x47415453, // 153
        NOCM = 0x4D434F4E, // 154
        LENS = 0x534E454C, // 155
        LSPR = 0x5250534C, // 156
        GDRY = 0x59524447, // 157
        OVIS = 0x5349564F, // 158
        // ReSharper restore InconsistentNaming
    }
}
