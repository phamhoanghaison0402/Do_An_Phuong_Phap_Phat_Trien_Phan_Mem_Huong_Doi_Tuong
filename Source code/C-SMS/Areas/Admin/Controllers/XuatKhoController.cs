using Business.Implements;
using Common.Ultil;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Common.Models;
using System.Globalization;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class XuatKhoController : BaseController
    {
        readonly PhieuXuatKhoBusiness _phieuXuatKhoBus = new PhieuXuatKhoBusiness();
        readonly ChiTietPhieuXuatKhoBusiness _chiTietPhieuXuatKhoBus = new ChiTietPhieuXuatKhoBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        //
        // GET: /Admin/XuatKho/

        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Hoàn thành" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                               "Value", "Text");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.userName);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.userName);
            ViewBag.soPhieuXuatKhoTuTang = _phieuXuatKhoBus.LoadSoPhieuXuatKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoa(), "Value", "Text");
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DanhSachPhieuXuatKho(string searchString, string trangthai, string dateFrom, string dateTo, int page = 1, int pageSize = 10)
        {

            if (!string.IsNullOrEmpty(searchString)
                || !string.IsNullOrEmpty(trangthai))
            {
                return View(_phieuXuatKhoBus.SearchDanhSachPhieuXuatKho(searchString, trangthai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.userName).ToPagedList(page, pageSize));
            }
            if (!string.IsNullOrEmpty(dateFrom)
                && !string.IsNullOrEmpty(dateTo))
            {
                return View(_phieuXuatKhoBus.SearchDanhSachPhieuXuatKho(searchString, trangthai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.userName).ToPagedList(page, pageSize));
            }

            return View(_phieuXuatKhoBus.DanhSachPhieuXuatKho(HomeController.userName).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuXuatKho(PhieuXuatKhoViewModel phieuXuatKho)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                await _phieuXuatKhoBus.Create(phieuXuatKho);
                status = true;
                SetAlert("Đã Lưu Phiếu Xuất Kho Thành Công!!!", "success");
            }
            else
            {
                status = false;
                SetAlert("Đã Xảy Ra Lỗi! Bạn Hãy Tạo Lại Phiếu Xuất Kho", "error");
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ThongTinPhieuXuatKho(int id)
        {
            ViewBag.chiTietPhieuXuatKho = _phieuXuatKhoBus.thongTinChiTietPhieuXuatKhoTheoMa(id).ToList();
            ViewBag.phieuXuatKho = _phieuXuatKhoBus.thongTinPhieuXuatKhoTheoMa(id).ToList();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Deletes(int id)
        {
            PhieuXuatKho deletePhieuXuatKho = (PhieuXuatKho)await _phieuXuatKhoBus.Find(id);

            if (deletePhieuXuatKho == null)
            {
                SetAlert("Đã xảy ra lỗi! Bạn hãy hủy lại", "error");
                return RedirectToAction("Edit");
            }
            else
            {
                try
                {
                    await _phieuXuatKhoBus.HuyPhieuXuatKho(deletePhieuXuatKho);
                    SetAlert("Đã hủy phiếu xuất kho thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy hủy lại", "error");
                    return RedirectToAction("Edit");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
