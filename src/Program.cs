using DlgToTxt.Model;
using System;
using System.IO;

namespace DlgToTxt
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: ");
                Console.WriteLine("  dlgtotxt dialog.tlk filelisting.csv");
                return (int)ExitCode.InvalidArguments;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("TLK file does not exist");
                return (int)ExitCode.NoTlkFile;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine("File listing does not exist");
                return (int)ExitCode.NoListingFile;
            }

            var processor = new Processor();
            try
            {
                processor.Process(args[0], args[1]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)ExitCode.GenericError;
            }

            return (int)ExitCode.Success;
        }
    }
}