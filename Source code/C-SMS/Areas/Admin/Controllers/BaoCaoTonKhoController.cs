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
    public class BaoCaoTonKhoController : BaseController
    {
        //
        // GET: /Admin/BaoCaoTonKho/
        static int _thang, _nam;

        readonly BaoCaoTonKhoBusiness _baoCaoTonKhoBUS = new BaoCaoTonKhoBusiness();

        public ActionResult Index()
        {
            ViewBag.Thang = new SelectList(new[]{ new { Value = "1", Text = "Tháng 1" },
                                                  new { Value = "2", Text = "Tháng 2" },
                                                  new { Value = "3", Text = "Tháng 3" },
                                                  new { Value = "4", Text = "Tháng 4" },
                                                  new { Value = "5", Text = "Tháng 5" },
                                                  new { Value = "6", Text = "Tháng 6" },
                                                  new { Value = "7", Text = "Tháng 7" },
                                                  new { Value = "8", Text = "Tháng 8" },
                                                  new { Value = "9", Text = "Tháng 9" },
                                                  new { Value = "10", Text = "Tháng 10" },
                                                  new { Value = "11", Text = "Tháng 11" },
                                                  new { Value = "12", Text = "Tháng 12" },},
                                              "Value", "Text");
            ViewBag.Nam = new SelectList(new[]{ new { Value = "2016", Text = "2016" },
                                                  new { Value = "2017", Text = "2017" },
                                                  new { Value = "2018", Text = "2018" },
                                                  new { Value = "2019", Text = "2019" },
                                                  new { Value = "2020", Text = "2020" },
                                                 },
                                             "Value", "Text");
            return View();
        }

        public ActionResult DanhSachBaoCaoTonKho(string thang, string nam)
        {
            if (thang != "")
            {
                _thang = Convert.ToInt32(thang);
            }
            if (nam != "")
            {
                _nam = Convert.ToInt32(nam);
            }
            return View(_baoCaoTonKhoBUS.ListView(HomeController.userName, _thang, _nam).ToList());
        }

        public ActionResult XuatFilePDF()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoTonKhoRP.rpt")));
                rd.SetDataSource(_baoCaoTonKhoBUS.ListView(HomeController.userName, _thang, _nam).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "BaoCaoTonKhoRP.pdf");
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
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoTonKhoRP.rpt")));
                rd.SetDataSource(_baoCaoTonKhoBUS.ListView(HomeController.userName, _thang, _nam).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/xls", "BaoCaoTonKhoRP.xls");
            }
            catch
            {
                SetAlert("Dữ liệu không có! Bạn hãy lọc lại dữ liệu", "error");
                return RedirectToAction("Index");
            }
           
        }
    }
}
