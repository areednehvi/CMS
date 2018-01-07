using CMS.Controllers;
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

namespace CMS.Views
{
    /// <summary>
    /// Interaction logic for FeeCollection.xaml
    /// </summary>
    public partial class TransactionsReport : UserControl
    {
        public TransactionsReport()
        {
            InitializeComponent();
            double height = SystemParameters.PrimaryScreenHeight - 250;
            GridLength gl = new GridLength(height);
            grdRowNo2.Height = gl;
           
        }
    }
}
