using NSR_Clone.Data;
using NSR_Clone.ObjectFolder;
using NSR_Clone.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSR_Clone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BatchViewModel();
        }

        private void Button_Click_AddSample(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.AddSample();
        }

        private void Button_Click_ToggleCustomers(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.ToggleCustomerList();
        }

        private void Button_Click_ToggleAnalysisJson(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.ToggleAnalysisOptions();
            vm.ToggleSelectedSampleOptions();
        }

        private void Button_Click_ToggleSamples(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.ToggleSampleList();
        }

        private void Button_Click_ToggleAnalyses(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.SelectSampleByIndex(SamplesListBox.SelectedIndex);
            vm.ToggleAnalysisList();
            vm.ToggleSelectedSampleOptions();
        }

        private void Button_Click_CloseAllSample(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.ToggleAnalysisList();
            vm.ToggleCloseButton();
        }

        private void Button_Click_SelectedSampleOptions(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            if (SamplesListBox.SelectedIndex == -1) return;
            vm.SelectSampleByIndex(SamplesListBox.SelectedIndex);
            vm.ToggleSelectedSampleOptions();
        }

        private void Button_Click_CompleteAnalysis(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            if (AnalysisListBox.SelectedIndex == -1) return;
            vm.CompleteSelectedAnalysis(AnalysisListBox.SelectedIndex);
            vm.ToggleCloseButton();
        }

        private void Button_Click_PickCustomer(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            string key = vm.CustomerKeys[CustomerListBox.SelectedIndex];
            vm.SetCustomer(key);
            vm.ToggleCustomerList();
        }

        private void Button_Click_PickAnalysis(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            if (AnalysisJsonListBox.SelectedIndex == -1) return;
            string key = vm.AnalysisKeys[AnalysisJsonListBox.SelectedIndex];
            vm.AddAnalysis(key);
            vm.ToggleAnalysisOptions();
        }

        private void Button_Click_OpenNewBatch(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            vm.CreateNewBatch();
        }

        private async void Button_Click_SaveBatch(object sender, RoutedEventArgs e)
        {
            BatchViewModel vm = (BatchViewModel)DataContext;
            await vm.SaveBatchToDB();
        }
    }
}