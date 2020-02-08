using Crud.Model;
using Crud.View;
using Crud.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crud.UserControls
{
    /// <summary>
    /// Interaction logic for LoadingUserControl.xaml
    /// </summary>
    public partial class LoadingUserControlView : UserControl 
    {
        private LoadingUserControlViewModel loadingUserControlViewModel;

        public LoadingUserControlView(ref EventHandler load)
        {
            loadingUserControlViewModel = new LoadingUserControlViewModel(ref load);
            InitializeComponent();
            this.DataContext = loadingUserControlViewModel;
        }
    }
}
