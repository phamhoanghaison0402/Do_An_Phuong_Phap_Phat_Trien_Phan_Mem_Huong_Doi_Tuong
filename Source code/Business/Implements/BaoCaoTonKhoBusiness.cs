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
    public class BaoCaoTonKhoBusiness
    {
        SMSEntities dbContext;
        private readonly BaoCaoTonKhoRepository _baoCaoTonKhoRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;

        public BaoCaoTonKhoBusiness()
        {
            dbContext = new SMSEntities();
            _baoCaoTonKhoRepo = new BaoCaoTonKhoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<BaoCaoTonKhoViewModel> ListView(string nhanVienCode, int month, int year)
        {
            IQueryable<BaoCaoTonKho> danhSachBaoCaoTonKho = _baoCaoTonKhoRepo.GetAll();
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<BaoCaoTonKhoViewModel> allForManager = new List<BaoCaoTonKhoViewModel>();

            if (month == DateTime.Now.Month && year == DateTime.Now.Year)
            {
                allForManager = (from baoCaoTonKho in danhSachBaoCaoTonKho
                                 join hangHoa in danhSachHangHoa
                                 on baoCaoTonKho.MaHangHoa equals hangHoa.MaHangHoa
                                 where baoCaoTonKho.Thang == DateTime.Now.Month && baoCaoTonKho.Nam == DateTime.Now.Year
                                 select new
                                 {
                                     MaBaoCaoTonKho = baoCaoTonKho.MaBaoCaoTonKho,
                                     Thang = baoCaoTonKho.Thang,
                                     Nam = baoCaoTonKho.Nam,
                                     MaHangHoa = baoCaoTonKho.MaHangHoa,
                                     TenHangHoa = hangHoa.TenHangHoa,
                                     DonViTinh = hangHoa.DonViTinh,
                                     SoLuongTonDau = baoCaoTonKho.SoLuongTonDau,
                                     SoLuongNhap = baoCaoTonKho.SoLuongNhap,
                                     SoLuongXuat = baoCaoTonKho.SoLuongXuat,
                                     SoLuongTonCuoi = baoCaoTonKho.SoLuongTonCuoi

                                 }).AsEnumerable().Select(x => new BaoCaoTonKhoViewModel()
                                 {
                                     maBaoCaoTonKho = x.MaBaoCaoTonKho,
                                     thang = x.Thang,
                                     nam = x.Nam,
                                     maHangHoa = x.MaHangHoa,
                                     tenHangHoa = x.TenHangHoa,
                                     donViTinh = x.DonViTinh,
                                     soLuongTonDau = x.SoLuongTonDau,
                                     soLuongNhap = x.SoLuongNhap,
                                     soLuongXuat = x.SoLuongXuat,
                                     soLuongTonCuoi = x.SoLuongTonCuoi
                                 }).OrderBy(x => x.maBaoCaoTonKho).ToList();
                return allForManager;
            }
            else
            {
                allForManager = (from baoCaoTonKho in danhSachBaoCaoTonKho
                                 join hangHoa in danhSachHangHoa
                                 on baoCaoTonKho.MaHangHoa equals hangHoa.MaHangHoa
                                 where baoCaoTonKho.Thang == month && baoCaoTonKho.Nam == year
                                 select new
                                 {
                                     MaBaoCaoTonKho = baoCaoTonKho.MaBaoCaoTonKho,
                                     Thang = baoCaoTonKho.Thang,
                                     Nam = baoCaoTonKho.Nam,
                                     MaHangHoa = baoCaoTonKho.MaHangHoa,
                                     TenHangHoa = hangHoa.TenHangHoa,
                                     DonViTinh = hangHoa.DonViTinh,
                                     SoLuongTonDau = baoCaoTonKho.SoLuongTonDau,
                                     SoLuongNhap = baoCaoTonKho.SoLuongNhap,
                                     SoLuongXuat = baoCaoTonKho.SoLuongXuat,
                                     SoLuongTonCuoi = baoCaoTonKho.SoLuongTonCuoi

                                 }).AsEnumerable().Select(x => new BaoCaoTonKhoViewModel()
                                 {
                                     maBaoCaoTonKho = x.MaBaoCaoTonKho,
                                     thang = x.Thang,
                                     nam = x.Nam,
                                     maHangHoa = x.MaHangHoa,
                                     tenHangHoa = x.TenHangHoa,
                                     donViTinh = x.DonViTinh,
                                     soLuongTonDau = x.SoLuongTonDau,
                                     soLuongNhap = x.SoLuongNhap,
                                     soLuongXuat = x.SoLuongXuat,
                                     soLuongTonCuoi = x.SoLuongTonCuoi
                                 }).OrderBy(x => x.maBaoCaoTonKho).ToList();
                return allForManager;
            }
        }
    }
}
