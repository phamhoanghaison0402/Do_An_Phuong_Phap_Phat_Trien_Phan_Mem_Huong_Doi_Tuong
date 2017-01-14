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

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhapKhoController : BaseController
    {
        readonly PhieuNhapKhoBusiness _phieuNhapKhoBus = new PhieuNhapKhoBusiness();
        readonly ChiTietPhieuNhapKhoBusiness _chiTietPhieuNhapKhoBus = new ChiTietPhieuNhapKhoBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        readonly NhaCungCapBusiness _nhanCungCapBus = new NhaCungCapBusiness();
        //
        // GET: /Admin/NhapKho/

        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Hoàn thành" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                               "Value", "Text");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.nhaCungCap = _nhanCungCapBus.LoadNhaCungCap();
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.userName);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.userName);
            ViewBag.soPhieuNhapKhoTuTang = _phieuNhapKhoBus.LoadSoPhieuNhapKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoaKho(), "Value", "Text");
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult LoadThongTinHangHoa(int id)
        {
            var result = _hangHoaBus.LayThongTinHangHoa(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult DanhSachPhieuNhapKho(string searchString, string trangthai, string dateFrom, string dateTo, int page = 1, int pageSize = 10)
        {
            return View(_phieuNhapKhoBus.SearchDanhSachPhieuNhapKho(searchString, trangthai, Convert.ToDateTime(dateFrom), Convert.ToDateTime(dateTo), HomeController.userName).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public async Task<JsonResult> LuuPhieuNhapKho(PhieuNhapKhoViewModel phieuNhapKho)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                await _phieuNhapKhoBus.Create(phieuNhapKho);
                status = true;
                SetAlert("Đã Lưu Phiếu Nhập Kho Thành Công!!!", "success");
            }
            else
            {
                status = false;
                SetAlert("Đã Xảy Ra Lỗi! Bạn Hãy Tạo Lại Phiếu Nhập Kho", "error");
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Deletes(int id)
        {
            PhieuNhap deletePhieuNhapKho = (PhieuNhap)await _phieuNhapKhoBus.Find(id);

            if (deletePhieuNhapKho == null)
            {
                SetAlert("Đã xảy ra lỗi! Bạn hãy hủy lại", "error");
                return RedirectToAction("Edit");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        await _phieuNhapKhoBus.HuyPhieuNhapKho(deletePhieuNhapKho);
                        SetAlert("Đã hủy phiếu nhập kho thành công!!!", "success");
                    }
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy hủy lại", "error");
                    return RedirectToAction("Edit");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThongTinPhieuNhapKho(int id)
        {
            ViewBag.chiTietPhieuNhapKho = _phieuNhapKhoBus.thongTinChiTietPhieuNhapKhoTheoMa(id).ToList();
            ViewBag.phieuNhapKho = _phieuNhapKhoBus.thongTinPhieuNhapKhoTheoMa(id).ToList();
            return View();
        }
    }
}
