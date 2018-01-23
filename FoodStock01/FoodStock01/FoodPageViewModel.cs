using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodStock01
{
    class FoodPageViewModel
    {
        public ObservableCollection<Food> Foods
        {
            get;
            private set;
        }

        public FoodPageViewModel()
        {
            if (FoodModel.SelectFood() != null)
            {
                var query01 = FoodModel.SelectFood();

                Foods = new ObservableCollection<Food>();
                foreach (var food01 in query01)
                {
                    Food f01 = new Food
                    {
                        F_no = food01.F_no,
                        F_name = food01.F_name,
                        F_result = food01.F_result,
                        F_date = food01.F_date
                    };

                    FoodModel.UpdateF_date(food01.F_no, food01.F_name, food01.F_result, food01.F_date);
                    Foods.Add(f01);
                    //FoodModel.UpdateF_date(food01.F_no, food01.F_name, food01.F_result, food01.F_date);
                }

                Foods = new ObservableCollection<Food>();
                foreach (var food02 in query01)
                {
                    Food f02 = new Food
                    {
                        F_no = food02.F_no,
                        F_name = food02.F_name,
                        F_result = food02.F_result,
                        F_date = food02.F_date
                    };
                    Foods.Add(f02);
                }
            }
            else
            {
                Foods = new ObservableCollection<Food> {
                    new Food {
                       F_name = "NoData",
                       //F_date = new DateTime(1970,1,1)
                       F_result = 999
                    }
                };
            }
        }
    }

    public class Food
    {
        public int F_no { get; set; }//追加
        public string F_name { get; set; }
        public int F_result { get; set; }
        public DateTime F_date { get; set; }

    }
}