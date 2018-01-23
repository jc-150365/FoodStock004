using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodStock01;
using System.Windows.Input;

namespace FoodStock01
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage1 : ContentPage
    {
        String s = "http://cookpad.com/search/";

        public FoodPage1(string title)
        {
            //タイトル
            Title = title;

            InitializeComponent();

        }

        /****************起動時にアラートが出る************************/
        protected override void OnResume()
        {
            /*
            var query = FoodModel.SelectFood02();
            base.OnResume();
            //設定した通知日数に合致する食材あればアラート↓
              if(query >= 1)
              {
                DisplayAlert("消費期限通知", "期限が近づいている食材があります", "OK");
               }
             */
            base.OnResume();

            DisplayAlert("消費期限通知", "期限が近づいている食材があります", "OK");
        }
        /**************************************************************/

        void ChackBoxChanged(object sender, bool isChecked)
        {
            //選択された時の処理
            if (isChecked)
            {
                s += ((CheckBox)sender).Text + "　";
            }
            //選択が外された時の処理
            else
            {
                s = s.Replace(((CheckBox)sender).Text + "　", "");
            }
        }

        //デリート押された
        //void OnDelete_Clicked(object sender, EventArgs args)
        async void OnDelete_Clicked(object sender, EventArgs args)
        {
            string no = ((CustomButtonDelete)sender).NoText;
            string name = ((CustomButtonDelete)sender).NameText;

            /*
            DisplayAlert("Delete","主キー"+no+" "+name,"OK");
            
            int f_no = int.Parse(no);
            FoodModel.DeleteFood(f_no);
            Title = "食材リスト";
            s = "http://cookpad.com/search/";
            InitializeComponent();
            */
            var result = await DisplayAlert("削除", "この食材を削除しますか", "OK", "キャンセル");//
            if (result == true)
            {
                int f_no = int.Parse(no);
                FoodModel.DeleteFood(f_no);

                Title = "食材リスト";
                s = "http://cookpad.com/search/";//

                InitializeComponent();
            }
        }

        void OnSearch_Clicked(object sender, EventArgs args)
        {
            //ページ遷移
            Navigation.PushAsync(new NextPage(s));
        }

        //引っ張ったとき（更新）
        private async void OnRefreshing(object sender, EventArgs e)
        {
            // 1秒処理を待つ
            await Task.Delay(1000);

            //リフレッシュを止める
            list.IsRefreshing = false;

            Title = "食材リスト";
            s = "http://cookpad.com/search/";

            InitializeComponent();
        }
    }
}