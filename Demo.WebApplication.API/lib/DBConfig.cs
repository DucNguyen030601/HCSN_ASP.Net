using Demo.WebApplication.API;
using Demo.WebApplication.Common.Entities;
using MySql.Data.MySqlClient;
namespace Demo.WebApplication.API.lib
{
    public class DBConfig
    {
        #region Field
        public MySqlConnection mySqlConnection;
        private static readonly string server = "localhost";
        private static readonly string database = "misa.qlts_mf1566_nnduc";
        private static readonly string user = "root";
        private static readonly string pass = "";
        #endregion

        #region Constructor
        public DBConfig()
        {
            mySqlConnection = new MySqlConnection($"server={server};database={database};uid={user};pwd={pass};Old Guids=true;");

        }
        #endregion
     
        #region Get Data From Table
        /// <summary>
        /// Hàm lấy dữ liệu trong bảng fixed_asset
        /// </summary>
        /// <param name="query">Câu truy vấn</param>
        /// <returns>trả về dữ liệu phù hợp thông qua câu truy vấn</returns>
        /// Author NNduc (11/3/2023)
        public List<FixedAsset> FixedAssets()
        {
            string query = "SELECT * FROM view_fixed_asset vfa";
            List<FixedAsset> data = new List<FixedAsset>();
            using (MySqlConnection connection = mySqlConnection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new FixedAsset()
                    {
                        fixed_asset_id = reader.GetString(0),
                        fixed_asset_code = reader.GetString(1),
                        fixed_asset_name = reader.GetString(2),
                        department_id = reader.GetString(6),
                        department_code = reader.GetString(7),
                        department_name = reader.GetString(8),
                        fixed_asset_category_id = reader.GetString(9),
                        fixed_asset_category_code=reader.GetString(10),
                        fixed_asset_category_name = reader.GetString(11),
                        purchase_date = reader.GetDateTime(12),
                        cost = reader.GetDecimal(13),
                        quantity = reader.GetInt32(14),
                        depreciation_rate = reader.GetFloat(15),
                        tracked_year = reader.GetInt32(16),
                        life_time = reader.GetInt32(17),
                        production_year = reader.GetDateTime(18),
                        created_date = reader.GetDateTime(19),
                        modified_date = reader.GetDateTime(20),
                        active = reader.GetBoolean(21)
                    });
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Hàm lấy dữ liệu trong bảng department
        /// </summary>
        /// <param name="query">Câu truy vấn</param>
        /// <returns>trả về dữ liệu phù hợp thông qua câu truy vấn</returns>
        /// Author NNduc (11/3/2023)
        public List<Department> Departments(string query)
        {
            List<Department> data = new List<Department>();
            using (MySqlConnection connection = mySqlConnection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Department()
                    {
                        department_id = reader.GetString(0),
                        department_code = reader.GetString(1),
                        department_name = reader.GetString(2),
                    });
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// Hàm lấy dữ liệu trong bảng fixed_asset_category
        /// </summary>
        /// <param name="query">Câu truy vấn</param>
        /// <returns>trả về dữ liệu phù hợp thông qua câu truy vấn</returns>
        /// Author NNduc (11/3/2023)
        public List<FixedAssetCategory> FixedAssetCategores(string query)
        {
            List<FixedAssetCategory> data = new List<FixedAssetCategory>();
            using (MySqlConnection connection = mySqlConnection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new FixedAssetCategory()
                    {
                        fixed_asset_category_id = reader.GetString(0),
                        fixed_asset_category_code = reader.GetString(1),
                        fixed_asset_category_name = reader.GetString(2),
                        depreciation_rate = reader.GetFloat(4),
                        life_time = reader.GetInt32(5)
                    }); 
                }
                connection.Close();
            }
            return data;
        }
        #endregion

        #region Excute Query
        /// <summary>
        /// Thực thi câu lệnh truy vấn
        /// </summary>
        /// <param name="query">truy vấn</param>
        public void Excute(string query)
        {
            using (MySqlConnection connection = mySqlConnection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        #endregion

    }
}
