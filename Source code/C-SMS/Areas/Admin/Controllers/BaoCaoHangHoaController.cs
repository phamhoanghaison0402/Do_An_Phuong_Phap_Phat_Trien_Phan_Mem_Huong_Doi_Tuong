using Business.Implements;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BaoCaoHangHoaController : BaseController
    {
        //
        // GET: /Admin/BaoCaoHangHoa/
        static bool _trangThai = true;

        readonly BaoCaoHangHoaBusiness _baoCaoHangHoaBUS = new BaoCaoHangHoaBusiness();


        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Đang kinh doanh" },
                                                    new { Value = "false", Text = "Ngừng kinh doanh" }},
                                              "Value", "Text");
            return View();
        }

        public ActionResult DanhSachBaoCaoHangHoa(string trangThai)
        {
            //if (trangThai != "")
            //{
            //    _trangThai = Convert.ToBoolean(trangThai);
            //}

            if (trangThai == null)
            {
                _trangThai = true;
            }
            else
            {
                _trangThai = Convert.ToBoolean(trangThai);
            }

            return View(_baoCaoHangHoaBUS.ListView(HomeController.userName, _trangThai).ToList());
        }
        
        //xét cứng trạng thái true
        public ActionResult XuatFilePDF()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoHangHoaRP.rpt")));
                rd.SetDataSource(_baoCaoHangHoaBUS.ListView(HomeController.userName, true).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "BaoCaoHangHoaRP.pdf");
            }
            catch
            {
                SetAlert("Dữ liệu không có! Bạn hãy lọc lại dữ liệu", "error");
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult XuatFileEXE()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoHangHoaRP.rpt")));
                rd.SetDataSource(_baoCaoHangHoaBUS.ListView(HomeController.userName, true).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/xls", "BaoCaoHangHoaRP.xls");
            }
            catch
            {
                SetAlert("Dữ liệu không có! Bạn hãy lọc lại dữ liệu", "error");
                return RedirectToAction("Index");
            }
           
        }
    }
}
