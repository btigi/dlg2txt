using System.Collections.Generic;
using System.IO;
using DlgToTxt.Model;
using static DlgToTxt.Common;

namespace DlgToTxt.Reader
{
    public class TlkFileReader
    {
        public TlkFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public TlkFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var tlkFile = ParseFile(br);
                return tlkFile;
            }
        }

        private TlkFile ParseFile(BinaryReader br)
        {
            var header = (TlkHeaderBinary)Common.ReadStruct(br, typeof(TlkHeaderBinary));

            var stringDataEntries = new List<TlkEntryBinary>();
            var stringEntries = new List<string>();

            br.BaseStream.Seek(18, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                var stringDataEntry = (TlkEntryBinary)Common.ReadStruct(br, typeof(TlkEntryBinary));
                stringDataEntries.Add(stringDataEntry);
            }

            var stringLocation = header.StringOffset;
            br.BaseStream.Seek(header.StringOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StringCount; i++)
            {
                br.BaseStream.Seek(stringLocation, SeekOrigin.Begin);
                var stringEntry = br.ReadChars(stringDataEntries[i].StringLength);
                stringLocation += stringDataEntries[i].StringLength;
                stringEntries.Add(new string(stringEntry));
            }

            var tlk = new TlkFile
            {
                LangugeId = header.LanguageId
            };

            int stringIndex = 0;
            foreach (var data in stringDataEntries)
            {
                var stringInfo = new StringEntry
                {
                    Strref = stringIndex,
                    Flags = (StringEntryType)data.Flags,
                    PitchVariance = data.PitchVariance,
                    Sound = data.Sound.ToString(),
                    Text = stringEntries[stringIndex],
                    VolumeVariance = data.VolumeVariance
                };
                tlk.Strings.Add(stringInfo);
                stringIndex++;
            }

            return tlk;
        }
    }
}
