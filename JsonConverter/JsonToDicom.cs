using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Dicom;
using Newtonsoft.Json.Linq;

namespace JsonConverter
{
    public class JsonToDicom
    {
        public static DicomDataset Parse(String json)
        {
            return CreateDataSet(json);
        }

        public static DicomDataset Load(String path)
        {
            var fd = File.OpenText(path);
            var s = fd.ReadToEnd();
            return Parse(s);
        }

        public static DicomDataset CreateDataSet(String json)
        {
            var ds = new DicomDataset();
            var dj = new DicomJObject();
            var elements = dj.Parse(json);

            foreach (var element in elements)
            {
                Insert(ds, element.Value);
            }

            return ds;
        }

        private static void Insert(DicomDataset dataSet, DicomJElement jElement)
        {
            var tag = jElement.Tag;
            var vr = jElement.Vr;
            var dcmTag = DicomTag.Parse(tag);
            var dcmVr = dcmTag.DictionaryEntry.ValueRepresentations[0];

            var strValues = jElement.GetStrings();

            if (vr == "SQ")
            {
                var sqItems = new List<DicomDataset>();

                foreach (var jSqItem in jElement.sqElements)
                {
                    var sqItem = new DicomDataset();

                    foreach (var jSqElement in jSqItem)
                    {
                        Insert(sqItem, jSqElement.Value);
                    }

                    sqItems.Add(sqItem);
                }

                dataSet.Add(dcmTag, sqItems.ToArray());

                return;
            }

            switch (vr)
            {
                case "AT": // Attribute Tag
                {
                    var result = strValues.Select(DicomTag.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "FD":
                {
                    var result = strValues.Select(Double.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "FL":
                {
                    var result = strValues.Select(float.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "PN":
                {
                    // Note that Multiname fields are enforced in the new DICOM Web Standard
                    var n = JObject.Parse(strValues[0]);
                    var aname = (string) n["Alphabetic"];
                    dataSet.Add(dcmTag, aname);
                    break;
                }
                case "SL":
                {
                    var result = strValues.Select(Int32.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "SS":
                {
                    var result = strValues.Select(Int16.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "UL":
                {
                    var result = strValues.Select(UInt32.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }
                case "UN":
                {
                    var result = strValues.Select(byte.Parse).ToArray();
                    dataSet.Add(dcmTag, result);
                    break;
                }

                case "OB":
                case "OW":
                case "OF":
                {
                    if (strValues.Length > 0)
                    {
                        var buff = System.Convert.FromBase64String(strValues[0]);
                        dataSet.Add(dcmTag, buff);
                    }
                    break;
                }


                default:
                {
                    dataSet.Add<String>(dcmTag, strValues);
                    break;
                }
            }

            
        }

    }
}
