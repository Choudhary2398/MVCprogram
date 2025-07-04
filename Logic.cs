using ministore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using System.IO;

namespace ministore
{
    public class Logic
    {
        string cs = ConfigurationManager.ConnectionStrings["dbpro"].ConnectionString;

            
        public List<Listofproducts> GetAll()
        {
            List<Listofproducts> product = new List<Listofproducts>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spgetproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    product.Add(new Listofproducts()
                    {
                        productid = Convert.ToInt32(dr["productid"]),
                        productname = Convert.ToString(dr["productname"]),
                        price = Convert.ToInt32(dr["price"]),
                        category = Convert.ToString(dr["category"]),
                        images = Convert.ToString(dr["images"])
                    });
                }
            }
            return product;
        }
       
        public Listofproducts GetByproductid(int productid)
        {
            Listofproducts list = new Listofproducts();
            Listofproducts products = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spinsertproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", list.productid);
                cmd.Parameters.AddWithValue("@productname", list.productname);
                cmd.Parameters.AddWithValue("@price", list.price);
                cmd.Parameters.AddWithValue("@category", list.category);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    products = new Listofproducts
                    {
                        productid = Convert.ToInt32(dr["productid"]),
                        productname = Convert.ToString(dr["productname"]),
                        price = Convert.ToInt32(dr["price"]),
                        category = Convert.ToString(dr["category"]),
                        images = Convert.ToString(dr["images"])
                    };
                }

            }
            return products;
        }
       
        public void Insert(Listofproducts products)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("spinsertproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productname", products.productname);
                cmd.Parameters.AddWithValue("@price", products.price);
                cmd.Parameters.AddWithValue("@category", products.category);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(Listofproducts products)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spupdateproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", products.productid);
                cmd.Parameters.AddWithValue("@productname", products.productname);
                cmd.Parameters.AddWithValue("@price", products.price);
                cmd.Parameters.AddWithValue("@category", products.category);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(Listofproducts products)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spdeleteproducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", products.productid);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public string SaveImage(HttpPostedFileBase file, string serverPath, string relativePath)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            string fileName = Path.GetFileName(file.FileName); // just original file name
            string fullPath = Path.Combine(serverPath, fileName);

            file.SaveAs(fullPath);
            return Path.Combine(relativePath, fileName).Replace("\\", "/");
        }
    }
}