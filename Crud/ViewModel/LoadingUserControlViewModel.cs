using Crud.View;
using Crud.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Crud.ViewModel
{
    public class LoadingUserControlViewModel : UserControl ,INotifyPropertyChanged
    {
        private event EventHandler Load;

        public LoadingUserControlViewModel(ref EventHandler load)
        {
            this.Load += LoadingUserControlViewModel_Load;
            load = this.Load;
        }

        private void LoadingUserControlViewModel_Load(object sender, EventArgs e)
        {
            this.Loading = (bool)sender;
        }

        private bool loading;

        public bool Loading 
        {
            get { return loading; }
            set 
            {
                loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string update)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(update));
        }

    }
}