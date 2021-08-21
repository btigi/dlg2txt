using System;
using System.Collections.Generic;
using static DlgToTxt.Common;

namespace DlgToTxt.Model
{
    public class TlkFile
    {
        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        public Int16 LangugeId;
        public List<StringEntry> Strings = new List<StringEntry>();
    }

    public class StringEntry
    {
        public string Text { get; set; }
        public StringEntryType Flags { get; set; }
        public string Sound { get; set; }
        public Int32 VolumeVariance { get; set; }
        public Int32 PitchVariance { get; set; }
        public Int32 Strref { get; set; }
    }
}