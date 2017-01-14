using Business.Implements;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class PhieuChiController : BaseController
    {
        //
        // GET: /Admin/PhieuChi/
        readonly PhieuChiBusiness _phieuChiBus = new PhieuChiBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        readonly PhieuNhapKhoBusiness _phieuNhapBus = new PhieuNhapKhoBusiness();

        public ActionResult Index()
        {
            ViewBag.trangthai = new SelectList(new[]{ new { Value = "true", Text = "Hoàn thành" },
                                                    new { Value = "false", Text = "Đã hủy" }},
                                              "Value", "Text");
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.userName);
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.userName);
            ViewBag.soPhieuChiTuTang = _phieuChiBus.LoadSoPhieuChi();
            ViewBag.maPhieuNhap = new SelectList(_phieuChiBus.LoadSoPhieuNhapKho(), "Value", "Text");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PhieuChiViewModel phieuChi)
        {
            if (ModelState.IsValid)
            {
                await _phieuChiBus.Create(phieuChi);
                SetAlert("Đã Lưu Phiếu Chi Thành Công!!!", "success");
            }
            else
            {
                SetAlert("Đã Xảy Ra Lỗi! Bạn Hãy Tạo Lại Phiếu Chi", "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult LayTongTienPhieuNhap(int id)
        {
            var result = _phieuNhapBus.LayTongTienPhieuNhap(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DanhSachPhieuChi(string searchString, string trangthai, string dateFrom, string dateTo, int page = 1, int pageSize = 10)
        {
            DateTime tungay = Convert.ToDateTime(null);
            DateTime denngay = Convert.ToDateTime(null);
            try
            {
                denngay = Convert.ToDateTime(dateFrom);
            }
            catch (Exception)
            {
            }
            try
            {
                tungay = Convert.ToDateTime(dateTo);
            }
            catch (Exception)
            {
            }
            return View(_phieuChiBus.SearchDanhSachPhieuChi(searchString, trangthai, tungay, denngay, HomeController.userName).ToPagedList(page, pageSize));
        }
        public ActionResult ThongTinPhieuChi(int id)
        {
            ViewBag.phieuChi = _phieuChiBus.thongTinPhieuChiTheoMa(id).ToList();
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
        public async Task<ActionResult> Delete(int id)
        {
            PhieuChi deletePhieuChi = (PhieuChi)await _phieuChiBus.Find(id);

            if (deletePhieuChi == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _phieuChiBus.HuyPhieuChi(deletePhieuChi);
                    SetAlert("Đã hủy phiếu chi thành công!!!", "success");
                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy hủy lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

    }
}
