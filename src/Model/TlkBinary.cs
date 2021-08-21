using System;
using System.Runtime.InteropServices;

namespace DlgToTxt.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TlkHeaderBinary
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] ftype;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 4)]
        public char[] fversion;
        public Int16 LanguageId;
        public Int32 StringCount;
        public Int32 StringOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct TlkEntryBinary
    {
        public Int16 Flags;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public char[] Sound;
        public Int32 VolumeVariance;
        public Int32 PitchVariance;
        public Int32 StringIndex;
        public Int32 StringLength;
    }
}