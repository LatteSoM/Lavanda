using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace Kalendarus
{
    /// <summary>
    /// Логика взаимодействия для DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {
        public static DateTime dateOfSelectDay;
        public DayControl()
        {
            InitializeComponent();
        }

        private void dayBut_Click(object sender, RoutedEventArgs e)
        {
            dateOfSelectDay = new DateTime(MainPage.date.Year, MainPage.date.Month, Convert.ToInt32(dayNum.Content));
            MainWindow win = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (win != null)
            {
                win.Pageframe.Content = new SelectPage();
            }
        }
    } 
}
