using Kalendarus.Images.FileManipulations;
using Kalendarus.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using ListItem = Kalendarus.Models.ListItem;

namespace Kalendarus
{
    /// <summary>
    /// Логика взаимодействия для SelectPage.xaml
    /// </summary>
    public partial class SelectPage : Page
    {
        private static List<FoodItemControl> items = new List<FoodItemControl>();
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "userSelecr.json";
        bool fileExist = File.Exists(path);
        bool existIntemOnThisDaty = false;

        public SelectPage()
        {
            InitializeComponent();
            monLbl.Content = DayControl.dateOfSelectDay.ToLongDateString();
            try
            {
                if (fileExist)
                {
                    EatToday.listOfDaysWhenUserEat = Kurvalizator.myDeser<List<EatToday>>("userSelecr.json").ToList();
                    EatToday.manip = EatToday.listOfDaysWhenUserEat.Where(
                        x => x.selectDate == DayControl.dateOfSelectDay.ToLongDateString()).ToList();
                    if (EatToday.listOfDaysWhenUserEat.Count > 0) { existIntemOnThisDaty = true; }
                }
            }
            catch { }
            placeItems();
        }

        private List<Uri> pictureFill()
        {
            List<Uri> list = new List<Uri>();
            string a = System.IO.Path.Combine(Directory.GetCurrentDirectory() + "..\\..\\..\\Images\\foodIco");
            var dir = new DirectoryInfo(a);
            foreach (var file in dir.GetFiles())
            {
                Uri url = new Uri(file.FullName, UriKind.Absolute);
                list.Add(url);
            }
            return list;
        }

        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            exiteToMainPage();
        }

        private void exiteToMainPage()
        {
            items.Clear();
            MainWindow win = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (win != null)
            {
                win.Pageframe.Content = new MainPage();
            }
        }

        private void placeItems()
        {
            List<Uri> uris = pictureFill();
            List<string> fdName = new List<string>() { "cake", "pizza", "sushi", "taco" };
            for (int i = 0; i < 4; i++)
            {
                items.Add(new FoodItemControl());
                items[i].pic.Source = new BitmapImage(uris[i]);
                items[i].pic.Height = 50;
                items[i].foodName.Content = fdName[i];
                items[i].foodName.Content.ToString();

                if (existIntemOnThisDaty && EatToday.manip.Count != 0)
                {
                    foreach (var dish in EatToday.manip[0].selectedFoodItems)
                    {
                        if (dish.nameOfDish == items[i].foodName.Content.ToString())
                        {
                            items[i].pic.Source = dish.picturePath;
                            items[i].isSelect.IsChecked = dish.select;
                            items[i].foodName.Content = dish.nameOfDish;
                            items[i].foodName.Content.ToString();
                        }
                    }
                }
            }
            panel.ItemsSource = items;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            List<FoodItemControl> sorted = items.Where(x => x.isSelect.IsChecked == true).ToList();
            List<ListItem> sortedCollectionToSave = new List<ListItem>();

            foreach (var item in sorted)
            {
                sortedCollectionToSave.Add(new ListItem()
                {
                    select = (bool)item.isSelect.IsChecked,
                    picturePath = item.pic.Source,
                    pictureHigh = item.pic.Height,
                    nameOfDish = item.foodName.Content.ToString()
                });
            }
            EatToday thisUserEatToday = new EatToday()
            {
                selectDate = DayControl.dateOfSelectDay.ToLongDateString(),
                selectedFoodItems = sortedCollectionToSave
            };
            int delete = 1;
            foreach (var itemn in EatToday.manip)
            {
                if (itemn.selectDate == thisUserEatToday.selectDate) 
                { 
                    EatToday.listOfDaysWhenUserEat.RemoveAt(EatToday.listOfDaysWhenUserEat.IndexOf(itemn));
                    break;
                }
            }
            EatToday.listOfDaysWhenUserEat.Add(thisUserEatToday);
            Kurvalizator.mySer(EatToday.listOfDaysWhenUserEat, "userSelecr.json");
            exiteToMainPage();
        }

    }
}
