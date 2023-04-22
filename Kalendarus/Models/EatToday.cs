using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Kalendarus.Models
{
    internal class EatToday
    {
        public static List<EatToday> listOfDaysWhenUserEat = new List<EatToday>();
        public static List<EatToday> manip = new List<EatToday>();
        public string selectDate;
        public List<ListItem> selectedFoodItems;
    }
}
