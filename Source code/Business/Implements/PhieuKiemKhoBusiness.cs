using Common.Models;
using Common.Ultil;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class PhieuKiemKhoBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuKiemKhoRepository _phieuKiemKhoRepo;
        private readonly ChiTietPhieuKiemKhoRepository _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private NhanVienBusiness _nhanVienBus;

        public PhieuKiemKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuKiemKhoRepo = new PhieuKiemKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _chiTietPhieuKiemKhoRepo = new ChiTietPhieuKiemKhoRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public IList<KiemKhoViewModel> SearchDanhSachPhieuKiemKho(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<KiemKhoViewModel> all = new List<KiemKhoViewModel>();
            List<KiemKhoViewModel> allForManager = new List<KiemKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 5)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieukiemkho in danhSachPhieuKiemKho
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && (
                                     phieukiemkho.SoPhieuKiemKho.ToString().Contains(key)
                                  || phieukiemkho.TrangThai.ToString().Equals(trangthai)))
                           select new
                           {
                               SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                               NgayKiemKho = phieukiemkho.NgayKiemKho,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieukiemkho.TrangThai,
                               ChuThich = phieukiemkho.GhiChu,

                           }).AsEnumerable().Select(x => new KiemKhoViewModel()
                           {
                               soPhieuKiemKho = x.SoPhieuKiemKho,
                               ngayKiemKho = x.NgayKiemKho,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieukiemkho in danhSachPhieuKiemKho
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && phieukiemkho.TrangThai.ToString().Equals(trangthai))
                           select new
                           {
                               SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                               NgayKiemKho = phieukiemkho.NgayKiemKho,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieukiemkho.TrangThai,
                               ChuThich = phieukiemkho.GhiChu,

                           }).AsEnumerable().Select(x => new KiemKhoViewModel()
                           {
                               soPhieuKiemKho = x.SoPhieuKiemKho,
                               ngayKiemKho = x.NgayKiemKho,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                    return all;
                }

                all = (from phieukiemkho in danhSachPhieuKiemKho
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName) && phieukiemkho.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                           NgayKiemKho = phieukiemkho.NgayKiemKho,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieukiemkho.TrangThai,
                           ChuThich = phieukiemkho.GhiChu,

                       }).AsEnumerable().Select(x => new KiemKhoViewModel()
                       {
                           soPhieuKiemKho = x.SoPhieuKiemKho,
                           ngayKiemKho = x.NgayKiemKho,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                       }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieukiemkho.NgayKiemKho >= tungay.Date && phieukiemkho.NgayKiemKho <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                         NgayKiemKho = phieukiemkho.NgayKiemKho,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieukiemkho.TrangThai,
                                         ChuThich = phieukiemkho.GhiChu,

                                     }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                     {
                                         soPhieuKiemKho = x.SoPhieuKiemKho,
                                         ngayKiemKho = x.NgayKiemKho,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieukiemkho.SoPhieuKiemKho.ToString().Contains(key))
                                     select new
                                     {
                                         SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                         NgayKiemKho = phieukiemkho.NgayKiemKho,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieukiemkho.TrangThai,
                                         ChuThich = phieukiemkho.GhiChu,

                                     }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                     {
                                         soPhieuKiemKho = x.SoPhieuKiemKho,
                                         ngayKiemKho = x.NgayKiemKho,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieukiemkho.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                         NgayKiemKho = phieukiemkho.NgayKiemKho,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieukiemkho.TrangThai,
                                         ChuThich = phieukiemkho.GhiChu,

                                     }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                     {
                                         soPhieuKiemKho = x.SoPhieuKiemKho,
                                         ngayKiemKho = x.NgayKiemKho,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                    return allForManager;
                }

                allForManager = (from phieukiemkho in danhSachPhieuKiemKho
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieukiemkho.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                                     NgayKiemKho = phieukiemkho.NgayKiemKho,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieukiemkho.TrangThai,
                                     ChuThich = phieukiemkho.GhiChu,

                                 }).AsEnumerable().Select(x => new KiemKhoViewModel()
                                 {
                                     soPhieuKiemKho = x.SoPhieuKiemKho,
                                     ngayKiemKho = x.NgayKiemKho,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                 }).OrderByDescending(x => x.soPhieuKiemKho).ToList();
                return allForManager;
            }
        }

        public int LoadSoPhieuKiemKho()
        {
            var soPhieuKiemKho = from phieukiemkho in _phieuKiemKhoRepo.GetAll()
                                 orderby phieukiemkho.SoPhieuKiemKho descending
                                 select phieukiemkho.SoPhieuKiemKho;

            int demSoPhieu = _phieuKiemKhoRepo.GetAll().Count();
            if (demSoPhieu == 0)
            {
                return 1;
            }
            return (soPhieuKiemKho.First() + 1);
        }

        public async Task Create(KiemKhoViewModel O)
        {
            PhieuKiemKho order = new PhieuKiemKho
            {
                SoPhieuKiemKho = O.soPhieuKiemKho,
                NgayKiemKho = O.ngayKiemKho,
                MaNhanVien = O.maNhanVien,
                TrangThai = true,
                GhiChu = O.ghiChu,       
                NgayChinhSua = DateTime.Now,
            };
            foreach (var i in O.chiTietPhieuKiemKho)
            {
                order.ChiTietPhieuKiemKhos.Add(i);
            }
            await _phieuKiemKhoRepo.InsertAsync(order);
        }

        public IEnumerable<KiemKhoViewModel> thongTinChiTietPhieuKiemKhoTheoMa(int soPhieuKiemKho)
        {
            IQueryable<ChiTietPhieuKiemKho> dsPhieuKiemKho = _chiTietPhieuKiemKhoRepo.GetAll();

            var all = (from chitietphieukiemkho in dsPhieuKiemKho
                       join hanghoa in _hangHoaRepo.GetAll()
                       on chitietphieukiemkho.MaHangHoa equals hanghoa.MaHangHoa
                       select new
                       {
                           SoPhieuKiemKho = chitietphieukiemkho.SoPhieuKiemKho,
                           MaHangHoa = hanghoa.MaHangHoa,
                           SoLuongHienTai = chitietphieukiemkho.SoLuongHienTai,
                           SoLuongKiemTra = chitietphieukiemkho.SoLuongKiemTra,
                           TenHangHoa = hanghoa.TenHangHoa,
                           DonViTinh = hanghoa.DonViTinh,

                       }).AsEnumerable().Select(x => new KiemKhoViewModel()
                    {
                        soPhieuKiemKho = x.SoPhieuKiemKho,
                        maHangHoa = x.MaHangHoa,
                        soLuongHienTai = x.SoLuongHienTai,
                        soLuongKiemTra = x.SoLuongKiemTra,
                        tenHangHoa = x.TenHangHoa,
                        donViTinh = x.DonViTinh,
                    }).ToList();

            var information = (from i in all
                               where (i.soPhieuKiemKho == soPhieuKiemKho)
                               select i).ToList();
            return information.ToList();
        }

        public IEnumerable<KiemKhoViewModel> thongTinPhieuKiemKhoTheoMa(int soPhieuKiemKho)
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<KiemKhoViewModel> all = new List<KiemKhoViewModel>();

            all = (from phieukiemkho in danhSachPhieuKiemKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieukiemkho.SoPhieuKiemKho.Equals(soPhieuKiemKho))
                   select new
                   {
                       SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                       NgayKiemKho = phieukiemkho.NgayKiemKho,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieukiemkho.TrangThai,
                       ChuThich = phieukiemkho.GhiChu,
                   }).AsEnumerable().Select(x => new KiemKhoViewModel()
                   {
                       soPhieuKiemKho = x.SoPhieuKiemKho,
                       ngayKiemKho = x.NgayKiemKho,
                       tenNhanVien = x.TenNhanVien,
                       trangThai = x.TrangThai,
                       ghiChu = x.ChuThich,
                   }).ToList();
            return all;
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuKiemKhoRepo.GetByIdAsync(ID);
        }

        public async Task HuyPhieuKiemKho(object editModel)
        {
            PhieuKiemKho editPhieuKiemKho = (PhieuKiemKho)editModel;
            editPhieuKiemKho.TrangThai = false;

            await _phieuKiemKhoRepo.EditAsync(editPhieuKiemKho);
        }

        public IEnumerable<ThongTinHoatDongViewModel> ThongTinHoatDong()
        {
            IQueryable<PhieuKiemKho> danhSachPhieuKiemKho = _phieuKiemKhoRepo.GetAll();
            List<ThongTinHoatDongViewModel> all = new List<ThongTinHoatDongViewModel>();

            all = (from phieukiemkho in danhSachPhieuKiemKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieukiemkho.MaNhanVien equals nhanvien.MaNhanVien
                   orderby phieukiemkho.NgayChinhSua descending
                   select new
                   {
                       SoPhieuKiemKho = phieukiemkho.SoPhieuKiemKho,
                       NgayChinhSua = phieukiemkho.NgayChinhSua,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieukiemkho.TrangThai,
                       
                   }).AsEnumerable().Select(x => new ThongTinHoatDongViewModel()
                   {
                       soPhieuKiemKho = x.SoPhieuKiemKho,
                       ngayChinhSuaKiemKho = x.NgayChinhSua,
                       tenNhanVienKiemKho = x.TenNhanVien,
                        trangThaiKiemKho = x.TrangThai,
                   }).Take(2).ToList();
            return all;
        }
    }
    
}