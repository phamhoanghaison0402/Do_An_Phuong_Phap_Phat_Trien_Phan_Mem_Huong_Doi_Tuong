using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        readonly LoaiHangHoaBusiness _loaiHangHoaBus = new LoaiHangHoaBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.hangHoaMoiNhat = _hangHoaBus.DanhSachHangHoaMoiNhat();
            ViewBag.hangHoaBanChayNhat = _hangHoaBus.DanhSachHangHoaBanChayNhat();
            ViewBag.hangHoaGiamGia = _hangHoaBus.DanhSachHangHoaGiamGia();
            return View();
        }

        public PartialViewResult DanhSachLoaiHangHoa()
        {
            var menuLoaiHangHoa = _loaiHangHoaBus.LoadDSLoaiHangHoa();
            return PartialView("~/Views/PartitalView/MenuManagerPartial.cshtml", menuLoaiHangHoa);
        }

        public ActionResult TimKiemSanPham(string searchString, int page = 1, int pageSize = 8)
        {
            ViewBag.TimKiemSanPham = _hangHoaBus.TimKiemHangHoa(searchString);
            ViewBag.SearchString = searchString;
            return View(_hangHoaBus.TimKiemHangHoa(searchString).ToPagedList(page, pageSize));
        }

        public JsonResult ListTenHangHoa(string q)
        {
            var data = _hangHoaBus.ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}