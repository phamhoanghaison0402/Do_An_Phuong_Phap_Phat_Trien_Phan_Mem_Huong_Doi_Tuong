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
    public class BaoCaoBanHangBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuBanHangRepository _phieuBanHangRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;

        public BaoCaoBanHangBusiness()
        {
            dbContext = new SMSEntities();
            _phieuBanHangRepo = new PhieuBanHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<BaoCaoBanHangViewModel> ListView2(string userName, DateTime dateFrom, DateTime dateTo, int page = 1, int pageSize = 5)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<BaoCaoBanHangViewModel> all = new List<BaoCaoBanHangViewModel>();
            List<BaoCaoBanHangViewModel> allForManager = new List<BaoCaoBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 4)
            {
                all = (from phieuBanHang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName))
                       select new
                       {
                           NgayBan = phieuBanHang.NgayBan,
                           SoDonHang = 0,
                           TongTien = phieuBanHang.TongTien
                       }).AsEnumerable().Select(x => new BaoCaoBanHangViewModel()
                       {
                           ngayBan = x.NgayBan,
                           soDonHang = x.SoDonHang,
                           tongTien = x.TongTien
                       }).OrderBy(x => x.ngayBan).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieuBanHang in danhSachPhieuBanHang
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuBanHang.MaNhanVien equals nhanVien.MaNhanVien
                                 select new
                                 {
                                     NgayBan = phieuBanHang.NgayBan,
                                     SoDonHang = 0,
                                     TongTien = phieuBanHang.TongTien
                                 }).AsEnumerable().Select(x => new BaoCaoBanHangViewModel()
                                 {
                                     ngayBan = x.NgayBan,
                                     soDonHang = x.SoDonHang,
                                     tongTien = x.TongTien
                                 }).OrderBy(x => x.ngayBan).ToList();
                return allForManager;
            }
        }

        public IList<BaoCaoBanHangViewModel> ListView(string userName, DateTime dateFrom, DateTime dateTo)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<BaoCaoBanHangViewModel> all = new List<BaoCaoBanHangViewModel>();
            List<BaoCaoBanHangViewModel> allForManager = new List<BaoCaoBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 4)
            {
                all = (from phieuBanHang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName))
                       select new
                       {
                           NgayBan = phieuBanHang.NgayBan,
                           SoDonHang = 0,
                           TongTien = phieuBanHang.TongTien
                       }).AsEnumerable().Select(x => new BaoCaoBanHangViewModel()
                       {
                           ngayBan = x.NgayBan,
                           soDonHang = x.SoDonHang,
                           tongTien = x.TongTien
                       }).OrderBy(x => x.ngayBan).ToList();
                return all;
            }
            else
            {
                if ((!(dateFrom == default(DateTime))) && (!(dateTo == default(DateTime))))
                {
                    allForManager = (from phieuBanHang in danhSachPhieuBanHang
                                     join nhanVien in _nhanVienRepo.GetAll()
                                     on phieuBanHang.MaNhanVien equals nhanVien.MaNhanVien
                                     where phieuBanHang.NgayBan >= dateFrom.Date && phieuBanHang.NgayBan <= dateTo.Date
                                     group phieuBanHang by phieuBanHang.NgayBan into pgroup
                                     select new
                                     {
                                         NgayBan = pgroup.Key,
                                         SoDonHang = pgroup.Count(),
                                         TongTien = pgroup.Sum(phieuBanHang => phieuBanHang.TongTien)
                                     }).AsEnumerable().Select(x => new BaoCaoBanHangViewModel()
                                     {
                                         ngayBan = x.NgayBan,
                                         soDonHang = x.SoDonHang,
                                         tongTien = x.TongTien
                                     }).OrderBy(x => x.ngayBan).ToList();
                    return allForManager;
                }
            }
            allForManager = (from phieuBanHang in danhSachPhieuBanHang
                             join nhanVien in _nhanVienRepo.GetAll()
                             on phieuBanHang.MaNhanVien equals nhanVien.MaNhanVien
                             where phieuBanHang.NgayBan.Month == DateTime.Now.Month && phieuBanHang.NgayBan.Year == DateTime.Now.Year
                             group phieuBanHang by phieuBanHang.NgayBan into pgroup
                             select new
                             {
                                 NgayBan = pgroup.Key,
                                 SoDonHang = pgroup.Count(),
                                 TongTien = pgroup.Sum(phieuBanHang => phieuBanHang.TongTien)
                             }).AsEnumerable().Select(x => new BaoCaoBanHangViewModel()
                             {
                                 ngayBan = x.NgayBan,
                                 soDonHang = x.SoDonHang,
                                 tongTien = x.TongTien
                             }).OrderBy(x => x.ngayBan).ToList();
            return allForManager;
        }
    }
}
