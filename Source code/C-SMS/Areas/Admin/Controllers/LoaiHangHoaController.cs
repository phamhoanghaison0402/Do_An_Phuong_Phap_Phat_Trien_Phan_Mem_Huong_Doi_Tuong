using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using Common.ViewModels;
using Common.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class LoaiHangHoaController : BaseController
    {
        readonly LoaiHangHoaBusiness _loaiHangHoaBus = new LoaiHangHoaBusiness();
        //
        // GET: /Admin/LoaiHangHoa/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(LoaiHangHoa loaiHangHoa)
        {                      
            try
            {
                await _loaiHangHoaBus.Create(loaiHangHoa);
                SetAlert("Đã thêm loại sản phẩm thành công!!!", "success");
            }
            catch
            {
                TempData["loaiHangHoa"] = loaiHangHoa;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DanhSachLoaiHangHoa(string searchString, int page = 1, int pageSize = 10)
        {
            return View(_loaiHangHoaBus.LoadDanhSachLoaiHangHoa(searchString).ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinLoaiHangHoa(int id)
        {
            ViewBag.loaiHangHoa = _loaiHangHoaBus.LoadDanhSachLoaiHangHoaTheoMa(id).ToList();
            return View(ViewBag.loaiHangHoa);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.loaiHangHoa = _loaiHangHoaBus.LoadDanhSachLoaiHangHoaTheoMa(id).ToList();
            return View(ViewBag.loaiHangHoa);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, LoaiHangHoa loaiHangHoa)
        {
            LoaiHangHoa edit = (LoaiHangHoa)await _loaiHangHoaBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _loaiHangHoaBus.Update(loaiHangHoa, edit);
                    SetAlert("Đã cập nhật loại sản phẩm thành công!!!", "success");

                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy cập nhật lại", "error");
                    return RedirectToAction("Edit");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
