using Common.Models;
using Common.Ultil;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.Implements
{
    public class PhieuChiBusiness
    {
        SMSEntities dbContext = null;
        private readonly PhieuChiRepository _phieuChiRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private NhanVienBusiness _nhanVienBus;
        private readonly PhieuNhapKhoRepository _phieuNhapKhoRepo;

        public PhieuChiBusiness()
        {
            dbContext = new SMSEntities();
            _phieuChiRepo = new PhieuChiRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
            _phieuNhapKhoRepo = new PhieuNhapKhoRepository(dbContext);
        }
        public async Task Create(PhieuChiViewModel O)
        {
            DateTime today = DateTime.Now;
            PhieuChi phieuChi = new PhieuChi
            {
                //SoPhieuChi = O.soPhieuChi,
                NgayChi = O.ngayChi,
                MaNhanVien = O.maNhanVien,
                MaPhieuNhap = O.maPhieuNhap,
                TongTienChi = O.tongTienChi,
                GhiChu = O.ghiChu,
                TrangThai = true,
                NgayChinhSua = today
            };
            await _phieuChiRepo.InsertAsync(phieuChi);
        }
        public int LoadSoPhieuChi()
        {
            var soPhieuChi = from phieuchi in _phieuChiRepo.GetAll()
                             orderby phieuchi.SoPhieuChi descending
                             select phieuchi.SoPhieuChi;

            int a = _phieuChiRepo.GetAll().Count();
            if (a == 0)
            {
                return 1;
            }
            return (soPhieuChi.First() + 1);
        }
        public List<Object> LoadSoPhieuNhapKho()
        {
            var list = from pc in _phieuChiRepo.GetAll()
                       select new
                       {
                           pc.MaPhieuNhap
                       }.MaPhieuNhap;

            var pn = _phieuNhapKhoRepo.GetAll();

            var result = from n in pn
                         where !list.Contains(n.SoPhieuNhap) && n.TrangThai.Equals(true) 
                       select new SelectListItem
                       {
                           Text = n.SoPhieuNhap.ToString(),
                           Value = n.SoPhieuNhap.ToString(),
                       };

            return new List<Object>(result);
        }



        public IList<PhieuChiViewModel> SearchDanhSachPhieuChi(String key, string trangthai, DateTime tungay, DateTime denngay, string maNhanVien)
        {
            IQueryable<PhieuChi> dsPhieuChi = _phieuChiRepo.GetAll();
            List<PhieuChiViewModel> all = new List<PhieuChiViewModel>();
            List<PhieuChiViewModel> allForManager = new List<PhieuChiViewModel>();

            if (_nhanVienBus.layMaChucVu(maNhanVien) == 7)
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    all = (from phieuchi in dsPhieuChi
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.MaNhanVien.Equals(maNhanVien) && phieuchi.NgayChi >= tungay.Date && phieuchi.NgayChi <= denngay.Date)
                           select new
                           {
                               SoPhieuchi = phieuchi.SoPhieuChi,
                               NgayChi = phieuchi.NgayChi,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTienChi = phieuchi.TongTienChi,
                               GhiChu = phieuchi.GhiChu,
                               TrangThai = phieuchi.TrangThai,

                           }).AsEnumerable().Select(x => new PhieuChiViewModel()
                           {
                               soPhieuChi = x.SoPhieuchi,
                               ngayChi = x.NgayChi,
                               tenNhanVien = x.TenNhanVien,
                               tongTienChi = x.TongTienChi,
                               ghiChu = x.GhiChu,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return all;
                }

                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieuchi in dsPhieuChi
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.MaNhanVien.Equals(maNhanVien) && (
                                     phieuchi.SoPhieuChi.ToString().Contains(key)))
                           select new
                           {
                               SoPhieuchi = phieuchi.SoPhieuChi,
                               NgayChi = phieuchi.NgayChi,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTienChi = phieuchi.TongTienChi,
                               GhiChu = phieuchi.GhiChu,
                               TrangThai = phieuchi.TrangThai,
                           }).AsEnumerable().Select(x => new PhieuChiViewModel()
                           {
                               soPhieuChi = x.SoPhieuchi,
                               ngayChi = x.NgayChi,
                               tenNhanVien = x.TenNhanVien,
                               tongTienChi = x.TongTienChi,
                               ghiChu = x.GhiChu,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieuchi in dsPhieuChi
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.MaNhanVien.Equals(maNhanVien)
                           && (phieuchi.TrangThai.Equals(trangthai)))
                           select new
                           {
                               SoPhieuchi = phieuchi.SoPhieuChi,
                               NgayChi = phieuchi.NgayChi,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTienChi = phieuchi.TongTienChi,
                               GhiChu = phieuchi.GhiChu,
                               TrangThai = phieuchi.TrangThai,
                           }).AsEnumerable().Select(x => new PhieuChiViewModel()
                           {
                               soPhieuChi = x.SoPhieuchi,
                               ngayChi = x.NgayChi,
                               tenNhanVien = x.TenNhanVien,
                               tongTienChi = x.TongTienChi,
                               ghiChu = x.GhiChu,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return all;
                }

                all = (from phieuchi in dsPhieuChi
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.MaNhanVien.Equals(maNhanVien))
                       select new
                       {
                           SoPhieuchi = phieuchi.SoPhieuChi,
                           NgayChi = phieuchi.NgayChi,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TongTienChi = phieuchi.TongTienChi,
                           GhiChu = phieuchi.GhiChu,
                           TrangThai = phieuchi.TrangThai,
                       }).AsEnumerable().Select(x => new PhieuChiViewModel()
                       {
                           soPhieuChi = x.SoPhieuchi,
                           ngayChi = x.NgayChi,
                           tenNhanVien = x.TenNhanVien,
                           tongTienChi = x.TongTienChi,
                           ghiChu = x.GhiChu,
                           trangThai = x.TrangThai,
                       }).OrderByDescending(x => x.soPhieuChi).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieuchi in dsPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuchi.NgayChi >= tungay.Date && phieuchi.NgayChi <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuchi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTienChi = phieuchi.TongTienChi,
                                         GhiChu = phieuchi.GhiChu,
                                         TrangThai = phieuchi.TrangThai,
                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuchi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTienChi = x.TongTienChi,
                                         ghiChu = x.GhiChu,
                                         trangThai = x.TrangThai,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieuchi in dsPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuchi.SoPhieuChi.ToString().Contains(key))
                                     select new
                                     {
                                         SoPhieuchi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTienChi = phieuchi.TongTienChi,
                                         GhiChu = phieuchi.GhiChu,
                                         TrangThai = phieuchi.TrangThai,
                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuchi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTienChi = x.TongTienChi,
                                         ghiChu = x.GhiChu,
                                         trangThai = x.TrangThai,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }

                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieuchi in dsPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieuchi.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuchi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTienChi = phieuchi.TongTienChi,
                                         GhiChu = phieuchi.GhiChu,
                                         TrangThai = phieuchi.TrangThai,
                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuchi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTienChi = x.TongTienChi,
                                         ghiChu = x.GhiChu,
                                         trangThai = x.TrangThai,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }
                allForManager = (from phieuchi in dsPhieuChi
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieuchi.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuchi = phieuchi.SoPhieuChi,
                                     NgayChi = phieuchi.NgayChi,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TongTienChi = phieuchi.TongTienChi,
                                     GhiChu = phieuchi.GhiChu,
                                     TrangThai = phieuchi.TrangThai,
                                 }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                 {
                                     soPhieuChi = x.SoPhieuchi,
                                     ngayChi = x.NgayChi,
                                     tenNhanVien = x.TenNhanVien,
                                     tongTienChi = x.TongTienChi,
                                     ghiChu = x.GhiChu,
                                     trangThai = x.TrangThai,
                                 }).OrderByDescending(x => x.soPhieuChi).ToList();
                return allForManager;
            }
        }
        public IEnumerable<PhieuChiViewModel> thongTinPhieuChiTheoMa(int soPhieuChi)
        {
            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            List<PhieuChiViewModel> all = new List<PhieuChiViewModel>();

            all = (from phieuchi in danhSachPhieuChi
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieuchi.SoPhieuChi.Equals(soPhieuChi))
                   select new
                   {
                       SoPhieuchi = phieuchi.SoPhieuChi,
                       NgayChi = phieuchi.NgayChi,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TongTienChi = phieuchi.TongTienChi,
                       GhiChu = phieuchi.GhiChu,
                       TrangThai = phieuchi.TrangThai,
                       MaPhieuNhap = phieuchi.MaPhieuNhap,
                   }).AsEnumerable().Select(x => new PhieuChiViewModel()
                   {
                       soPhieuChi = x.SoPhieuchi,
                       ngayChi = x.NgayChi,
                       tenNhanVien = x.TenNhanVien,
                       tongTienChi = x.TongTienChi,
                       ghiChu = x.GhiChu,
                       maPhieuNhap = x.MaPhieuNhap,
                   }).ToList();
            return all;
        }
        public async Task<object> Find(int ID)
        {
            return await _phieuChiRepo.GetByIdAsync(ID);
        }
        public async Task HuyPhieuChi(object editModel)
        {
            try
            {
                DateTime today = DateTime.Now;
                PhieuChi editPhieuChi = (PhieuChi)editModel;
                editPhieuChi.TrangThai = false;
                editPhieuChi.NgayChinhSua = today;
                await _phieuChiRepo.EditAsync(editPhieuChi);
            }
            catch (Exception)
            {

            }
        }

        public IEnumerable<ThongTinHoatDongViewModel> ThongTinHoatDong()
        {
            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            List<ThongTinHoatDongViewModel> all = new List<ThongTinHoatDongViewModel>();

            all = (from phieuchi in danhSachPhieuChi
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                   orderby phieuchi.NgayChinhSua descending
                   select new
                   {
                       SoPhieuChi = phieuchi.SoPhieuChi,
                       NgayChinhSua = phieuchi.NgayChinhSua,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieuchi.TrangThai,
                       TongTienChi = phieuchi.TongTienChi,
                       MaPhieuNhap = phieuchi.MaPhieuNhap,
                   }).AsEnumerable().Select(x => new ThongTinHoatDongViewModel()
                   {
                       soPhieuChi = x.SoPhieuChi,
                       ngayChinhSuaChi = x.NgayChinhSua,
                       tenNhanVienChi = x.TenNhanVien,
                       trangThaiChi = x.TrangThai,
                       tongTienChi = x.TongTienChi,
                       maPhieuNhap = x.MaPhieuNhap,
                   }).Take(2).ToList();
            return all;
        }

        public object TongTienChi()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            var all = (from phieuchi in danhSachPhieuChi
                       where phieuchi.NgayChi.Day.Equals(ngay)
                             && phieuchi.NgayChi.Month.Equals(thang)
                             && phieuchi.NgayChi.Year.Equals(nam)
                       select new
                       {
                           TongTien = phieuchi.TongTienChi,
                       }).AsEnumerable().Select(x => new PhieuChi()
                       {
                           TongTienChi = x.TongTien,
                       }).Sum(x => x.TongTienChi);
            return all;
        }

        public object SoPhieuChi()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            var all = (from phieuchi in danhSachPhieuChi
                       where phieuchi.NgayChi.Day.Equals(ngay)
                             && phieuchi.NgayChi.Month.Equals(thang)
                             && phieuchi.NgayChi.Year.Equals(nam)
                       select new
                       {
                           SoPhieuChi = phieuchi.SoPhieuChi,
                       }).AsEnumerable().Select(x => new PhieuChi()
                       {
                           SoPhieuChi = x.SoPhieuChi,
                       }).Count();
            return all;
        }
    }
}
