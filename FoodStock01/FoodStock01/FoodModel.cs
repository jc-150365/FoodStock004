using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FoodStock01
{
    [Table("Food")]//テーブル名を指定
    public class FoodModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int F_no { get; set; } //食材No

        public string F_name { get; set; } //食材名

        public int F_result { get; set; } //登録時点での、消費期限までの日数

        public DateTime F_date { get; set; } //消費期限

        public string F_limit { get; set; } //現在時刻との差（後で使うかも）

        public TimeSpan F_span { get; set; } //現在日時との差（後で使うかも）

        /********************インサートメソッド**********************************/
        public static void InsertFood(int f_no, string f_name, int f_result, DateTime f_date)
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<FoodModel>();

                    db.Insert(new FoodModel() { F_no = f_no, F_name = f_name, F_result = f_result, F_date = f_date });
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /*******************セレクトメソッド**************************************/
        public static List<FoodModel> SelectFood()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースに指定したSQLを発行
                    return db.Query<FoodModel>("SELECT * FROM [Food] ORDER BY [F_result]");

                }
                catch (Exception e)
                {

                    System.Diagnostics.Debug.WriteLine(e);
                    return null;
                }
            }
        }

        /********************デリートメソッド*************************************/
        public static void DeleteFood(int f_no)
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<FoodModel>();

                    db.Delete<FoodModel>(f_no);//デリートで渡す値は主キーじゃないといけない説
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /********************オールデリートメソッド*************************************/
        public static void DeleteAllFood()
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<FoodModel>();

                    db.DeleteAll<FoodModel>();//デリートで渡す値は主キーじゃないといけない説
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /********************アップデートメソッド（日付）*************************************/
        public static void UpdateF_date(int f_no, string f_name, int f_result, DateTime f_date)
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<FoodModel>();

                    //TimeSpan t = f_date - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1);//よくわからん
                    TimeSpan t = f_date - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//よくわからん

                    int span = t.Days;

                    db.Update(new FoodModel() { F_no = f_no, F_name = f_name, F_result = span, F_date = f_date });

                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /************************************ここから通知を試すためのセレクトメソッド************************************/
        public static int SelectF_result()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    int setting = 100;

                    setting = SettingModel.SelectSetting_Max();

                    List<FoodModel> resultList = db.Query<FoodModel>("SELECT [F_result] FROM [Food] WHERE [F_result] = " + setting) ;

                    int[] resultArray = new int[100];

                    int result = -999;

                    int i = 0;

                    foreach (FoodModel fdm in resultList)
                    {
                        resultArray[i] = fdm.F_result;
                        i++;
                    }

                    result = resultArray[0];

                    return result;

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return -999;
                }
            }
        }
        /************************************ここまで通知を試すためのセレクトメソッド************************************/
    }
}