﻿using System;
using Dicom;
using DicomJsonConverter;
using JsonToDicom;


namespace JsonToDicom
{
    class Program
    {
        static void Main(string[] args)
        {
            var jq = new DicomJObject();
            jq.Load(args[0]);

            var vr = jq.GetVR(jq.Tag(DicomTag.PatientName));
            var v = jq.GetValue("00080018", 0);
            var v2 = jq.GetValue("00280010", 0);
            var se = jq.GetElement("00400275");
            var sq = jq.GetSQElement("00400275", 0 , "00400007");
            var t = sq.GetString();
            var s = sq.ToString();
                           
            Console.WriteLine(v);
        }
    }
}
