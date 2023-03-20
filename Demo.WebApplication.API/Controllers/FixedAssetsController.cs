
using Demo.WebApplication.API.lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using Dapper;
using Demo.WebApplication.Common.Entities;
using Demo.WebApplication.Common.Entities.DTO;
using Demo.WebApplication.Common;
using Demo.WebApplication.Common.Enums;
using Demo.WebApplication.DL.FixedAssetDL;
using Demo.WebApplication.BL.FixedAssetBL;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedAssetsController : ControllerBase
    {
        DBConfig db = new DBConfig();

        private IFixedAssetBL _fixedAssetBL;
        
        public FixedAssetsController(IFixedAssetBL fixedAssetBL)
        {
            _fixedAssetBL = fixedAssetBL;
        }
            
        /// <summary>
        /// Lấy danh sách Tài sản qua bộ lọc
        /// </summary>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <returns>Trạng thái, thông tin danh sách Tài sản, Tổng số bản ghi, Tổng số trang</returns>
        /// Author NNduc (12/03/2023)
        [HttpGet("Filter")]
        public IActionResult Filter(string? departmentName="",string? fixedAssetCategoryName="",string? filter = "",int? page=1,int? pageSize=10)
        {
            try
            {
                //Chuẩn bị tên stored
                var storedProcudureName = "Proc_fixed_asset_GetPaging";

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                //1. Truyền tên bảng
                parameters.Add("v_Table", "view_fixed_asset");
                //2.Truyền số trang muốn đến, vì trong data dữ liệu bắt đầu là 0
                parameters.Add("v_Offset", page - 1);
                //3. Truyền số lượng bản ghi trong trang
                parameters.Add("v_Limit", pageSize);
                //4. Truyền câu điều kiện
                parameters.Add("v_Where", string.Format("" +
                    "department_name like '%{0}%' AND fixed_asset_category_name like '%{1}%' " +
                    " AND ( fixed_asset_name like '%{2}%' OR fixed_asset_code like '%{2}%')",
                    departmentName, fixedAssetCategoryName, filter));
                //5.Truyền sắp xếp, mặc định là sắp xếp modifier 
                parameters.Add("v_Sort", "");

                //chuẩn bị kết nối
                var mySqlConnection = db.mySqlConnection;

                //Thực hiện câu lệnh sql
                var multipleResults = mySqlConnection.QueryMultiple(storedProcudureName, parameters, commandType: CommandType.StoredProcedure);
                if (multipleResults != null)
                {
                    //lấy danh sách Tài sản
                    var fixedAssets = multipleResults.Read<FixedAsset>().ToList();
                    //Lấy tổng số bản ghi
                    var totalRecord = multipleResults.Read<int>().Single();
                    //Lấy tổng số trang 
                    var totalPage = (int)(totalRecord % pageSize == 0 ? (totalRecord / pageSize) : (totalRecord / pageSize + 1));

                    return StatusCode(StatusCodes.Status200OK, new PagingResult<FixedAsset>
                    {
                        Data = fixedAssets,
                        TotalRecord = totalRecord,
                        TotalPage = totalPage
                    });
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }


        }

        /// <summary>
        /// Lấy danh sách Tài sản mặc định
        /// </summary>
        /// <returns>Trạng thái và danh sách tài sản</returns>
        [HttpGet]
        public IActionResult FixedAssets()
        {
            try
            {
                //Thực hiện câu lệnh sql
                
                List<FixedAsset> fixedAssets = _fixedAssetBL.GetFixedAssetList();

                //Xử lý kết quả trả về
                if (fixedAssets != null) return StatusCode(StatusCodes.Status200OK, fixedAssets);
                else return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
        
        /// <summary>
        /// Lấy mã Tài sản mới
        /// </summary>
        /// <returns>Trạng thái, mã Tài sản</returns>
        /// Author NNduc(12/03/2023)
        [HttpGet("NewFixedAssetCode")]
        public IActionResult NewFixedAssetCode()
        {
            try
            {
                //Chuẩn bị tên view
                var viewName = "view_fixed_asset";

                //chuẩn bị kết nối
                var mySqlConnection = db.mySqlConnection;

                //Thực hiện câu lệnh sql và lấy ra bản ghi đầu tiên
                var fixedAsset = mySqlConnection.QueryFirstOrDefault<FixedAsset>(viewName, commandType: CommandType.TableDirect);

                //Lấy mã tài sản
                string fixedAssetCode = fixedAsset.fixed_asset_code;

                //Tách chuỗi ra và lấy 2 ký tự đầu tiên
                string fixedAssetCodeText = fixedAssetCode.Substring(0, 2);

                //Lấy các chữ số cuối chuyển sang int rồi cộng thêm 1
                int fixedAssetCodeNumber = int.Parse(fixedAssetCode.Substring(2)) + 1;

                //Gán lại mã code mới
                fixedAssetCode = fixedAssetCodeText + fixedAssetCodeNumber;
                return StatusCode(200, fixedAssetCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// Thêm mới Tài sản
        /// </summary>
        /// <param name="fixedAsset">Thông tin của Object Fixed Asset </param>
        /// <returns>Trạng thái và tiêu đề</returns>
        /// Author NNduc(12/03/2023)
        [HttpPost]
        public IActionResult InsertFixedAsset( [FromBody]FixedAsset fixedAsset)
        {
            if (db.FixedAssets().Where(n=>n.fixed_asset_code==fixedAsset.fixed_asset_code).Count()!=0)
            {
                return StatusCode(400, new 
                { 
                    title = "Mã tài sản không được trùng!" 
                });
            }
            else
            {
                try
                {
                    string query = string.Format("CALL PROC_fixed_asset_Insert('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}','{14}')",
                        fixedAsset.fixed_asset_code, fixedAsset.fixed_asset_name, fixedAsset.department_id, fixedAsset.department_code, fixedAsset.department_name,
                        fixedAsset.fixed_asset_category_id, fixedAsset.fixed_asset_category_code, fixedAsset.fixed_asset_category_name,
                        fixedAsset.quantity, fixedAsset.cost, fixedAsset.life_time, fixedAsset.depreciation_rate, fixedAsset.tracked_year, fixedAsset.purchase_date, fixedAsset.production_year);
                    db.Excute(query);
                    return StatusCode(201, new
                    {
                        title = "Thêm tài sản thành công!"
                    });
                }
                catch
                {
                    return StatusCode(500, new
                    {
                        title = "Thêm tài sản không thành công!"
                    });
                }
            }
            
        }

        /// <summary>
        /// Sửa mới Tài sản
        /// </summary>
        /// <param name="fixedAsset">Thông tin của Object Fixed Asset </param>
        /// <returns>Trạng thái và tiêu đề</returns>
        /// Author NNduc(12/03/2023)
        [HttpPut("{id}")]
        public IActionResult UpdateFixedAsset(string id,[FromBody] FixedAsset fixedAsset)
        {
            //kiểm tra có đối tượng theo id có null không
            if (db.FixedAssets().SingleOrDefault(n => n.fixed_asset_id == id) == null)
            {
                return StatusCode(400, new
                {
                    title = "Không có tài sản nào để sửa!"
                });
            }  
            
            //lấy code Tài sản
            string code = db.FixedAssets().SingleOrDefault(n=>n.fixed_asset_id==id).fixed_asset_code;

            //kiểm tra nếu mã mới khác mã cũ và mã mới đã được sử dụng thì trả về
             if (code!=fixedAsset.fixed_asset_code && db.FixedAssets().Where(n => n.fixed_asset_code == fixedAsset.fixed_asset_code).Count() != 0)
            {
                return StatusCode(400, new
                {
                    title = "Mã tài sản không được trùng!"
                });
            }
            else
            {
                try
                {
                    string query = string.Format("CALL PROC_fixed_asset_Update('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13},'{14}','{15}')",
                        id,fixedAsset.fixed_asset_code, fixedAsset.fixed_asset_name, fixedAsset.department_id, fixedAsset.department_code, fixedAsset.department_name,
                        fixedAsset.fixed_asset_category_id, fixedAsset.fixed_asset_category_code, fixedAsset.fixed_asset_category_name,
                        fixedAsset.quantity, fixedAsset.cost, fixedAsset.life_time, fixedAsset.depreciation_rate, fixedAsset.tracked_year, fixedAsset.purchase_date, fixedAsset.production_year);
                    db.Excute(query);
                    return StatusCode(200, new
                    {
                        title = "Sửa tài sản thành công!"
                    });
                }
                catch
                {
                    return StatusCode(500, new
                    {
                        title = "Sửa tài sản không thành công!"
                    });
                }
            }
        }

        /// <summary>
        /// Xoá Tài sản theo mã
        /// </summary>
        /// <param name="code">Mã Tài sản</param>
        /// <returns>Trạng thái, tiêu đề</returns>
        [HttpDelete("{entityID}")]
        public IActionResult DeleteFixedAsset([FromRoute]string entityID)
        {
            if (db.FixedAssets().SingleOrDefault(n => n.fixed_asset_id == entityID) == null)
            {
                return StatusCode(400, new
                {
                    title = "Không có tài sản nào để xoá!"
                });
            }
            else
            {
                try
                {
                    string query = $"CALL PROC_fixed_asset_Delete('{entityID}')";
                    db.Excute(query);
                    return StatusCode(200, new
                    {
                        title = "Xoá Tài sản thành công!"
                    });
                }
                catch
                {
                    return StatusCode(400, new
                    {
                        title = "Xoá tài sản không thành công!"
                    });
                }
            }
        }
    }
}
