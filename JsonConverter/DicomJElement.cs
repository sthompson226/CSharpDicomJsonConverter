using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DicomJsonConverter
{
    public class DicomJElement
    {
        public JObject Element;
        public String Tag = "";
        public String Vr = "";
        public List<Dictionary<string, DicomJElement>> sqItems = new List<Dictionary<string, DicomJElement>>();

        public String GetString(int index = 0, bool literalName = false)
        {
            Boolean personName = false;
            JObject elm = Element;
            if (elm == null) return null;
            JToken vals = elm["Values"];
            if (Vr.ToLower() == "pn") personName = true;
            int count = vals.Count();
            if (count == 0 || index >= count) return null;
            if (personName == false)
            {
                string v = vals[index].ToString();
                return v;
            }

            if (literalName)
            {
                return "";
            }

            JToken an = vals[0]["Alphabetic"];
            return an.ToString();
        }

        public String[] GetStrings()
        {
            JObject elm = Element;
            if (elm == null) return null;
            JToken vals = elm["Values"];
            return vals.Select(v => v.ToString()).ToArray();
        }

        public Double GetDouble(int index = 0)
        {
            JObject elm = Element;
            if (elm == null) return Double.NaN;
            JToken vals = elm["Values"];
            int count = vals.Count();
            if (count == 0 || index >= count) return Double.NaN;
            string v = vals[index].ToString();
            Double d;
            if (Double.TryParse(v, out d)) return d;
            return Double.NaN;
        }

        public int Count()
        {
            JObject elm = Element;
            if (elm == null) return 0;
            JToken vals = elm["Values"];
            return vals.Count();
        }

        public override string ToString()
        {
            return Element.ToString();
        }
    }
}