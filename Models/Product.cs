using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LabExam.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        public static List<Product> GetAllProducts()
        {
            List<Product> prodLst = new List<Product>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LabExam;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Products";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Product e = new Product();
                    e.ProductId = dr.GetInt32("ProductId");
                    e.ProductName = dr.GetString("ProductName");
                    e.Rate = dr.GetDecimal("Rate");
                    e.Description = dr.GetString("Description");
                    e.CategoryName = dr.GetString("CategoryName");

                    prodLst.Add(e);

                }
                dr.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return prodLst;
        }

        public static Product GetSingleProduct(int ProdID)
        {
            Product? e = new Product();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LabExam;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Products where ProductId=@ProdID";

                cmd.Parameters.Add("@ProdID", SqlDbType.Int).Value = ProdID;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    e.ProductId = dr.GetInt32("ProductId");
                    e.ProductName = dr.GetString("ProductName");
                    e.Rate = dr.GetDecimal("Rate");
                    e.Description = dr.GetString("Description");
                    e.CategoryName = dr.GetString("CategoryName");
                }
                else
                {
                    e = null;
                }
                dr.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return e;
        }

        public static int Update(Product obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LabExam;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateProduct";

                cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@Rate", obj.Rate);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

                int Status = cmd.ExecuteNonQuery();

                if (Status > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return 0;
        }

        public static int Delete(int ProdID)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LabExam;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteProduct";

                cmd.Parameters.AddWithValue("@ProductId", ProdID);

                int Status = cmd.ExecuteNonQuery();


                if (Status > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return 0;
        }

        public static int InserProduct(Product obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LabExam;Integrated Security=True";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertProduct";

                cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@Rate", obj.Rate);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);


                int cnt = cmd.ExecuteNonQuery();

                if (cnt > 0)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return 0;
        }
    }

}
