using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FoodStock01
{
    [Table("Setting")]//テーブル名を指定
    public class SettingModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Set_no { get; set; } //設定No

        public int Set_alert { get; set; } //通知日数

        /********************インサートメソッド（通知日数）**********************/
        public static void InsertSetting(int set_no, int set_alert)
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<SettingModel>();

                    db.Insert(new SettingModel() { Set_no = set_no, Set_alert = set_alert });
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /*******************セレクトメソッド（最新の通知日数）*************************************/
        public static List<SettingModel> SelectSetting()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースに指定したSQLを発行
                    return db.Query<SettingModel>("SELECT [Set_alert] FROM [Setting]");

                }
                catch (Exception e)
                {

                    System.Diagnostics.Debug.WriteLine(e);
                    return null;
                }
            }
        }

        /********************アップデートメソッド（2回目以降の通知設定）**********************/
        public static void UpdateSetting(int set_no, int set_alert)
        {
            //データベースに接続する
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    //データベースにFoodテーブルを作成する
                    db.CreateTable<SettingModel>();

                    db.Update(new SettingModel() { Set_no = set_no, Set_alert = set_alert });
                    db.Commit();
                }
                catch (Exception e)
                {
                    db.Rollback();
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        /*******************セレクトメソッド（試し）*************************************/
        public static int SelectSetting_Max()
        {
            using (SQLiteConnection db = new SQLiteConnection(App.dbPath))
            {
                try
                {
                    /**********試し*************/
                    List<SettingModel> SetList = db.Query<SettingModel>("SELECT [Set_alert] FROM [Setting]"); 

                    int[] SetArray = new int[100];

                    int alert = 1;

                    int i = 0;

                    foreach (SettingModel stm in SetList)
                    {
                        //SetArray[i++] = stm.Set_alert;
                        SetArray[i] = stm.Set_alert;
                        i++;
                    }

                    alert = SetArray[0];

                    //データベースに指定したSQLを発行
                    return alert;

                }
                catch (Exception e)
                {

                    System.Diagnostics.Debug.WriteLine(e);
                    //return null;
                    return -999;
                }
            }
        }
    }
}