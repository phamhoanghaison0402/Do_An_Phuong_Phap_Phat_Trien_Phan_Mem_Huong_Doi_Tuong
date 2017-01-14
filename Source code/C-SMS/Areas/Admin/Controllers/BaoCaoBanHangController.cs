using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using WebBanHang.Reports;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BaoCaoBanHangController : BaseController
    {
        //
        // GET: /Admin/BaoCaoBanHang/

        static DateTime _dateFrom;
        static DateTime _dateTo;

        readonly BaoCaoBanHangBusiness _baoCaoBanHangBUS = new BaoCaoBanHangBusiness();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachBaoCaoBanHang(string dateFrom, string dateTo)
        {
            _dateFrom = Convert.ToDateTime(dateFrom);
            if (_dateFrom == default(DateTime))
            {
                _dateFrom = Convert.ToDateTime("1/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            }
            _dateTo = Convert.ToDateTime(dateTo);
            if (_dateTo == default(DateTime))
            {
                _dateTo = DateTime.Now;
            }
            return View(_baoCaoBanHangBUS.ListView(HomeController.userName, _dateFrom, _dateTo).ToList());
        }

        public ActionResult XuatFilePDF()
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoBanHangRP.rpt")));
                rd.SetDataSource(_baoCaoBanHangBUS.ListView(HomeController.userName, _dateFrom, _dateTo).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                rd.SetParameterValue("txtDateFrom", _dateFrom.ToString("dd/MM/yyyy"));
                rd.SetParameterValue("txtDateTo", _dateTo.ToString("dd/MM/yyyy"));
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "BaoCaoBanHangRP.pdf");
            }
            catch
            {
                SetAlert("Dữ liệu không có! Bạn hãy lọc lại dữ liệu", "error");
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult XuatFileEXE()
        {
            //GridView gv = new GridView();
            //gv.DataSource = _baoCaoBanHangBUS.ListView(HomeController.nhanVienCode, _dateFrom, _dateTo).ToList();

            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=CustomerReport.xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gv.RenderControl(htw);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();

            //return RedirectToAction("Index");
           

            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/BaoCaoBanHangRP.rpt")));
                rd.SetDataSource(_baoCaoBanHangBUS.ListView(HomeController.userName, _dateFrom, _dateTo).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                rd.SetParameterValue("txtDateFrom", _dateFrom.ToString("dd/MM/yyyy"));
                rd.SetParameterValue("txtDateTo", _dateTo.ToString("dd/MM/yyyy"));
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/xls", "BaoCaoBanHangRP.xls");
            }
            catch
            {
                SetAlert("Dữ liệu không có! Bạn hãy lọc lại dữ liệu", "error");
                return RedirectToAction("Index");
            }
        }
    }
}
