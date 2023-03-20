namespace Demo.WebApplication.Common.Entities
{
    public class Department
    {
        #region Declare

        /// <summary>
        /// id phòng ban
        /// </summary>
        public string? department_id { get; set; }

        /// <summary>
        /// Mã của phòng ban
        /// </summary>
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// Có phải là cha không
        /// </summary>
        public bool is_parent { get; set; }

        /// <summary>
        /// Id phòng ban cha
        /// </summary>
        public string? parent_id { get; set; }

        /// <summary>
        /// Id của đơn vị
        /// </summary>
        public string? organization_id { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? created_by { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime created_date { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? modified_by { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime modified_date { get; set; } 
        #endregion
    }
}
