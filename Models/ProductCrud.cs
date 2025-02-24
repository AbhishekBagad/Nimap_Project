using Microsoft.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Nimap_Project.Models
{
    public class ProductCrud
    {
        private IConfiguration configuration;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            conn = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            cmd = new SqlCommand("select p.productId,p.productName,P.CategoryId,c.CategoryName from Products p join Category c on p.CategoryId=c.CategoryId", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product p = new Product();
                    p.ProductId = Convert.ToInt32(dr["ProductId"]);
                    p.ProductName = dr["ProductName"].ToString();
                    p.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    p.CategoryName = dr["CategoryName"].ToString();
                    list.Add(p);

                }
            }
            conn.Close();
            return list;
        }

        public Product GetProductById(int id)
        {
            Product p = new Product();
            cmd = new SqlCommand("select * from products where productid=@productid", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@productid",id);            
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    p.ProductId = Convert.ToInt32(dr["productId"]);
                    p.ProductName = dr["productName"].ToString();
                    p.CategoryId = Convert.ToInt32(dr["CategoryId"]);

                }

            }
            conn.Close();
            return p;
        }

        public int AddProduct(Product p)
        {
            int result = 0;
            string qry = "insert into Products(productname,categoryId) values(@productname,@categoryId)";
            cmd=new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@productname", p.ProductName);
            cmd.Parameters.AddWithValue("@categoryId",p.CategoryId);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public int UpdateProduct(Product p)
        {
            int result = 0;
            string qry = "update Products set productName=@productname,categoryid=@categoryid where productid=@productid";
            cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@productname",p.ProductName);
            cmd.Parameters.AddWithValue("@categoryid", p.CategoryId);
            cmd.Parameters.AddWithValue("@productid", p.ProductId);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from products where productid=@productid";
            cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@productid", id);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

    }
}
