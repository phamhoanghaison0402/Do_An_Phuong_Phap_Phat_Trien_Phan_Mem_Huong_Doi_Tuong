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
    public class BaoCaoPhieuChiBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuChiRepository _phieuChiRepo;
        private readonly NhanVienRepository _nhanVienRepo;

        private NhanVienBusiness _nhanVienBus;
        public BaoCaoPhieuChiBusiness()
        {
            dbContext = new SMSEntities();
            _phieuChiRepo = new PhieuChiRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<BaoCaoPhieuChiViewModel> ListView(string nhanVienCode, DateTime dateFrom, DateTime dateTo)
        {
            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            List<BaoCaoPhieuChiViewModel> allForManager = new List<BaoCaoPhieuChiViewModel>();

            if ((!(dateFrom == default(DateTime))) && (!(dateTo == default(DateTime))))
            {
                allForManager = (from phieuChi in danhSachPhieuChi
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuChi.MaNhanVien equals nhanVien.MaNhanVien
                                 where phieuChi.NgayChi >= dateFrom.Date && phieuChi.NgayChi <= dateTo.Date
                                 select new
                                 {
                                     NgayChi = phieuChi.NgayChi,
                                     GhiChu = phieuChi.GhiChu,
                                     TongTienChi = phieuChi.TongTienChi
                                 }).AsEnumerable().Select(x => new BaoCaoPhieuChiViewModel()
                                 {
                                     ngayChi = x.NgayChi,
                                     ghiChu = x.GhiChu,
                                     tongTienChi = x.TongTienChi
                                 }).OrderBy(x => x.ngayChi).ToList();
                return allForManager;
            }
            else
            {
                allForManager = (from phieuChi in danhSachPhieuChi
                                 join nhanVien in _nhanVienRepo.GetAll()
                                 on phieuChi.MaNhanVien equals nhanVien.MaNhanVien
                                 where phieuChi.NgayChi.Month == DateTime.Now.Month && phieuChi.NgayChi.Year == DateTime.Now.Year
                                 select new
                                 {
                                     NgayChi = phieuChi.NgayChi,
                                     GhiChu = phieuChi.GhiChu,
                                     TongTienChi = phieuChi.TongTienChi
                                 }).AsEnumerable().Select(x => new BaoCaoPhieuChiViewModel()
                                 {
                                     ngayChi = x.NgayChi,
                                     ghiChu = x.GhiChu,
                                     tongTienChi = x.TongTienChi
                                 }).OrderBy(x => x.ngayChi).ToList();
                return allForManager;
            }
        }
    }
}
