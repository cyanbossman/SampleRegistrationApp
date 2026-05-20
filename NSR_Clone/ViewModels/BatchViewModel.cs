using NSR_Clone.ObjectFolder;
using NSR_Clone.SampleClass;
using NSR_Clone.JsonFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;

namespace NSR_Clone.ViewModels
{
    public class BatchViewModel : INotifyPropertyChanged
    {
        private BatchClass _batch = BatchClass.Create();

        public BatchClass Batch
        {
            get => _batch;
            set
            {
                _batch = value;
                OnPropertyChanged(nameof(SampleCount));
                OnPropertyChanged(nameof(IsDone));
                OnPropertyChanged(nameof(ChosenCustomer));
                OnPropertyChanged(nameof(SampleOptions));
                OnPropertyChanged(nameof(AnalysisOptions));
                SelectedSample = null;
            }
        }

        public int SampleCount => _batch.Samples.Count;
        public event PropertyChangedEventHandler? PropertyChanged;
        public SampleClass.SampleClass ?_SelectedSample;
        private bool _showCustomerList = false;
        private bool _showSampleList = false;
        private bool _showAnalysisList = false;
        private bool _showAnalysisOptions = false;
        private bool _showSelectedSampleOptions = false;
        //below had to be public to get from batch instance with public bool as further below
        //private bool _IsDone;
        private bool _showCloseButton = false;
        private int _selectedSampleNumber;
        public CustomerObject? ChosenCustomer => _batch.Customer;
        public List<string> CustomerKeys => CustomerFactory.GetAll().Keys.ToList();
        public List<string> AnalysisKeys => AnalysisFactory.GetAll().Keys.ToList();

        public void CreateNewBatch()
        {
            Batch = BatchClass.Create();
        }

        public bool IsDone
        {
            get => _batch.IsDone;
            set { IsDone = value; OnPropertyChanged(nameof(CompleteSelectedAnalysis)); OnPropertyChanged(nameof(SetCustomer)); }
        }
        public void SetSelectedSample(int sampleNumber)
        {
            _selectedSampleNumber = sampleNumber;
        }
        public SampleClass.SampleClass SelectedSample
        {
            get => _SelectedSample;
            set
            {
                _SelectedSample = value;
                OnPropertyChanged(nameof(SelectedSample));
                OnPropertyChanged(nameof(AnalysisOptions));
            }
        }

        //To access _batch without going public
        public void SelectSampleByIndex(int index)
        {
            if (index < 0 || index >= _batch.Samples.Count) return;
            SelectedSample = _batch.Samples[index];
            _selectedSampleNumber = SelectedSample.SampleNumber;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Visibility AnalysisOptionsVisibility =>
            _showAnalysisOptions ? Visibility.Visible : Visibility.Collapsed;

        public Visibility CustomerListVisibility =>
            _showCustomerList ? Visibility.Visible : Visibility.Collapsed;

        public Visibility SampleListVisibility =>
            _showSampleList ? Visibility.Visible : Visibility.Collapsed;
        public Visibility AnalysisListVisibility =>
            _showAnalysisList ? Visibility.Visible : Visibility.Collapsed;
        public Visibility CloseButtonVisibility =>
           _showCloseButton ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SelectedSampleOptions =>
           _showSelectedSampleOptions ? Visibility.Visible : Visibility.Collapsed;

        public void ToggleSampleList()
        {
            _showSampleList = !_showSampleList;
            OnPropertyChanged(nameof(SampleListVisibility));
        }

        public void ToggleSelectedSampleOptions()
        {
            _showSelectedSampleOptions = !_showSelectedSampleOptions;
            OnPropertyChanged(nameof(SelectedSampleOptions));
        }
        public void ToggleAnalysisOptions()
        {
            _showAnalysisOptions = !_showAnalysisOptions;
            OnPropertyChanged(nameof(AnalysisOptionsVisibility));
        }

        public void ToggleAnalysisList()
        {
            _showAnalysisList = !_showAnalysisList;
            OnPropertyChanged(nameof(AnalysisListVisibility));
        }
        public void ToggleCloseButton()
        {
            _showCloseButton = !_showCloseButton;
            OnPropertyChanged(nameof(CloseButtonVisibility));
        }

        public void ToggleCustomerList()
        {
            _showCustomerList = !_showCustomerList;
            OnPropertyChanged(nameof(CustomerListVisibility));
        }    

        public void AddSample()
        {
            _batch.AddSample();
            OnPropertyChanged(nameof(SampleCount));
            OnPropertyChanged(nameof(SampleOptions));
        }

        public void SetCustomer(string key)
        {
            _batch.SetCustomer(key);
            OnPropertyChanged(nameof(ChosenCustomer));
        }

        public void AddAnalysis(string key)
        {
            _batch.AddAnalysisToSample(_selectedSampleNumber, key);
            OnPropertyChanged(nameof(AnalysisOptions));
        }

        public List<string> CustomerOptions => CustomerFactory
            .GetAll()
            .Select(kvp => $"{kvp.Key} - {kvp.Value.NAME}")
            .ToList();

        public List<string> AnalysisOptions => SelectedSample?.Analysis
            .Select(a => $"Name: {a.Name} | Is Completed: {a.IsDone}")
            .ToList() ?? [];

        public List<string> SampleOptions => _batch.Samples
            .Select(s => $"{s.SampleNumber}") //.Select(s => $"{s.SampleNumber} - {s.Analysis}")
            .ToList();

        public List<string> AnalysisOptionsJson => AnalysisFactory
           .GetAll()
           .Select(kvp => $"{kvp.Key} - {kvp.Value.NAME}")
           .ToList();

        public void CompleteSelectedAnalysis(int AnalysisIndex)
        {
            if (SelectedSample == null) return;
            int sampleIndex = _batch.Samples.IndexOf(_SelectedSample);
            _batch.CompleteAnalysis(sampleIndex, AnalysisIndex);
            OnPropertyChanged(nameof(AnalysisOptions));
            OnPropertyChanged(nameof(IsDone));
        }

        public async Task SaveBatchToDB()
        {
            await _batch.CreateBatchAndSave();
        }
    }
}


