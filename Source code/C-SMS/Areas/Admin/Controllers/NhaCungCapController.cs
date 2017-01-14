using Business.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.Models;
using System.Threading.Tasks;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NhaCungCapController : BaseController
    {
        readonly NhaCungCapBusiness _nhaCungCapBus = new NhaCungCapBusiness();
        //
        // GET: /Admin/NhaCungCap/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DanhSachNhaCungCap(string searchString, int page = 1, int pageSize = 10)
        {
            return View(_nhaCungCapBus.SearchDanhSachNhaCungCap(searchString).ToPagedList(page, pageSize));
        }

        public ActionResult ThongTinNhaCungCap(int id)
        {
            ViewBag.nhaCungCap = _nhaCungCapBus.LoadDanhSachNhaCungCapTheoMa(id).ToList();         
            return View(ViewBag.nhaCungCap);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NhaCungCap nhaCungCap)
        {
            try
            {
                await _nhaCungCapBus.Create(nhaCungCap);
                SetAlert("Đã tạo mới nhà cung cấp thành công!!!", "success");
            }
            catch
            {
                SetAlert("Đã xảy ra lỗi! Bạn hãy tạo lại", "error");

            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.nhaCungCap = _nhaCungCapBus.LoadDanhSachNhaCungCapTheoMa(id).ToList();
            return View(ViewBag.nhaCungCap);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, NhaCungCap nhaCungCap)
        {
            NhaCungCap edit = (NhaCungCap)await _nhaCungCapBus.Find(id);
            if (edit == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    await _nhaCungCapBus.Update(nhaCungCap, edit);
                    SetAlert("Đã cập nhật nhà cung cấp thành công!!!", "success");

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
