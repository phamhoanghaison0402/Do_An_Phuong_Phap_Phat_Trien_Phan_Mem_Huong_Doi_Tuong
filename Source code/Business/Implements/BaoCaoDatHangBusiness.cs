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
    public class BaoCaoDatHangBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuDatHangRepository _phieuDatHangRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;

        public BaoCaoDatHangBusiness() 
        {
            dbContext = new SMSEntities();
            _phieuDatHangRepo = new PhieuDatHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<BaoCaoDatHangViewModel> ListView(string nhanVienCode, DateTime dateFrom, DateTime dateTo)
        {
            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            List<BaoCaoDatHangViewModel> allForManager = new List<BaoCaoDatHangViewModel>();

            if ((!(dateFrom == default(DateTime))) && (!(dateTo == default(DateTime))))
            {
                allForManager = (from phieuDatHang in danhSachPhieuDatHang
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuDatHang.MaNhanVien equals nhanVien.MaNhanVien
                                 where phieuDatHang.NgayDat >= dateFrom.Date && phieuDatHang.NgayDat <= dateTo.Date
                                 group phieuDatHang by phieuDatHang.NgayDat into pgroup
                                 select new
                                 {
                                     NgayDat = pgroup.Key,
                                     SoDonHang = pgroup.Count(),
                                     TongTien = pgroup.Sum(phieuDatHang => phieuDatHang.TongTien)
                                 }).AsEnumerable().Select(x => new BaoCaoDatHangViewModel()
                                 {
                                     ngayDat = x.NgayDat,
                                     soDonHang = x.SoDonHang,
                                     tongTien = x.TongTien
                                 }).OrderBy(x => x.ngayDat).ToList();
                return allForManager;
            }
            else
            {
                allForManager = (from phieuDatHang in danhSachPhieuDatHang
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuDatHang.MaNhanVien equals nhanVien.MaNhanVien
                                 where phieuDatHang.NgayDat.Month == DateTime.Now.Month && phieuDatHang.NgayDat.Year == DateTime.Now.Year
                                 group phieuDatHang by phieuDatHang.NgayDat into pgroup
                                 select new
                                 {
                                     NgayDat = pgroup.Key,
                                     SoDonHang = pgroup.Count(),
                                     TongTien = pgroup.Sum(phieuDatHang => phieuDatHang.TongTien)
                                 }).AsEnumerable().Select(x => new BaoCaoDatHangViewModel()
                                 {
                                     ngayDat = x.NgayDat,
                                     soDonHang = x.SoDonHang,
                                     tongTien = x.TongTien
                                 }).OrderBy(x => x.ngayDat).ToList();
                return allForManager;
            }
        }
    }
}
