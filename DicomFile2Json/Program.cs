using System;
using System.IO;
using DicomJsonConverter;


namespace DicomFile2Json
{
    class Program
    {
        // A simple test driver. Feed any DICOM Part 10 file to it.
        static void Main(string[] args)
        {
            var s = DicomToJson.Convert(args[0], DicomToJson.OutputFormat.AddLinefeeds, 50
                * 1024 * 1024);
            if (args.Length == 1)
            {
                Console.Write(s);
                Console.WriteLine("Done! Press any key.");
                Console.ReadKey();
            }
            else
            {
                var f = File.CreateText(args[1]);
                f.WriteLine(s);
                f.Close();
            }
        }
    }
}
