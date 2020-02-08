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
using System.Windows.Shapes;

namespace Crud.View
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window , IDisposable
    {
        AddWindowViewModel addWindowViewModel;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AddWindow()
        {
            addWindowViewModel = new AddWindowViewModel(this);
            InitializeComponent();
            this.DataContext = addWindowViewModel;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
