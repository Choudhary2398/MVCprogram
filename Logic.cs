using second_Mvc_Web_Apps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace second_Mvc_Web_Apps
{
    public class Logic
    {
        string conString = ConfigurationManager.ConnectionStrings["dbArif"].ConnectionString;

        public List<ListofEmployees> GetAll()
        {
            List<ListofEmployees> list = new List<ListofEmployees>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM employees", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    list.Add(new ListofEmployees
                    {
                        empid = Convert.ToInt32(rdr["empid"]),
                        empname = rdr["empname"].ToString()
                    });
                }
            }

            return list;
        }

        public ListofEmployees GetByempid(int empid)
        {
            ListofEmployees list = new ListofEmployees();
            ListofEmployees employees = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("select * from employees where empid = @empid", con);
                cmd.Parameters.AddWithValue("@empid", list.empid);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    employees = new ListofEmployees
                    {
                        empid = Convert.ToInt32(rdr["id"]),
                        empname = rdr["name"].ToString()
                    };
                }
            }
            return employees;
        }

        public void Insert(ListofEmployees employees)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("insert into employees (empname) values ( @empname)", con);

                cmd.Parameters.AddWithValue("@empname", employees.empname);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ListofEmployees employees)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE employees SET empname = @empname where empid = @empid", con);
                    cmd.Parameters.AddWithValue("@empid", employees.empid);
                    cmd.Parameters.AddWithValue("@empname", employees.empname);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from employees where empid = @empid", con);
                cmd.Parameters.AddWithValue("@empid", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}