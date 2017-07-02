using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using WebApplication1.Models;
using WebApplication1.Helper;
using System.Reflection;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    //public class lenrnObsoleteAttribute
    //{
    //    [Obsolete("Use the New ", true)]
    //    public void OldMethod()
    //    {
    //        Console.WriteLine("it's Old");
    //    }
    //    public void NewMethod()
    //    {
    //        Console.WriteLine("it's New");
    //    }
    //}

    public class HomeController : Controller
    {
        //public void Inherited()
        //{
        //    MemberInfo info = typeof(WorldModel);
        //    object[] attributes = info.GetCustomAttributes(true);
        //    for (int i = 0; i < attributes.Length; i++)
        //    {
        //        TableAttribute attr = (TableAttribute)attributes[i];
        //        Response.Write(attr.TableName + attributes[i]);
        //    }
        //    Response.Write("--------------------------");
        //    info = typeof(WorldModel);
        //    attributes = info.GetCustomAttributes(true);
        //    for (int i = 0; i < attributes.Length; i++)
        //    {
        //        TableAttribute attr = (TableAttribute)attributes[i];
        //        Response.Write(attr.TableName + attributes[i]);
        //    }
        //}

        DBHelper dbhelper = new DBHelper();
        int DataLoopCount = 0;
        static int DataCount = 0;
        static int PageCount = 0;
        static int PageDataCount = Convert.ToInt32(WebConfigurationManager.AppSettings["PageDataCount"]);
        string _conn = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //[OutputCache(Duration =10)]
        //[OutputCache(CacheProfile = "TestConfigCache")]
        public ActionResult Index(string action)
        {
            //Inherited();
            if (TempData["DataIndex"] == null)
            {
                TempData["DataIndex"] = 0;//当前数据下标
                TempData["PageIndex"] = 1;//当前页数
            }
            //string action = collection["action"];
            if (action == "下一页")
            {
                //int DataLoopCount = 0;
                if (Convert.ToInt32(TempData["DataIndex"]) >= DataCount)
                {
                    @ViewBag.b = TempData["TempData"];
                    TempData["DataIndex"] = (TempData["DataIndex"]);
                    TempData["PageIndex"] = (TempData["PageIndex"]);
                    TempData["TempData"] = @ViewBag.b;
                    return View();
                }

                if (Convert.ToInt32(TempData["DataIndex"]) < DataCount)
                {
                    //using (MySqlConnection connection = new MySqlConnection(_conn))
                    //{
                    //connection.Open();
                    //string mysql = string.Format("SELECT * from eee WHERE State=0 limit {0} ,{1} ", TempData["DataIndex"], PageDataCount);
                    //MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                    //MySqlDataReader myreader = mycommand.ExecuteReader();
                    //MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //adapter.SelectCommand = mycommand;
                    //DataTable Dt = new DataTable();
                    //adapter.Fill(Dt);
                    //TempData["TempData"] = Dt.Rows;
                    //ViewBag.Dt = Dt.Rows;
                    //TempData["DataLoopCount"] = DataLoopCount + 4;
                    //MySqlDataReader Dr = mycommand.ExecuteReader();
                    //if (Convert.ToInt32(TempData["DataLoopCount"]) == 0)
                    //{
                    //    TempData["DataLoopCount"] = DataLoopCount + 4;
                    //}
                    //else
                    //{
                    //    TempData["DataLoopCount"] = Convert.ToInt32(TempData["DataLoopCount"]) + 4;
                    //}
                    //TempData["DataLoopCount"] = DataLoopCount + 4;
                    //TempData["DataLoopCount"] = Convert.ToInt32(TempData["DataLoopCount"]) + 4;
                    TempData["DataIndex"] = Convert.ToInt32(TempData["DataIndex"]) + 4;
                    List<eeeModel> WorldModelList = new List<eeeModel>();
                    WorldModelList = DBHelper.ConvertData<eeeModel>(" WHERE State=0 limit " + TempData["DataIndex"] + ",4");
                    TempData["DataIndex"] = Convert.ToInt32(TempData["DataIndex"]);
                    TempData["PageIndex"] = Convert.ToInt32(TempData["PageIndex"]) + 1;
                    //while (Dr.Read())
                    //{
                    //    eeeModel worldmodel = new eeeModel();
                    //    worldmodel.id = Convert.ToString(Dr["id"]);
                    //    worldmodel.Name = Convert.ToString(Dr["Name"]);
                    //    worldmodel.Advantage = Convert.ToString(Dr["Advantage"]);
                    //    worldmodel.Disadvantage = Convert.ToString(Dr["Disadvantage"]);
                    //    worldmodel.Country = Convert.ToString(Dr["Country"]);
                    //    worldmodel.Recommend_Models = Convert.ToString(Dr["Recommend_Models"]);
                    //    WorldModelList.Add(worldmodel);       
                    //}
                    ViewBag.b = WorldModelList;
                    // }
                }
            }

            if (action == "上一页")
            {
                if (Convert.ToInt32(TempData["PageIndex"]) > 1)
                {
                    TempData["DataIndex"] = PageDataCount * (Convert.ToInt32(TempData["PageIndex"]) - 2);
                    //using (MySqlConnection connection = new MySqlConnection(_conn))
                    //{
                    //    connection.Open();
                    //    string mysql = string.Format("SELECT*FROM eee WHERE State=0 limit {0},{1} ", TempData["DataIndex"], PageDataCount);
                    //    MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                    //MySqlDataAdapter adapter = new MySqlDataAdapter();
                    //adapter.SelectCommand = mycommand;
                    //DataTable Dt = new DataTable();
                    //adapter.Fill(Dt);

                    //TempData["TempData"] = Dt.Rows;
                    //ViewBag.Dt = Dt.Rows;
                    //MySqlDataReader Dr = mycommand.ExecuteReader();
                    //DataLoopCount += 1;
                    TempData["PageIndex"] = Convert.ToInt32(TempData["PageIndex"]) - 1;
                    List<eeeModel> WorldModelList = new List<eeeModel>();
                    WorldModelList = DBHelper.ConvertData<eeeModel>(" WHERE State=0 limit " + TempData["DataIndex"] + ",4");
                    TempData["DataIndex"] = Convert.ToInt32(TempData["DataIndex"]) + DataLoopCount;

                    //while (Dr.Read())
                    //{
                    //    eeeModel worldmodel = new eeeModel();
                    //    worldmodel.ld = Convert.ToString(Dr["ld"]);
                    //    worldmodel.Name = Convert.ToString(Dr["Name"]);
                    //    worldmodel.Advantage = Convert.ToString(Dr["Advantage"]);
                    //    worldmodel.Disadvantage = Convert.ToString(Dr["Disadvantage"]);
                    //    worldmodel.Country = Convert.ToString(Dr["Country"]);
                    //    worldmodel.Recommend_Models = Convert.ToString(Dr["Recommend_Models"]);
                    //    WorldModelList.Add(worldmodel);
                    //}

                    ViewBag.b = WorldModelList;
                    // }
                }
                else
                {
                    @ViewBag.Dt = TempData["TempData"];
                    TempData["DataIndex"] = (TempData["DataIndex"]);
                    TempData["PageIndex"] = (TempData["PageIndex"]);
                    TempData["TempData"] = @ViewBag.b;
                }
            }

            if (DataCount == 0)
            {
                // using (MySqlConnection connection = new MySqlConnection(_conn))
                // {
                //connection.Open();
                //string mysql = "select count(*) from eee";
                //MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                //DataCount = Convert.ToInt32(mycommand.ExecuteScalar());
                List<eeeModel> eeemodel = new List<eeeModel>();
                eeemodel = DBHelper.ConvertData<eeeModel>(" WHERE State=0 ");
                List<eeeModel> WorldModelList = new List<eeeModel>();
                WorldModelList = DBHelper.ConvertData<eeeModel>(" WHERE State=0 limit " + TempData["DataIndex"] + ",4");
                //Type type = WorldModelList.GetType();
                //PropertyInfo[] propertyinfo = type.GetProperties();
                DataCount = eeemodel.Count;
                PageCount = DataCount / PageDataCount;
                if (DataCount % PageDataCount != 0)
                {
                    PageCount += 1;
                }
                //}
                //using (MySqlConnection connection = new MySqlConnection(_conn))
                //{
                //connection.Open();
                int DataLoopCount = 0;
                //string mysql = string.Format("SELECT * from eee  WHERE State = 0 limit {0} ,{1} ", TempData["DataIndex"], PageDataCount);
                //MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                //MySqlDataReader myreader = mycommand.ExecuteReader();
                //MySqlDataAdapter adapter = new MySqlDataAdapter();
                //adapter.SelectCommand = mycommand;
                //DataSet Ds = new DataSet();
                //DataTable Dt = new DataTable();
                //adapter.Fill(Dt);

                TempData["PageIndex"] = Convert.ToInt32(TempData["PageIndex"]);
                //TempData["TempData"] = Dt.Rows;
                //ViewBag.Dt = Dt.Rows;
                //MySqlDataReader Dr = mycommand.ExecuteReader();

                //List<eeeModel> WorldModelList = new List<eeeModel>();
                //WorldModelList = DBHelper.ConvertData<eeeModel>();
                TempData["DataIndex"] = Convert.ToInt32(TempData["DataIndex"]) + DataLoopCount;
                ViewBag.b = WorldModelList;
                //}


                //using (MySqlConnection connection = new MySqlConnection(_conn))
                //{
                //}
                //string[] SqlData = new string[10];
                //int i = 0;
                //while (Ds.DataSet()) {
                // string a = myreader.GetString(1);
                // SqlData.SetValue(a,i);
                // i += 1;
                //}
                //myreader.Read();
                //ViewBag.data = SqlData;
                //}
                //string sql = "update city set name='abc' where id=1";
                //    MySqlCommand mycommand2 = new MySqlCommand(sql,connection);
                //    if (mycommand2.ExecuteNonQuery()==1) {
                //        ViewBag.Result = "update successfull";
                // }
            }
            if (action == "删除")
            {
                using (MySqlConnection connection = new MySqlConnection(_conn))
                {
                    //connection.Open();
                    string[] checks = Request.Form.GetValues("checkboxeds");
                    // int[] IdDel=new int[] { };
                    int a;
                    foreach (string i in checks)
                    {
                        if (i == "false") { }
                        else
                        {
                            a = Convert.ToInt32(i);
                            TempData["Id"] = a;
                            ViewBag.cc = a;
                            //string mysql = string.Format("UPDATE eee SET State=1 WHERE id={0}", a);
                            // MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                            if (dbhelper.Delete(a)) { }
                            //IdDel =new [] { i };
                        }
                    }
                    //TempData["fc"] = Convert.ToInt32(IdDel[0]);
                    //foreach ( ) {
                    //}
                }
            }
            //return View();
            //return Content("<Script>alert('请登录');history.go(-1);</Script>");
            if (action == "查询")
            {
                string key = Request["guanjianzi"];
                if (key != null)
                {
                    List<eeeModel> eeemodel = new List<eeeModel>();
                    eeemodel = DBHelper.SerData<eeeModel>(key);
                    //using (MySqlConnection connection = new MySqlConnection(_conn))
                    //{
                    //    connection.Open();
                    //    string mysql = string.Format("SELECT * FROM eee WHERE id={0} or Advantage={0} or Disadvantage={0} or Country={0} or Recommend_Models={0}", guanjianzi);
                    //    MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                    //}
                    ViewBag.b = eeemodel;
                }
            }
            return View();
        }

        public ActionResult Update(string action)
        {
            if (action == "保存")
            {
                string Name = Request["Name"];
                string Advantage = Request["Advantage"];
                string Disadvantage = Request["Disadvantage"];
                string Country = Request["Country"];
                if (Name != null && Advantage != null && Disadvantage != null && Country != null)
                {
                    //using (MySqlConnection connection = new MySqlConnection(_conn))
                    //{
                    //connection.Open();
                    //string mysql = string.Format("INSERT INTO eee(Name,Advantage,Disadvantage,Country) VALUES('{0}','{1}','{2}','{3}')", Name, Advantage, Disadvantage, Country);
                    //MySqlCommand mycommand = new MySqlCommand(mysql, connection);
                    eeeModel worldmodel = new eeeModel();
                    worldmodel.Name = Name;
                    worldmodel.Advantage = Advantage;
                    worldmodel.Disadvantage = Disadvantage;
                    worldmodel.Country = Country;

                    if (DBHelper.InsertData<eeeModel>(worldmodel) > 0)
                    {
                        ViewBag.sdasf = 1;
                    }
                    //}
                }
            }
            return View();
        }
        //public ActionResult About(FromCallection collection)
        //{
        //    if (TempData["aaa"]) {
        //        string aaa = TempData["aaa"].ToString();
        //    }
        //    string action = collection["action"];
        //    if (action == "SubmitToAbout1") {

        //    }
        //    else if (action== "SubmitToAbout2") {
        //    }
        //    //ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult About(WorldModel worldmodel) {
        //    if (string.IsNullOrEmpty(worldmodel.Name))
        //    {
        //        ModelState.AddModelError("Name", "'Name'是必须字段");
        //    }
        //    if (string.IsNullOrEmpty(worldmodel.CountryCode))
        //    {
        //        ModelState.AddModelError("CountryCode", "'CountryCode'是必须字段");
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        return View(worldmodel);
        //    }
        //    else
        //    {
        //        return Content("输入数据验证通过");
        //    }
        //    string sql = "insert into world values(Name="+worldmodel.Name+")";
        //}
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Lg()
        {
            return View();
        }
    }
}