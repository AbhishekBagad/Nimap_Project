using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nimap_Project.Models;

namespace Nimap_Project.Services
{
    public class ProductServices : IProductServices
    {
        private readonly string connectionString;

        public ProductServices(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Product GetProductById(int id)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                {
                    conn.Open();
                    string qry = "select * from Products Where ProductId=@id";
                    using (SqlCommand cmd = new SqlCommand(qry, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product = new Product()
                                {
                                    ProductId = reader.GetInt32(0),
                                    ProductName = reader.GetString(1),
                                    CategoryId = reader.GetInt32(2)
                                    //CategoryName = reader.GetString(3)
                                };

                            }


                        }


                    }
                   
                }
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string qry = "select p.ProductId,p.ProductName,p.CategoryId,c.CategoryName from Products p Join Category c On p.CategoryId=c.CategoryId";

                using (SqlCommand cmd = new SqlCommand(qry, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                CategoryId = reader.GetInt32(2),
                                CategoryName = reader.GetString(3)
                            });

                        }
                    }
                }
            return products;
        }
        public int AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = "Insert Into Products(ProductName,CategoryId) Values(@Name,@categoryId)";
                using (SqlCommand cmd=new SqlCommand(qry, conn)) 
                {
                    cmd.Parameters.AddWithValue("@name",product.ProductName);
                    cmd.Parameters.AddWithValue("@categoryId",product.CategoryId);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = "Delete from Products Where ProductId=@id";
                using(SqlCommand cmd=new SqlCommand(qry,conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
                
            }
        }

   

        public int UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string qry = "Update Products Set ProductName=@name,CategoryId=@categoryId where ProductId=@id";
                using(SqlCommand cmd=new SqlCommand(qry, conn))
                {
                    cmd.Parameters.AddWithValue("@name", product.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@id",product.ProductId);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
