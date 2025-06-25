using second_Mvc_Web_Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace second_Mvc_Web_Apps
{
    public class Registrationlogic
    {

        public bool check_loginis_valid(RegisterModel registerModel)
        {
            string conString = ConfigurationManager.ConnectionStrings["dbArif"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_loginuser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                
                cmd.Parameters.AddWithValue("@email", registerModel.email);
                cmd.Parameters.AddWithValue("@password", registerModel.password);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void Registration_page(RegisterModel registerModel)
        {
            string conString = ConfigurationManager.ConnectionStrings["dbArif"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_insertregisterdetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fullname", registerModel.fullname);
                cmd.Parameters.AddWithValue("@email", registerModel.email);
                cmd.Parameters.AddWithValue("@password", registerModel.password);
                cmd.Parameters.AddWithValue("@regdate", registerModel.regdate);
                cmd.Parameters.AddWithValue("@countryid", registerModel.countryid);
         
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
           
    }
}