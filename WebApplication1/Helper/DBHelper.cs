using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using System.Diagnostics;
using System.Reflection;
using WebApplication1.Filters;

namespace WebApplication1.Helper
{
    public class DBHelper
    {
        public static string _conn = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        // public eee() { }
        //using (MySqlConnection connection = new MySqlConnection(_conn))
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from eee");
            strSql.Append("where Id=@Id");
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Id",MySqlDbType.Int32)
            };
            parameters[0].Value = Id;
            return Exists(strSql.ToString(), parameters);
        }
        public bool Exists(string sqlStr, MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(_conn))
            {
                connection.Open();
                MySqlCommand mycommand = new MySqlCommand(sqlStr, connection);
                bool a = Convert.ToBoolean(mycommand.ExecuteScalar());
                return a;
            }
        }

        public int ExecuteSql(string sqlStr, MySqlParameter[] parameters)
        {

            using (MySqlConnection connection = new MySqlConnection(_conn))
            {
                connection.Open();
                MySqlCommand mycommand = new MySqlCommand(sqlStr, connection);
                int a =Convert.ToInt32(mycommand.ExecuteScalar()) ;
                return a;
            }
        }
        //更新一条数据
        public bool Add(WebApplication1.Models.eeeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into eee(");
            strSql.Append("Name,Advantage,Disadvantage,Country)");
            strSql.Append(" values (");
            strSql.Append("'" + model.Name + "','" + model.Advantage + "','" + model.Disadvantage + "','" + model.Country + "')");
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Id",MySqlDbType.Int32,5),
                new MySqlParameter("@Name",MySqlDbType.VarChar,50),
                new MySqlParameter("@Advantage", MySqlDbType.VarChar,100),
                new MySqlParameter("@Disadvantage", MySqlDbType.VarChar,100),
                new MySqlParameter("@Country", MySqlDbType.VarChar,50),
                new MySqlParameter("@Recommend_Models",MySqlDbType.VarChar,50),
                new MySqlParameter("@State",MySqlDbType.Int32,5),
            };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Advantage;
            parameters[3].Value = model.Disadvantage;
            parameters[4].Value = model.Country;
            parameters[5].Value = model.Recommend_Models;
            parameters[6].Value = model.State;

            int rows = ExecuteSql(Convert.ToString(strSql), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //删除列表数据
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE eee SET State=1 WHERE id=" + Id);
            int rows = ExecuteSql(Convert.ToString(strSql), null);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int AccSer(string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Id FROM user WHERE Name='"+key+"'");
            int id = ExecuteSql(Convert.ToString(strSql), null);
            return id;
        }

        public int AccLg(string Name ,string Password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Id FROM user WHERE Name='"+ Name + "' and PassWord='"+ Password +"'");
            int id = ExecuteSql(Convert.ToString(strSql), null);
            return id;
        }

        //列表数据
        public static List<T> ConvertData<T>(string sql)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            string sqlStr = "select * from " + type.Name.Remove(type.Name.IndexOf("Model")) + sql;
            //TableAttribute Attr = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute),true).First();
            //string TableName = Attr.TableName;
            using (MySqlConnection connection = new MySqlConnection(_conn))
            {
                connection.Open();
                MySqlCommand mycommand = new MySqlCommand(sqlStr, connection);
                MySqlDataReader Dr = mycommand.ExecuteReader();
                while (Dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        for (int j = 0; j < Dr.FieldCount; j++)
                        {
                            if (properties[i].Name == Dr.GetName(j))
                            {
                                object value = Dr[j];
                                properties[i].SetValue(model, value, null);
                            }
                        }
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        //插入数据
        public static int InsertData<T>(T model)
        {
            int count = 0;
            List<T> list = new List<T>();
            Type type = typeof(T);
            StringBuilder strSql = new StringBuilder();
            PropertyInfo[] propertyinfo = type.GetProperties();
            strSql.Append("insert into " + type.Name.Remove(type.Name.IndexOf("Model")) + "(");
            foreach (var a in propertyinfo)
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }
                if (count > 1)
                {
                    strSql.Append(",");
                }
                strSql.Append(a.Name);
                count++;
            }
            strSql.Append(") values( ");
            count = 0;
            PropertyInfo[] propertyname = type.GetProperties();
            foreach (var b in propertyname)
            {
                object valuename = b.GetValue(model, null);
                if (count == 0)
                {
                    count++;
                    continue;
                }
                if (count > 1)
                {
                    strSql.Append(",");
                }
                strSql.Append("'");
                strSql.Append(valuename);
                strSql.Append("'");
                count++;
            }
            strSql.Append(")");
            using (MySqlConnection connection = new MySqlConnection(_conn))
            {
                connection.Open();
                MySqlCommand mycommand = new MySqlCommand(Convert.ToString(strSql), connection);
                int a = mycommand.ExecuteNonQuery();
                return a;
            }
        }

        //关键字查询
        public static List<T> SerData<T>(string key)
        {
            int count = 0;
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] propertyinfo = type.GetProperties();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + type.Name.Remove(type.Name.IndexOf("Model")) + " WHERE (");
            foreach (var c in propertyinfo)
            {
                object valuename = c.Name;
                if (count == 0)
                {
                    count++;
                    continue;
                }
                if (count > 1)
                {
                    strSql.Append(" or ");
                }
                strSql.Append(valuename);
                strSql.Append(" like ");
                strSql.Append("'%");
                strSql.Append(key);
                strSql.Append("%'");
                count++;
            }
            strSql.Append(")");
            //strSql.Append("Name like '%" + key + "%' or Advantage like '%" + key + "%' or Disadvantage like '%" + key + "%' or Country like '%" + key + "%' or Recommend_Models like '%" + key + "%')");
            strSql.Append(" and state=0");
            using (MySqlConnection connection = new MySqlConnection(_conn))
            {
                connection.Open();
                MySqlCommand mycommand = new MySqlCommand(Convert.ToString(strSql), connection);
                MySqlDataReader Dr = mycommand.ExecuteReader();
                while (Dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < propertyinfo.Length; i++)
                    {
                        for (int j = 0; j < Dr.FieldCount; j++)
                        {
                            if (propertyinfo[i].Name == Dr.GetName(j))
                            {
                                object value = Dr[j];
                                propertyinfo[i].SetValue(model, value, null);
                            }
                        }
                    }
                    list.Add(model);
                }
            }
            return list;
        }
    }
}

