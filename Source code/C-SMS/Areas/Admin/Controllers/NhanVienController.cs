using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.ViewModels;
using System.Threading.Tasks;
using Common.Models;
using System.Net;
using System.IO;
using System.Web.Helpers;
using Common.Ultil;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhanVienController : BaseController
    {
        readonly NhanVienBusiness _nhanVienKhoBus = new NhanVienBusiness();
        readonly ChucVuBusiness _chucVuKhoBus = new ChucVuBusiness();
        //
        // GET: /Admin/NhanVien/

        public ActionResult Index()
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng Hoạt Động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            return View();
        }

        [HttpGet]
        public JsonResult CheckUserName(string username, string email, string sodienthoai, string cmnd)
        {
            var isDuplicate = false;

            foreach (var user in _nhanVienKhoBus.GetAllUserName())
            {
                if (user.UserName == username)
                    isDuplicate = true;
                if (user.Email == email)
                    isDuplicate = true;
                if (user.SoDienThoai == sodienthoai)
                    isDuplicate = true;
                if (user.CMND == cmnd)
                    isDuplicate = true;
             
            }

            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult DanhSachNhanVien(string searchString, string trangthai, string chucvu , int page = 1, int pageSize = 10)
        {
            if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(chucvu))
            {
                return View(_nhanVienKhoBus.SearchDanhSachNhanVien(searchString, trangthai, chucvu).ToPagedList(page, pageSize));
            }
            if (!string.IsNullOrEmpty(trangthai))
            {
                return View(_nhanVienKhoBus.SearchDanhSachNhanVien(searchString, trangthai, chucvu).ToPagedList(page, pageSize));
            }

            return View(_nhanVienKhoBus.LoadDanhSachNhanVien().ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinNhanVien(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng Hoạt Động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();

            List<SelectListItem> admin = new List<SelectListItem>();
            admin.Add(new SelectListItem { Text = "Chủ của hàng", Value = "3" });
            ViewBag.Admin = admin;
            ViewBag.thongTinNhanVien = _nhanVienKhoBus.LoadDanhSachNhanVienTheoMa(id).ToList();
      
            return View(ViewBag.thongTinNhanVien);
        }
        public ActionResult Create()
        {
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();
            return View();
        }
      
        [HttpPost]
        public async Task<ActionResult> Create(NhanVienViewModel nhanVien, HttpPostedFileBase avatar)
        { 
            if (avatar != null && avatar.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/image/user"),
                                               Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nhanVien.avatar = avatar.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                nhanVien.avatar = "default.png";
            }

            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/template/themnhanvien.html"));

                content = content.Replace("{{TenNhanVien}}", nhanVien.tenNhanVien);
                content = content.Replace("{{Username}}", nhanVien.userName);
                content = content.Replace("{{Password}}", nhanVien.password);

                String subject = "Thông tin từ cửa hàng BK Computer!!!";
                SentMail.Sent(subject, nhanVien.email, "csms.project.fpt@gmail.com", "T12345678", content);

                await _nhanVienKhoBus.Create(nhanVien);
                SetAlert("Đã thêm nhân viên thành công!!!", "success");
            }
            catch
            {
                TempData["nhanVien"] = nhanVien;
                SetAlert("Đã xảy ra lỗi! Bạn hãy thêm lại", "error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            List<SelectListItem> trangThai = new List<SelectListItem>();
            trangThai.Add(new SelectListItem { Text = "Đang Hoạt Động", Value = "true" });
            trangThai.Add(new SelectListItem { Text = "Ngừng Hoạt Động", Value = "false" });
            ViewBag.data = trangThai;
            ViewBag.chucvu = _chucVuKhoBus.LoadChucVu();

            List<SelectListItem> admin = new List<SelectListItem>();
            admin.Add(new SelectListItem { Text = "Chủ của hàng", Value = "3" });
            ViewBag.Admin = admin;
            return View(_nhanVienKhoBus.LoadDanhSachNhanVienTheoMa(id).ToList());
        }
       
        [HttpPost]
        public async Task<ActionResult> Edit(int id, NhanVienViewModel nhanVien, HttpPostedFileBase avatar)
        {
            if (avatar != null && avatar.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/image/user"),
                                               Path.GetFileName(avatar.FileName));
                    avatar.SaveAs(path);
                    nhanVien.avatar = avatar.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
               // nhanVien.avatar = "default.png";
                nhanVien.avatar = nhanVien.checkImage;
            }

            if(id == 1)
            {
                nhanVien.trangThai = true;
                nhanVien.maChucVu = 3;
            }
             //Get nhân viên muốn update (find by ID)
            NhanVien edit = (NhanVien)await _nhanVienKhoBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Access Update from Business
                try
                {
                    await _nhanVienKhoBus.Update(nhanVien, edit);
                    SetAlert("Đã cập nhật nhân viên thành công!!!", "success");

                }
                catch
                {
                    TempData["nhanVien"] = nhanVien;
                    SetAlert("Đã xảy ra lỗi! Bạn hãy cập nhật lại", "error");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}