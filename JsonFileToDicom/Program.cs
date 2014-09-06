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
            var s = JsonToDicom.Load(args[0]);

            var df = new DicomFile(s);
            
            df.Save(@"C:\temp\junk.dcm");
            Console.Write(s);
            Console.WriteLine("Done! Press any key.");
            Console.ReadKey();
        }
    }
}
