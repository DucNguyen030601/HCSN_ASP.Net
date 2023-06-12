using Demo.WebApplication.Common.BaseVA;
using ValidationAttributeEnum = Demo.WebApplication.Common.Enums.ValidationAttribute;

namespace Demo.WebApplication.Common.Entities
{
    public class FixedAssetIncrement
    {
        /// <summary>
        /// Id ghi tăng tài sản
        /// </summary>
        public string? fixed_asset_increment_id {get;set;}

        /// <summary>
        /// Mã ghi tăng tài sản
        /// </summary>
        [BaseRequired()]
        [BaseMaxLength(ValidationAttributeEnum.FixedAssetIncrement.MaxLengthFixedAssetIncrementCode)]
        public string? fixed_asset_increment_code {get;set;}

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        [BaseRequired()]
        public DateTime increment_date {get;set;}

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [BaseRequired()]
        public DateTime production_year { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        [BaseMaxLength(ValidationAttributeEnum.FixedAssetIncrement.MaxLengthDescription)]
        public string? description {get;set;}

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
    }
}
