using NSR_Clone.JsonFactory;
using NSR_Clone.ObjectFolder;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Windows.Documents;

namespace NSR_Clone.SampleClass
{
    public class SampleClass
    {
        public List<AnalysisObject> Analysis { get; set; } = [];
        public int SampleNumber {  get; set; }
        public int BatchId { get; set; } //Related to SQL preparation
        public int Id { get; set; }
        public int DbId { get; set; }//Related to SQL preparation

        //Temp for testing!:
        //public SampleClass()
        //{
        //    AddAnalysis("1");
        //}
        public bool IsDone()
        {
            if (Analysis.Count == 0)
                return false;

            foreach (AnalysisObject analysis in Analysis)
            {
                if (!analysis.IsDone)
                    return false;
            }

            return true;
        }

        //Should be oneline for dict??: "public Dictionary<int, SampleStatus> GetSampleStatuses()" then:  "{return Samples.ToDictionary(s => s.SampleNumber, s => s.GetStatus());}" ?.
        public SampleStatus GetStatus()
        {
            if (Analysis.Count == 0) return SampleStatus.Created;
            if (IsDone()) return SampleStatus.Completed;
            if (Analysis.Any(a => a.IsDone)) return SampleStatus.InProgress;
            return SampleStatus.Created;
        }

        public void AddAnalysis(string key)
        {
            Analysis.Add(AnalysisFactory.CreateAnalysis(key));
        }
        public void CompleteAnalysis(int analysisNumber)
        {
            if (analysisNumber >= 0 && analysisNumber <= Analysis.Count)
                Analysis[analysisNumber].CompleteAnalysis();
        }
    }
}

