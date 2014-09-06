using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dicom;
using Dicom.IO.Writer;
using JsonConverter;
using Dicom.IO;

namespace JsonFileToDicom
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = @"test.dcm";
            var s = JsonToDicom.Load(args[0]);

            var df = new DicomFile(s);

            if (args.Length > 1)
            {
                output = args[1];
            }

            df.Save(output);
            Console.Write(s);
            Console.WriteLine("Done! Press any key.");
            Console.ReadKey();
        }
    }
}
