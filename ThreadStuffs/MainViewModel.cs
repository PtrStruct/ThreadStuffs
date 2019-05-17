using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ThreadStuffs
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<string> Customers { get; set; }
        public RelayCommand StartAddingCommand { get; set; }
        public MainViewModel()
        {
            Customers = new ObservableCollection<string>();
            StartAddingCommand = new RelayCommand(o => AddCustomers(), o => true);
        }


        private void AddCustomers()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
            
            

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    foreach (var VARIABLE in GetHTML("https://pastebin.com/raw/4N8MTwiP"))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Customers.Add(VARIABLE.ToString());
                        });
                    }
                }
                catch (Exception eh)
                {
                    Console.WriteLine(eh);
                    throw;
                }

            });
        }

        public string GetHTML(string page)
        {
            WebClient client = new WebClient();
            return client.DownloadString(page);
        }
    }
}
