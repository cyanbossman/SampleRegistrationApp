using NSR_Clone.Data;
using NSR_Clone.JsonFactory;
using NSR_Clone.ObjectFolder;
using NSR_Clone.SampleClass;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Documents;


namespace NSR_Clone.ObjectFolder
{
    public class BatchClass
    {
        public BatchClass() { }

        public static BatchClass Create()
        {
            var batch = new BatchClass();
            batch.AddSample();
            return batch;
        }

        public int Id { get; set; }
        public int DbId { get; set; }//Related to SQL preparation

        public CustomerObject? Customer { get; set; }
        public List<SampleClass.SampleClass> Samples { get; set; } = [];

        public bool IsDone =>
            Samples.Count > 0 &&
            Customer != null &&
            Samples.All(a => a.IsDone());

        public bool IsReadyForSave =>
            Samples.Count > 0 &&
            Customer != null &&
            Samples.All(a => a.Analysis != null);
            

        public void AddSample()
        {
            int NextSampleNumber = Samples.Count > 0 ? Samples.Last().SampleNumber + 1 : 1;
            Samples.Add(new SampleClass.SampleClass { SampleNumber = NextSampleNumber });
        }

        public Dictionary<int, bool> GetSampleStatuses()
        {
            return Samples.ToDictionary(s => s.SampleNumber, s => s.IsDone());
        }        
        
        public void SetCustomer(string customerId)
        {
            Customer = null;
            Customer = CustomerFactory.CreateCustomer(customerId);
        }

        public void AddAnalysisToSample(int sampleNumber, string key)
        {
            SampleClass.SampleClass? sample = Samples.FirstOrDefault(s => s.SampleNumber == sampleNumber);
            sample?.AddAnalysis(key);
        }

        public void CompleteAnalysis(int sampleNumber, int analysis)
        {
            Samples[sampleNumber].CompleteAnalysis(analysis);
        }

        public async Task CreateBatchAndSave()
        {
            if (IsReadyForSave == false) return;
            using var db = new AppDbContext();
            if (DbId == 0)
                db.Batches.Add(this);
            else
                db.Batches.Update(this);
            await db.SaveChangesAsync();
        }
    }
}

