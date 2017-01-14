using Common.Models;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class PhieuBaoHanhBusiness
    {
        SMSEntities dbContext;

        private readonly PhieuBaoHanhRepository _phieuBaoHanhRepo;
        private readonly NhanVienRepository _nhanVienRepo;

        private NhanVienBusiness _nhanVienBus;

        public PhieuBaoHanhBusiness()
        {
            dbContext = new SMSEntities();
            _phieuBaoHanhRepo = new PhieuBaoHanhRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }
        public async Task Create(PhieuBaoHanhViewModel obj)
        {
            PhieuBaoHanh pbh = new PhieuBaoHanh
            {
                SoPhieuBaoHanh = obj.soPhieuBaoHanh,
                NgayLap = obj.ngayLap,
                NgayGiao = obj.ngayGiao,
                MaNhanVien = obj.maNhanVien,
                TenKhachHang = obj.tenKhachHang,
                SoDienThoai = obj.soDienThoai,
                GhiChu = obj.ghiChu,
                DaGiao = false,
                TrangThai = true,
                NgayChinhSua = DateTime.Now,
                Value = obj.modelName
            };
            await _phieuBaoHanhRepo.InsertAsync(pbh);
        }


        public IList<PhieuBaoHanhViewModel> SearchDanhSachPhieuDatHang(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuBaoHanh> danhSachPhieuBaoHanh = _phieuBaoHanhRepo.GetAll();
            List<PhieuBaoHanhViewModel> all = new List<PhieuBaoHanhViewModel>();
            List<PhieuBaoHanhViewModel> allForManager = new List<PhieuBaoHanhViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 6)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieuBaoHanh in danhSachPhieuBaoHanh
                           join nhanVien in _nhanVienRepo.GetAll()
                           on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                           where (nhanVien.UserName.Equals(userName) && (
                                     phieuBaoHanh.SoPhieuBaoHanh.ToString().Contains(key)
                                     ||phieuBaoHanh.TenKhachHang.Contains(key)
                                     || phieuBaoHanh.SoDienThoai.Contains(key)))
                           select new
                           {
                               SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                               NgayLap = phieuBaoHanh.NgayLap,
                               NgayGiao = phieuBaoHanh.NgayGiao,
                               TenNhanVien = nhanVien.TenNhanvien,
                               TenKhachHang = phieuBaoHanh.TenKhachHang,
                               SoDienThoai = phieuBaoHanh.SoDienThoai,
                               TrangThai = phieuBaoHanh.TrangThai,
                               DaGiao = phieuBaoHanh.DaGiao

                           }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                           {
                               soPhieuBaoHanh = x.SoPhieuBaoHanh,
                               ngayLap = x.NgayLap,
                               ngayGiao = x.NgayGiao,
                               tenNhanVien = x.TenNhanVien,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                               trangThai = x.TrangThai,
                               daGiao = x.DaGiao
                           }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieuBaoHanh in danhSachPhieuBaoHanh
                           join nhanVien in _nhanVienRepo.GetAll()
                           on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                           where (nhanVien.UserName.Equals(userName) && phieuBaoHanh.TrangThai.ToString().Equals(trangthai))
                           select new
                           {
                               SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                               NgayLap = phieuBaoHanh.NgayLap,
                               NgayGiao = phieuBaoHanh.NgayGiao,
                               TenNhanVien = nhanVien.TenNhanvien,
                               TenKhachHang = phieuBaoHanh.TenKhachHang,
                               SoDienThoai = phieuBaoHanh.SoDienThoai,
                               TrangThai = phieuBaoHanh.TrangThai,
                               DaGiao = phieuBaoHanh.DaGiao

                           }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                           {
                               soPhieuBaoHanh = x.SoPhieuBaoHanh,
                               ngayLap = x.NgayLap,
                               ngayGiao = x.NgayGiao,
                               tenNhanVien = x.TenNhanVien,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                               trangThai = x.TrangThai,
                               daGiao = x.DaGiao
                           }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                    return all;
                }

                all = (from phieuBaoHanh in danhSachPhieuBaoHanh
                       join nhanVien in _nhanVienRepo.GetAll()
                       on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                       where (nhanVien.UserName.Equals(userName) && phieuBaoHanh.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                           NgayLap = phieuBaoHanh.NgayLap,
                           NgayGiao = phieuBaoHanh.NgayGiao,
                           TenNhanVien = nhanVien.TenNhanvien,
                           TenKhachHang = phieuBaoHanh.TenKhachHang,
                           SoDienThoai = phieuBaoHanh.SoDienThoai,
                           TrangThai = phieuBaoHanh.TrangThai,
                           DaGiao = phieuBaoHanh.DaGiao

                       }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                       {
                           soPhieuBaoHanh = x.SoPhieuBaoHanh,
                           ngayLap = x.NgayLap,
                           ngayGiao = x.NgayGiao,
                           tenNhanVien = x.TenNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           trangThai = x.TrangThai,
                           daGiao = x.DaGiao
                       }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieuBaoHanh in danhSachPhieuBaoHanh
                                     join nhanVien in _nhanVienRepo.GetAll()
                                     on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                                     where (phieuBaoHanh.NgayLap >= tungay.Date && phieuBaoHanh.NgayLap <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                                         NgayLap = phieuBaoHanh.NgayLap,
                                         NgayGiao = phieuBaoHanh.NgayGiao,
                                         TenNhanVien = nhanVien.TenNhanvien,
                                         TenKhachHang = phieuBaoHanh.TenKhachHang,
                                         SoDienThoai = phieuBaoHanh.SoDienThoai,
                                         TrangThai = phieuBaoHanh.TrangThai,
                                         DaGiao = phieuBaoHanh.DaGiao

                                     }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                                     {
                                         soPhieuBaoHanh = x.SoPhieuBaoHanh,
                                         ngayLap = x.NgayLap,
                                         ngayGiao = x.NgayGiao,
                                         tenNhanVien = x.TenNhanVien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         trangThai = x.TrangThai,
                                         daGiao = x.DaGiao
                                     }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieuBaoHanh in danhSachPhieuBaoHanh
                                     join nhanVien in _nhanVienRepo.GetAll()
                                     on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                                     where (phieuBaoHanh.SoPhieuBaoHanh.ToString().Contains(key)
                                            || phieuBaoHanh.TenKhachHang.Contains(key)
                                            || phieuBaoHanh.SoDienThoai.Contains(key))
                                     select new
                                     {
                                         SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                                         NgayLap = phieuBaoHanh.NgayLap,
                                         NgayGiao = phieuBaoHanh.NgayGiao,
                                         TenNhanVien = nhanVien.TenNhanvien,
                                         TenKhachHang = phieuBaoHanh.TenKhachHang,
                                         SoDienThoai = phieuBaoHanh.SoDienThoai,
                                         TrangThai = phieuBaoHanh.TrangThai,
                                         DaGiao = phieuBaoHanh.DaGiao

                                     }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                                     {
                                         soPhieuBaoHanh = x.SoPhieuBaoHanh,
                                         ngayLap = x.NgayLap,
                                         ngayGiao = x.NgayGiao,
                                         tenNhanVien = x.TenNhanVien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         trangThai = x.TrangThai,
                                         daGiao = x.DaGiao
                                     }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieuBaoHanh in danhSachPhieuBaoHanh
                                     join nhanVien in _nhanVienRepo.GetAll()
                                     on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                                     where phieuBaoHanh.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                                         NgayLap = phieuBaoHanh.NgayLap,
                                         NgayGiao = phieuBaoHanh.NgayGiao,
                                         TenNhanVien = nhanVien.TenNhanvien,
                                         TenKhachHang = phieuBaoHanh.TenKhachHang,
                                         SoDienThoai = phieuBaoHanh.SoDienThoai,
                                         TrangThai = phieuBaoHanh.TrangThai,
                                         DaGiao = phieuBaoHanh.DaGiao

                                     }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                                     {
                                         soPhieuBaoHanh = x.SoPhieuBaoHanh,
                                         ngayLap = x.NgayLap,
                                         ngayGiao = x.NgayGiao,
                                         tenNhanVien = x.TenNhanVien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         trangThai = x.TrangThai,
                                         daGiao = x.DaGiao
                                     }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                    return allForManager;
                }

                allForManager = (from phieuBaoHanh in danhSachPhieuBaoHanh
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                                 where phieuBaoHanh.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                                     NgayLap = phieuBaoHanh.NgayLap,
                                     NgayGiao = phieuBaoHanh.NgayGiao,
                                     TenNhanVien = nhanVien.TenNhanvien,
                                     TenKhachHang = phieuBaoHanh.TenKhachHang,
                                     SoDienThoai = phieuBaoHanh.SoDienThoai,
                                     TrangThai = phieuBaoHanh.TrangThai,
                                     DaGiao = phieuBaoHanh.DaGiao

                                 }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                                 {
                                     soPhieuBaoHanh = x.SoPhieuBaoHanh,
                                     ngayLap = x.NgayLap,
                                     ngayGiao = x.NgayGiao,
                                     tenNhanVien = x.TenNhanVien,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     trangThai = x.TrangThai,
                                     daGiao = x.DaGiao
                                 }).OrderByDescending(x => x.soPhieuBaoHanh).ToList();
                return allForManager;
            }
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuBaoHanhRepo.GetByIdAsync(ID);
        }

        public async Task Delete(object deleteModel)
        {
            PhieuBaoHanh pbh = (PhieuBaoHanh)deleteModel;
            pbh.NgayChinhSua = DateTime.Now;
            pbh.TrangThai = false;

            await _phieuBaoHanhRepo.EditAsync(pbh);
        }

        public async Task Confirm(object confirmModel)
        {
            PhieuBaoHanh pbh = (PhieuBaoHanh)confirmModel;
            pbh.NgayChinhSua = DateTime.Now;
            pbh.DaGiao = true;

            await _phieuBaoHanhRepo.EditAsync(pbh);
        }

        public IEnumerable<PhieuBaoHanhViewModel> ThongTinPhieuBaoHanhTheoMa(int soPhieuBaoHanh)
        {
            IQueryable<PhieuBaoHanh> danhSachPhieuBaoHanh = _phieuBaoHanhRepo.GetAll();
            List<PhieuBaoHanhViewModel> all = new List<PhieuBaoHanhViewModel>();

            all = (from phieuBaoHanh in danhSachPhieuBaoHanh
                   join nhanVien in _nhanVienRepo.GetAll()
                   on phieuBaoHanh.MaNhanVien equals nhanVien.MaNhanVien
                   where phieuBaoHanh.SoPhieuBaoHanh.Equals(soPhieuBaoHanh)
                   select new
                   {
                       SoPhieuBaoHanh = phieuBaoHanh.SoPhieuBaoHanh,
                       NgayLap = phieuBaoHanh.NgayLap,
                       NgayGiao = phieuBaoHanh.NgayGiao,
                       TenNhanVien = nhanVien.TenNhanvien,
                       TenKhachHang = phieuBaoHanh.TenKhachHang,
                       SoDienThoai = phieuBaoHanh.SoDienThoai,
                       TongTien = phieuBaoHanh.TongTien,
                       GhiChu = phieuBaoHanh.GhiChu,
                       ModelName = phieuBaoHanh.Value,
                       DaGiao = phieuBaoHanh.DaGiao
                   }).AsEnumerable().Select(x => new PhieuBaoHanhViewModel()
                   {
                       soPhieuBaoHanh = x.SoPhieuBaoHanh,
                       ngayLap = x.NgayLap,
                       ngayGiao = x.NgayGiao,
                       tenNhanVien = x.TenNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       ghiChu = x.GhiChu,
                       modelName = x.ModelName,
                       daGiao = x.DaGiao
                   }).ToList();
            return all;
        }

        public int LoadSoPhieuBaoHanh()
        {
            int demSoPhieu = _phieuBaoHanhRepo.GetAll().Count();
            if (demSoPhieu == 0)
            {
                return 1;
            }

            var soPhieuBaoHanh = from phieuBaoHanh in _phieuBaoHanhRepo.GetAll()
                                 orderby phieuBaoHanh.SoPhieuBaoHanh descending
                                 select phieuBaoHanh.SoPhieuBaoHanh;
            return (soPhieuBaoHanh.First() + 1);
        }
    }
}
