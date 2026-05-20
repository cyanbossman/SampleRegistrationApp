using System;
using System.Collections.Generic;
using System.Text;

namespace NSR_Clone.ObjectFolder
{
    public class AnalysisObject
    {
        public string Name { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public int SampleId { get; set; } //Related to SQL preparation
        public int Id { get; set; } 
        public int DbId { get; set; }//Related to SQL preparation
        public AnalysisObject() { } //Related to SQL preparation

        public AnalysisObject(string name, int id)
        {
            Name = name;
            Id = id;
        }
        public void CompleteAnalysis()
        {
            IsDone = true;
        }
    }

    public class AnalysisData
    {
        public string NAME { get; set; }
        public int ID { get; set; }
    }
}