using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Common.ViewModels;
using System.Threading.Tasks;
using Common.Models;
using System.Drawing;
using System.IO;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HangHoaController : BaseController
    {
        //
        // GET: /Admin/HangHoa/

        readonly HangHoaBusiness _hangHoaKhoBus = new HangHoaBusiness();
        readonly LoaiHangHoaBusiness _loaiHangHoaKhoBus = new LoaiHangHoaBusiness();
        public ActionResult Index()
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang kinh doanh", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng kinh doanh", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            var a = _hangHoaKhoBus.GetAllModelName();
            return View();
        }

        [HttpGet]
        public JsonResult CheckModelName(string modelname)
        {
            var isDuplicate = false;

            foreach (var user in _hangHoaKhoBus.GetAllModelName())
            {
                if (user.ModelName == modelname)
                    isDuplicate = true;
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DanhSachHangHoa(string searchString, string trangthai, string loaihanghoa, int page = 1, int pageSize = 10)
        {
            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trangthai) || !string.IsNullOrEmpty(loaihanghoa))
            {
                return View(_hangHoaKhoBus.SearchDanhSachHangHoa(searchString, trangthai, loaihanghoa).ToPagedList(page, pageSize));
            }

            return View(_hangHoaKhoBus.LoadDanhSachHangHoa().ToPagedList(page, pageSize));
        }
        public ActionResult ThongTinHangHoa(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang kinh doanh", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng kinh doanh", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            ViewBag.thongTinHangHoa = _hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList();

            return View(ViewBag.thongTinHangHoa);
        }

        [HttpPost]
        public async Task<ActionResult> Create(HangHoaViewModel hangHoa, HttpPostedFileBase hinhAnh)
        {
            if (hinhAnh != null && hinhAnh.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/client/product"),
                                               Path.GetFileName(hinhAnh.FileName));
                    hinhAnh.SaveAs(path);
                    hangHoa.hinhAnh = hinhAnh.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                hangHoa.hinhAnh = "default.png";
            }

            try
            {
                await _hangHoaKhoBus.Create(hangHoa);
                SetAlert("Đã thêm sản phẩm thành công!!!", "success");
            }
            catch
            {
                TempData["hangHoa"] = hangHoa;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang kinh doanh", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng kinh doanh", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View(_hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, HangHoaViewModel HangHoa, HttpPostedFileBase hinhAnh)
        {
            if (hinhAnh != null && hinhAnh.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/client/product"),
                                               Path.GetFileName(hinhAnh.FileName));
                    hinhAnh.SaveAs(path);
                    HangHoa.hinhAnh = hinhAnh.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                HangHoa.hinhAnh = HangHoa.checkImage;
            }
            //Get nhân viên muốn update (find by ID)
            HangHoa edit = (HangHoa)await _hangHoaKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _hangHoaKhoBus.Update(HangHoa, edit);
                    SetAlert("Đã cập nhật sản phẩm thành công!!!", "success");

                }
                catch
                {
                    TempData["HangHoa"] = HangHoa;
                    SetAlert("Đã xảy ra lỗi! Bạn hãy cập nhật lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            //Get nhân viên muốn update (find by ID)
            HangHoa edit = (HangHoa)await _hangHoaKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _hangHoaKhoBus.Delete(edit);
                    SetAlert("Đã hủy sản phẩm thành công!!!", "success");

                }
                catch
                {
                    SetAlert("Đã xảy ra lỗi! Bạn hãy xóa lại", "error");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult ViewInfo(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang kinh doanh", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng kinh doanh", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.loaihanghoa = _loaiHangHoaKhoBus.LoadLoaiHangHoa();
            return View(_hangHoaKhoBus.LoadDanhSachHangHoaTheoMa(id).ToList());
        }
    }
}
