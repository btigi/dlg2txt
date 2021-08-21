using DlgToTxt.Model;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DlgToTxt
{
    public static class Common
    {        
        public const int NewString = -1;

        public const Int32 Bit0 = 1;
        public const Int32 Bit1 = 2 << 0;
        public const Int32 Bit2 = 2 << 1;
        public const Int32 Bit3 = 2 << 2;
        public const Int32 Bit4 = 2 << 3;
        public const Int32 Bit5 = 2 << 4;
        public const Int32 Bit6 = 2 << 5;
        public const Int32 Bit7 = 2 << 6;
        public const Int32 Bit8 = 2 << 7;
        public const Int32 Bit9 = 2 << 8;
        public const Int32 Bit10 = 2 << 9;
        public const Int32 Bit11 = 2 << 10;
        public const Int32 Bit12 = 2 << 11;
        public const Int32 Bit13 = 2 << 12;
        public const Int32 Bit14 = 2 << 13;
        public const Int32 Bit15 = 2 << 14;
        public const Int32 Bit16 = 2 << 15;
        public const Int32 Bit17 = 2 << 16;
        public const Int32 Bit18 = 2 << 17;
        public const Int32 Bit19 = 2 << 18;
        public const Int32 Bit20 = 2 << 19;
        public const Int32 Bit21 = 2 << 20;
        public const Int32 Bit22 = 2 << 21;
        public const Int32 Bit23 = 2 << 22;
        public const Int32 Bit24 = 2 << 23;
        public const Int32 Bit25 = 2 << 24;
        public const Int32 Bit26 = 2 << 25;
        public const Int32 Bit27 = 2 << 26;
        public const Int32 Bit28 = 2 << 27;
        public const Int32 Bit29 = 2 << 28;
        public const Int32 Bit30 = 2 << 29;
        public const Int32 Bit31 = 2 << 30;

        public static Object ReadStruct(BinaryReader br, Type t)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(t));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            Object s = (Object)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), t);
            handle.Free();
            return s;
        }

        public static string TryGetString(char[] chars)
        {
            //chars = chars.Where(a => a != '\0').ToArray();
            //return new string(chars);

            if ((chars.Length > 0) && (chars[0] != '\0'))
            {
                return new string(chars).TrimEnd('\0');
            }
            return String.Empty;
        }

        public static IEString ReadString(Int32 strref, TlkFile tlkFile)
        {
            var stringInfo = new IEString();
            stringInfo.Strref = strref;

            if (tlkFile != null)
            {
                if ((strref <= tlkFile.Strings.Count) && (strref > -1))
                {
                    stringInfo.Flags = tlkFile.Strings[strref].Flags;
                    stringInfo.PitchVariance = tlkFile.Strings[strref].PitchVariance;
                    stringInfo.Sound = tlkFile.Strings[strref].Sound;
                    stringInfo.Text = tlkFile.Strings[strref].Text;
                    stringInfo.VolumeVariance = tlkFile.Strings[strref].VolumeVariance;
                }
            }
            return stringInfo;
        }

        public struct IEString
        {
            public string Text { get; set; }
            public StringEntryType Flags { get; set; }
            public string Sound { get; set; }
            public Int32 VolumeVariance { get; set; }
            public Int32 PitchVariance { get; set; }
            public Int32 Strref { get; set; }
        }

        public enum StringEntryType
        {
            NoMessageData = 0,
            Text = 1,
            Sound = 2,
            Standard = 3,
            Tagged = 7
        }
    }
}