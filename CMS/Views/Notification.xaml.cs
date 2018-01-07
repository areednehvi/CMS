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
using System.Windows.Threading;

namespace CMS.Views
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        public Notification()
        {
            InitializeComponent();
            tbTitle.Text = "Notification";
            tbMessage.Text = "Saved Successfully";
        }

        public Notification(string Title, string Message, Boolean IsError = false)
        {
            InitializeComponent();
            tbTitle.Text = Title;
            if (IsError)
            {
                tbTitle.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF3939"));
            }
            tbMessage.Text = Message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartCloseWindow();
            PlaceWindowToRightCorner();
        }

        private void PlaceWindowToRightCorner()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 5;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void StartCloseWindow()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            Close();
        }

        
    }
}
