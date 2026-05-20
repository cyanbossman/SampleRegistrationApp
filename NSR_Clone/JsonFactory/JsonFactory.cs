using NSR_Clone.ObjectFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace NSR_Clone.JsonFactory
{
    public class AnalysisFactory
    {
        private static Dictionary<string, AnalysisData> _AnalysisData;

        static AnalysisFactory()
        {
            string json = File.ReadAllText("AnalysisData.json");
            _AnalysisData = JsonSerializer.Deserialize<Dictionary<string, AnalysisData>>(json);
        }

        public static AnalysisObject CreateAnalysis(string key)
        {
            AnalysisData data = _AnalysisData[key];
            return new AnalysisObject(data.NAME, data.ID);
        }

        public static Dictionary<string, AnalysisData> GetAll()
        {
            return _AnalysisData;
        }
    }

    public class CustomerFactory
    {
        private static Dictionary<string, CustomerData> _CustomerData;

        static CustomerFactory()
        {
            string json = File.ReadAllText("CustomerData.json");
            _CustomerData = JsonSerializer.Deserialize<Dictionary<string, CustomerData>>(json);
        }

        public static CustomerObject CreateCustomer(string key)
        {
            CustomerData data = _CustomerData[key];
            return new CustomerObject(data.NAME, data.CVR, data.ID);
        }

        public static Dictionary<string, CustomerData> GetAll()
        {
            return _CustomerData;
        }
    }
}