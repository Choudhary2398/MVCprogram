using FirstMvcWebApps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FirstMvcWebApps
{
    public class Logic
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbArif"].ToString());

        public void AddEmployee(InsertEmployeeModel insertEmployee)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[InsertEmployee]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", insertEmployee.Empname);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}