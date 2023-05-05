

using Demo.WebApplication.Common.BaseVA;
using Demo.WebApplication.Common.Enums;
using System.ComponentModel.DataAnnotations;
using ValidationAttributeEnum = Demo.WebApplication.Common.Enums.ValidationAttribute;
using ValidationAttributeResoure = Demo.WebApplication.Common.Resources.ValidationAttribute;

namespace Demo.WebApplication.Common.Entities
{
    /// <summary>
    /// Thông tin chi tiết về Tài sản
    /// </summary>
    public class FixedAsset
    {
        #region Declare
        /// <summary>
        /// Id tài sản
        /// </summary>
        public string? fixed_asset_id { get; set; }

        /// <summary>
        ///Mã tài sản
        /// </summary>
        [BaseRequired()]
        [BaseMaxLength(ValidationAttributeEnum.FixedAsset.MaxLenghtFixedAssetCode)]
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [BaseRequired()]
        [BaseMaxLength(ValidationAttributeEnum.FixedAsset.MaxLenghtFixedAssetName)]
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Id của đơn vị
        /// </summary>
        public string? organization_id { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? organization_code { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public string? organization_name { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [BaseRequired()]
        public string? department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [BaseRequired()]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [BaseRequired()]
        public string? department_name { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [BaseRequired()]
        public string? fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [BaseRequired()]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        
        [BaseRequired()]
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [BaseRequired()]
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// nguyên giá
        /// </summary>
        [BaseRequired()]
        [BaseRange((double)ValidationAttributeEnum.FixedAsset.RangeMinCost,(double)ValidationAttributeEnum.FixedAsset.RangeMaxCost)]
        public decimal? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [BaseRequired()]
        [BaseRange(ValidationAttributeEnum.FixedAsset.RangeMinQuantity,ValidationAttributeEnum.FixedAsset.RangeMaxQuantity)]
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        [BaseRequired()]
        [BaseRange(ValidationAttributeEnum.FixedAsset.RangeMinDepreciationRate,ValidationAttributeEnum.FixedAsset.RangeMaxDeperciationRate)]
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        [BaseRequired()]
        [BaseRange(ValidationAttributeEnum.FixedAsset.RangeMinTrackedYear,ValidationAttributeEnum.FixedAsset.RangeMaxTrackedYear)]
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [BaseRequired()]
        [BaseRange(ValidationAttributeEnum.FixedAsset.RangeMinLifeTime,ValidationAttributeEnum.FixedAsset.RangeMaxLifeTime)]
        public int? life_time { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        [BaseRequired()]
        public DateTime? production_year { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool active { get; set; }

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

        /// <summary>
        /// Nguồn hình thành
        /// </summary>
        public string? budget { get; set; }
        
        #endregion
    }
}
