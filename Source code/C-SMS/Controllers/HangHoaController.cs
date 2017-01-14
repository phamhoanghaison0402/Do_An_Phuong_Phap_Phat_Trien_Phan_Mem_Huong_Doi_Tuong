using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebBanHang.Controllers
{
    public class HangHoaController : Controller
    {
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();

        // GET: HangHoa
        public ActionResult Index()
        {
            ViewBag.hangHoaMoiNhat = _hangHoaBus.DanhSachHangHoaMoiNhat();
            return View();
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var a = _hangHoaBus.LoadHangHoaTheoMa(id);
            return View(a);
        }

        public ActionResult DanhSachSanPham(int id, int page = 1, int pageSize = 8)
        {
            ViewBag.tenLoaiHangHoa = _hangHoaBus.TenLoaiHangHoaTheoMaLoaiHangHoa(id);
            ViewBag.tongSanPham = _hangHoaBus.TongSanPhamTheoLoaiHang(id);
            var a = _hangHoaBus.DanhSachHangHoaTheoMaLoaiHangHoa(id).ToPagedList(page, pageSize);
            return View(a);
        }

        public ActionResult SanPhamKhuyenMai(int page = 1, int pageSize = 8)
        {
            ViewBag.tongSanPham = _hangHoaBus.TongSanPhamKhuyenMai();
            var a = _hangHoaBus.SanPhamKhuyenMai().ToPagedList(page, pageSize);
            return View(a);
        }
    }
}