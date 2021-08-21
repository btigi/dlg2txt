using DlgToTxt.Model;
using DlgToTxt.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DlgToTxt
{
    public class Processor
    {
        public void Process(string tlkFileLocation, string listingFileLocation)
        {
            var tlkReader = new TlkFileReader();
            var tlkFile = tlkReader.Read(tlkFileLocation);

            var dlgReader = new DlgFileReader();
            dlgReader.TlkFile = tlkFile;

            var dlgFiles = ProcessListingFile(listingFileLocation, dlgReader);

            var strings = new List<string>();
            foreach (var dlgFile in dlgFiles)
            {
                foreach (var state in dlgFile.file.States)
                {
                    var text = state.ResponseText.Text;
                    text = ProcessText(text);
                    strings.Add($"{text},{dlgFile.npc}");
                }
            }

            WriteOutput(strings);
        }

        private List<(DlgFile file, string npc)> ProcessListingFile(string filename, DlgFileReader dlgFileReader)
        {
            var result = new List<(DlgFile file, string npc)>();
            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var parts = line?.Split(",");

                if (parts?.Length == 2)
                {
                    if (File.Exists(parts[0]))
                    {
                        result.Add((dlgFileReader.Read(parts[0]), parts[1]));
                    }
                    else
                    {
                        throw new Exception($"{parts[0]} does not exist");
                    }
                }
            }
            return result;
        }

        private string ProcessText(string text)
        {
            text = text.Replace("\"", "");
            text = text.Replace(",", "");
            return text;
        }

        private void WriteOutput(List<string> strings)
        {
            var uniqueStrings = strings.Where(w => !string.IsNullOrEmpty(w)).Distinct().ToList();
            File.WriteAllLines("output.csv", uniqueStrings);
        }
    }
}