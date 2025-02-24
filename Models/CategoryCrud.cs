using Microsoft.Data.SqlClient;

namespace Nimap_Project.Models
{
    public class CategoryCrud
    {

        private IConfiguration configuration;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public CategoryCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            conn = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
       
        
        public IEnumerable<Category> GetCategory()
        {
            List<Category> list = new List<Category>();
            cmd=new SqlCommand("select * from Category",conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category cat=new Category();
                    cat.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    cat.CategoryName = Convert.ToString(dr["CategoryName"]);
                    list.Add(cat);
                }
            }
            conn.Close();

            return list;
        }

        public Category GetCategoryById(int id)
        {
            Category cat = new Category();
            cmd=new SqlCommand("select *  from Category where categoryid=@categoryid",conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@categoryid", id);
            dr=cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cat.CategoryId = Convert.ToInt32(dr["categoryid"]);
                    cat.CategoryName = dr["categoryname"].ToString();
                   
                }
            }
            conn.Close();
            return cat;
   
        }

        public int AddCategory(Category cat)
        {
            int result = 0;
            string qry="insert into Category(categoryname) values(@categoryname)";
            cmd= new SqlCommand(qry,conn);
            cmd.Parameters.AddWithValue("@categoryname",cat.CategoryName);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public int updateCategory(Category cat)
        {
            int result = 0;
            string qry = "update Category set categoryname=@categoryname where categoryid=@categoryid";
            cmd=new SqlCommand(qry,conn);
            cmd.Parameters.AddWithValue("@categoryname", cat.CategoryName);
            cmd.Parameters.AddWithValue("@categoryid",cat.CategoryId);
            conn.Open() ;
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }

        public int deleteCategory(int id)
        {
            int result = 0;
            string qry = ("delete from products where categoryid=@categoryid; delete from category where categoryid=@categoryid");
            cmd =new SqlCommand(qry,conn);
            cmd.Parameters.AddWithValue("@categoryid", id);
            conn.Open();
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}
