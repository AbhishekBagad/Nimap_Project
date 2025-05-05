

using Microsoft.Data.SqlClient;
using Nimap_Project.Models;

namespace Nimap_Project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string connectionString;

        public CategoryService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public IEnumerable<Category> GetCategories()
        {
            var categories=new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select * from Category";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }

                    }
                }
            }
            return categories;
        }


        Category ICategoryService.GetCategoryById(int id)
        {
            Category category = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select * from Category Where CategoryId=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            };

                        }
                    }

                }
                
            }
            return category;
        }
        public int AddCategory(Category category)
        {
            using SqlConnection conn=new SqlConnection(connectionString);
            {
                conn.Open();
                string querry = "Insert Into Category(CategoryName) Values(@name)";
                using (SqlCommand cmd = new SqlCommand(querry, conn))
                {
                    cmd.Parameters.AddWithValue("@name", category.CategoryName);
                   return cmd.ExecuteNonQuery();

                }
            }
        }
        public int UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Update Category Set CategoryName=@name where CategoryId=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", category.CategoryName);
                    cmd.Parameters.AddWithValue("@Id",category.CategoryId);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Delete From Category Where CategoryId=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
                string catqry = "Delete From Category where CategoryId=@id";
                using (SqlCommand cmd=new SqlCommand(catqry, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal dynamic GetCategory()
        {
            throw new NotImplementedException();
        }
    }
}
