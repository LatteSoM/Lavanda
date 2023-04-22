using Kalendarus.Images.FileManipulations;
using Kalendarus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
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
using System.Xml.Linq;

namespace Kalendarus
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        public static DateTime date = DateTime.Now;
        static int bi = howMuchDays();
        private List<DayControl> daysControlElemenrs = ttt(bi);

        
        public List<DayControl> _daysControlElemenrs
        {
            get { return daysControlElemenrs; }
            set 
            {
                daysControlElemenrs = value;
                OnPropertyChanged();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            monLbl.Content = date.ToLongDateString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        static private int howMuchDays()
        {
            int day = DateTime.DaysInMonth(Convert.ToInt32(date.Year), Convert.ToInt32(date.Month)); 
            return day;
        }

        private static  List<DayControl> ttt(int count)
        {
            List<DayControl> a = new List<DayControl>();
            for (int i = 0; i< count; i++)
            {
                a.Add(new DayControl());
                a[i].dayNum.Content = (i + 1).ToString();
            }

            return a;
        }

        private void rightBtn_Click(object sender, RoutedEventArgs e)
        {
            date = date.AddMonths(1);
            var dtaeInd = howMuchDays();
            monLbl.Content = date.ToLongDateString();
            _daysControlElemenrs = ttt(dtaeInd);
        }

        
        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            date = date.AddMonths(-1);
            var dtaeInd = howMuchDays();
            monLbl.Content = date.ToLongDateString();
            _daysControlElemenrs = ttt(dtaeInd);
        }
    }
}
