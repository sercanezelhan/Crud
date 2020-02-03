using Crud.Model;
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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window , IDisposable
    {

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p"></param>
        public UpdateWindow(Person p)
        {
            InitializeComponent();
            this.DataContext = new UpdateWindowViewModel(p);
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
