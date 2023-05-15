using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.DL.DBConfig;
using Demo.WebApplication.DL.FixedAssetDL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.WebApplication.BL.BaseBL;
using Demo.WebApplication.DL.BaseDL;
using System.Globalization;
using Demo.WebApplication.Common.Entities.DTO.FixedAsset;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ValidationAttributeResoure = Demo.WebApplication.Common.Resources.ValidationAttribute;
using Demo.WebApplication.Common.Enums;

namespace Demo.WebApplication.BL.FixedAssetBL
{
    public class FixedAssetBL : BaseBL<FixedAsset>, IFixedAssetBL
    {
        IFixedAssetDL _fixedAssetDL;
        public FixedAssetBL(IFixedAssetDL fixedAssetDL) : base(fixedAssetDL)
        {
            _fixedAssetDL = fixedAssetDL;
        }

        /// <summary>
        /// Hàm xử lý validate không dùng chung nếu muốn validata thay đổi
        /// thì các lớp con kế thừa và thay đổi lại
        /// </summary>
        /// <param name="record">Bản ghi muốn validate</param>
        /// <returns>Danh sách chuỗi bị lỗi</returns>
        /// Author NNduc(4/4/2023)
        protected override List<string> ValidateCustom(FixedAsset record)
        {
            List<string> result = new List<string>();
            TimeSpan diffDate = (record.production_year ?? DateTime.Now).Subtract(record.purchase_date ?? DateTime.Now);
            if (diffDate.TotalDays < 0)
            {
                result.Add(ValidationAttributeResoure.FixedAsset.PurchaseProductionDate);
            }
            return result;
        }

        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="sort">Thông tin muốn sắp xếp</param>
        /// <param name="where">Muốn lấy danh sách theo điều kiện</param>
        /// <returns></returns>
        /// Author NNduc(4/4/2023)
        public MemoryStream ExportExcel(string? sort = "", string? where = "")
        {
            var data = GetPagingResult<FixedAssetResult,FixedAssetMoreInfor>(0, -1, where, sort).Data;
            var moreData = (FixedAssetMoreInfor)GetPagingResult<FixedAssetResult, FixedAssetMoreInfor>(0, -1, where, sort).MoreInfo;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH TÀI SẢN");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;
                workSheet.Row(1).Height = 20;
                workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells["A1:I1"].Merge = true;
                workSheet.Cells["A1:I1"].Value = "DANH SÁCH TÀI SẢN";
                workSheet.Cells["A1:I1"].Style.Font.Size = 16;
                workSheet.Row(3).Height = 15;
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Cells["A3:I3"].Style.Font.Size = 13;
                workSheet.Cells["A3:I3"].Style.Fill.SetBackground(System.Drawing.Color.LightGray);

                workSheet.Cells[3, 1].Value = "STT";
                workSheet.Cells[3, 2].Value = " Mã tài sản ";
                workSheet.Cells[3, 3].Value = " Tên tài sản ";
                workSheet.Cells[3, 4].Value = " Loại tài sản ";
                workSheet.Cells[3, 5].Value = " Bộ phận sử dụng ";
                workSheet.Cells[3, 6].Value = " Số lượng ";
                workSheet.Cells[3, 7].Value = " Nguyên giá ";
                workSheet.Cells[3, 8].Value = " HM/KH luỹ kế ";
                workSheet.Cells[3, 9].Value = " Giá trị còn lại ";
                int index = 4;

                foreach (var item in data)
                {
                    workSheet.Cells[index, 1].Value = (index - 3).ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", ".");
                    workSheet.Cells[index, 2].Value = item.fixed_asset_code;
                    workSheet.Cells[index, 3].Value = item.fixed_asset_name;
                    workSheet.Cells[index, 4].Value = item.fixed_asset_category_name;
                    workSheet.Cells[index, 5].Value = item.department_name;
                    workSheet.Cells[index, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[index, 6].Value = item.quantity.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", ".");
                    workSheet.Cells[index, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[index, 7].Value = item.cost.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", ".");
                    workSheet.Cells[index, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[index, 8].Value = item.accumulated_depreciation.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", "."); ;
                    workSheet.Cells[index, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[index, 9].Value = item.residual_value.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", "."); ;

                    for (var i = 1; i < 10; i++)
                    {
                        workSheet.Cells[index, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                    index++;
                }

                workSheet.Cells[$"A{index}:E{index}"].Merge = true;
                workSheet.Cells[$"A{index}:E{index}"].Value = "Tổng: ";
                workSheet.Row(index).Height = 20;
                workSheet.Row(index).Style.Font.Bold = true;
                workSheet.Row(index).Style.Font.Size = 13;

                workSheet.Cells[index, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[index, 6].Value = moreData.total_quantity.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", ".");
                workSheet.Cells[index, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[index, 7].Value = moreData.total_cost.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", "."); ;
                workSheet.Cells[index, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[index, 8].Value = moreData.total_accumulated_depreciation.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", "."); ;
                workSheet.Cells[index, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[index, 9].Value = moreData.total_residual_value.ToString("N").Replace(",", ".").Replace(".00", "").Replace(",", "."); ;

                for (var i = 1; i < 10; i++)
                {
                    workSheet.Cells[3, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    workSheet.Column(i).AutoFit();
                    workSheet.Cells[index, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                package.Save();
                // Trả về file Excel
                stream.Position = 0;
            }
            return stream;
        }

        /// <summary>
        /// Hàm xử lý validate cho xoá 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Mảng danh sách các trường lỗi</returns>
        /// Author: NNduc (4/5/2023)
        protected override List<string> ValidateDeleteRecord(string entityId)
        {
            var result = new List<string>();
            var pagingResult = _fixedAssetDL.GetPagingResult<FixedAssetResult>(where: $"fixed_asset_id = '{entityId.Trim()}' AND fixed_asset_increment_code IS NOT NULL");
            var data = pagingResult.Data;
            if (data.Count() > 0)
            {
                var fixedAsset = data.FirstOrDefault();
                result.Add(string.Format(ValidationAttributeResoure.FixedAsset.DeleteAriseIncrement, fixedAsset.fixed_asset_code, fixedAsset.fixed_asset_increment_code));
            }
            return result;
        }
        
        /// <summary>
        /// Hàm xử lý validate cho xoá nhiều bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Mảng danh sách các trường lỗi</returns>
        /// Author: NNduc (4/5/2023)
        protected override List<string> ValidateDeleteRecords(List<string> entityIds)
        {
            entityIds = TrimStringElements(entityIds);
            var result = new List<string>();
            string entityIdString = "'" + string.Join("','",entityIds) + "'";
            var pagingResult = _fixedAssetDL.GetPagingResult<FixedAsset>(where: $"fixed_asset_id IN ({entityIdString}) AND fixed_asset_increment_code IS NOT NULL");
            var data = pagingResult.Data;
            if (data.Count() > 0)
            {
               result.Add(string.Format(ValidationAttributeResoure.FixedAsset.DeleteAriseIncrements, data.Count()));
            }
            return result;
        }
    }
}
