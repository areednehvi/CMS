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
using System.Windows.Shapes;

namespace CMS.Views
{
    /// <summary>
    /// Interaction logic for testView.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            ((DashboardController)gbStudentGenderRatioWidget.DataContext).Dashboard.StudentGenderRatioWidget.GBStudentGenderRatioWidget = this.gbStudentGenderRatioWidget;
            ((DashboardController)gbStudentGenderRatioWidget.DataContext).SetupStudentRatioWidget();

            ((DashboardController)gbStudentPaymentAsPerMonthWidget.DataContext).Dashboard.StudentPaymentAsPerMonthWidget.GBStudentPaymentAsPerMonthWidget = this.gbStudentPaymentAsPerMonthWidget;
            ((DashboardController)gbStudentPaymentAsPerMonthWidget.DataContext).SetupStudentPaymentAsPerMonthWidget();
        }
    }
}
