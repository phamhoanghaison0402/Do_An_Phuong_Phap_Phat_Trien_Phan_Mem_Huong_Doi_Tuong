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
    public class PhieuNhapKhoBusiness
    {
        SMSEntities dbContext = null;
        private readonly PhieuNhapKhoRepository _phieuNhapKhoRepo;
        private readonly ChiTietPhieuNhapKhoRepository _chiTietPhieuNhapKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly NhaCungCapRepository _nhaCungCapRepo;
        private HangHoaBusiness _hangHoaBus;
        private NhanVienBusiness _nhanVienBus;

        public PhieuNhapKhoBusiness()
        {
            dbContext = new SMSEntities();
            _phieuNhapKhoRepo = new PhieuNhapKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _nhaCungCapRepo = new NhaCungCapRepository(dbContext);
            _chiTietPhieuNhapKhoRepo = new ChiTietPhieuNhapKhoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _hangHoaBus = new HangHoaBusiness();
            _nhanVienBus = new NhanVienBusiness();
        }
       
        public int LoadSoPhieuNhapKho()
        {
            var soPhieuKiemKho = from phieunhapkho in _phieuNhapKhoRepo.GetAll()
                                 orderby phieunhapkho.SoPhieuNhap descending
                                 select phieunhapkho.SoPhieuNhap;

            int a = _phieuNhapKhoRepo.GetAll().Count();
            if (a == 0)
            {
                return 1;
            }
            return (soPhieuKiemKho.First() + 1);
        }

        public IList<PhieuNhapKhoViewModel> SearchDanhSachPhieuNhapKho(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuNhap> dsPhieuNhap = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();
            List<PhieuNhapKhoViewModel> allForManager = new List<PhieuNhapKhoViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 5)
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    all = (from phieunhap in dsPhieuNhap
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                           join nhacungcap in _nhaCungCapRepo.GetAll()
                           on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                           where (nhanvien.UserName.Equals(userName) && phieunhap.NgayNhap >= tungay.Date && phieunhap.NgayNhap <= denngay.Date)
                           select new
                           {
                               SoPhieuNhap = phieunhap.SoPhieuNhap,
                               NgayNhap = phieunhap.NgayNhap,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieunhap.TongTien,
                               GhiChu = phieunhap.Ghichu,
                               TenNhaCungCap = nhacungcap.TenNhaCungCap,
                               TrangThai = phieunhap.TrangThai,

                           }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                           {
                               soPhieuNhapKho = x.SoPhieuNhap,
                               ngayNhapKho = x.NgayNhap,
                               tenNhanVien = x.TenNhanVien,
                               tongTien = x.TongTien,
                               ghiChu = x.GhiChu,
                               tenNhaCungCap = x.TenNhaCungCap,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return all;
                }

                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieunhap in dsPhieuNhap
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                           join nhacungcap in _nhaCungCapRepo.GetAll()
                           on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                           where (nhanvien.UserName.Equals(userName) && (
                                     phieunhap.SoPhieuNhap.ToString().Contains(key)
                                  || nhacungcap.TenNhaCungCap.Contains(key)))
                           select new
                           {
                               SoPhieuNhap = phieunhap.SoPhieuNhap,
                               NgayNhap = phieunhap.NgayNhap,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieunhap.TongTien,
                               GhiChu = phieunhap.Ghichu,
                               TenNhaCungCap = nhacungcap.TenNhaCungCap,
                               TrangThai = phieunhap.TrangThai,

                           }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                           {
                               soPhieuNhapKho = x.SoPhieuNhap,
                               ngayNhapKho = x.NgayNhap,
                               tenNhanVien = x.TenNhanVien,
                               tongTien = x.TongTien,
                               ghiChu = x.GhiChu,
                               tenNhaCungCap = x.TenNhaCungCap,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieunhap in dsPhieuNhap
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                           join nhacungcap in _nhaCungCapRepo.GetAll()
                           on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                           where (nhanvien.UserName.Equals(userName) && (
                                     phieunhap.TrangThai.Equals(trangthai)))
                           select new
                           {
                               SoPhieuNhap = phieunhap.SoPhieuNhap,
                               NgayNhap = phieunhap.NgayNhap,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieunhap.TongTien,
                               GhiChu = phieunhap.Ghichu,
                               TenNhaCungCap = nhacungcap.TenNhaCungCap,
                               TrangThai = phieunhap.TrangThai,

                           }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                           {
                               soPhieuNhapKho = x.SoPhieuNhap,
                               ngayNhapKho = x.NgayNhap,
                               tenNhanVien = x.TenNhanVien,
                               tongTien = x.TongTien,
                               ghiChu = x.GhiChu,
                               tenNhaCungCap = x.TenNhaCungCap,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return all;
                }

                all = (from phieunhap in dsPhieuNhap
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                 join nhacungcap in _nhaCungCapRepo.GetAll()
                                 on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                                 where (nhanvien.UserName.Equals(userName))
                       select new
                       {
                           SoPhieuNhap = phieunhap.SoPhieuNhap,
                           NgayNhap = phieunhap.NgayNhap,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TongTien = phieunhap.TongTien,
                           GhiChu = phieunhap.Ghichu,
                           TenNhaCungCap = nhacungcap.TenNhaCungCap,
                           TrangThai = phieunhap.TrangThai,

                       }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                       {
                           soPhieuNhapKho = x.SoPhieuNhap,
                           ngayNhapKho = x.NgayNhap,
                           tenNhanVien = x.TenNhanVien,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                           tenNhaCungCap = x.TenNhaCungCap,
                           trangThai = x.TrangThai,
                       }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieunhap in dsPhieuNhap
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                           join nhacungcap in _nhaCungCapRepo.GetAll()
                           on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                           where (phieunhap.NgayNhap >= tungay.Date && phieunhap.NgayNhap <= denngay.Date)
                           select new
                           {
                               SoPhieuNhap = phieunhap.SoPhieuNhap,
                               NgayNhap = phieunhap.NgayNhap,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TongTien = phieunhap.TongTien,
                               GhiChu = phieunhap.Ghichu,
                               TenNhaCungCap = nhacungcap.TenNhaCungCap,
                               TrangThai = phieunhap.TrangThai,

                           }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                           {
                               soPhieuNhapKho = x.SoPhieuNhap,
                               ngayNhapKho = x.NgayNhap,
                               tenNhanVien = x.TenNhanVien,
                               tongTien = x.TongTien,
                               ghiChu = x.GhiChu,
                               tenNhaCungCap = x.TenNhaCungCap,
                               trangThai = x.TrangThai,
                           }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieunhap in dsPhieuNhap
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                     join nhacungcap in _nhaCungCapRepo.GetAll()
                                     on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                                     where (phieunhap.SoPhieuNhap.ToString().Contains(key)
                                            || nhacungcap.TenNhaCungCap.Contains(key))
                                     select new
                                     {
                                         SoPhieuNhap = phieunhap.SoPhieuNhap,
                                         NgayNhap = phieunhap.NgayNhap,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTien = phieunhap.TongTien,
                                         GhiChu = phieunhap.Ghichu,
                                         TenNhaCungCap = nhacungcap.TenNhaCungCap,
                                         TrangThai = phieunhap.TrangThai,

                                     }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                                     {
                                         soPhieuNhapKho = x.SoPhieuNhap,
                                         ngayNhapKho = x.NgayNhap,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTien = x.TongTien,
                                         ghiChu = x.GhiChu,
                                         tenNhaCungCap = x.TenNhaCungCap,
                                         trangThai = x.TrangThai,
                                     }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return allForManager;
                }

                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieunhap in dsPhieuNhap
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                     join nhacungcap in _nhaCungCapRepo.GetAll()
                                     on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                                     where phieunhap.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuNhap = phieunhap.SoPhieuNhap,
                                         NgayNhap = phieunhap.NgayNhap,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TongTien = phieunhap.TongTien,
                                         GhiChu = phieunhap.Ghichu,
                                         TenNhaCungCap = nhacungcap.TenNhaCungCap,
                                         TrangThai = phieunhap.TrangThai,

                                     }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                                     {
                                         soPhieuNhapKho = x.SoPhieuNhap,
                                         ngayNhapKho = x.NgayNhap,
                                         tenNhanVien = x.TenNhanVien,
                                         tongTien = x.TongTien,
                                         ghiChu = x.GhiChu,
                                         tenNhaCungCap = x.TenNhaCungCap,
                                         trangThai = x.TrangThai,
                                     }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                    return allForManager;
                }
                allForManager = (from phieunhap in dsPhieuNhap
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieunhap.MaNhanVien equals nhanvien.MaNhanVien
                                 join nhacungcap in _nhaCungCapRepo.GetAll()
                                 on phieunhap.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                                 where phieunhap.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuNhap = phieunhap.SoPhieuNhap,
                                     NgayNhap = phieunhap.NgayNhap,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TongTien = phieunhap.TongTien,
                                     GhiChu = phieunhap.Ghichu,
                                     TenNhaCungCap = nhacungcap.TenNhaCungCap,
                                     TrangThai = phieunhap.TrangThai,

                                 }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                                 {
                                     soPhieuNhapKho = x.SoPhieuNhap,
                                     ngayNhapKho = x.NgayNhap,
                                     tenNhanVien = x.TenNhanVien,
                                     tongTien = x.TongTien,
                                     ghiChu = x.GhiChu,
                                     tenNhaCungCap = x.TenNhaCungCap,
                                     trangThai = x.TrangThai,
                                 }).OrderByDescending(x => x.soPhieuNhapKho).ToList();
                return allForManager;
            }
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuNhapKhoRepo.GetByIdAsync(ID);
        }

        public async Task Create(PhieuNhapKhoViewModel O)
        {
            PhieuNhap phieuNhap = new PhieuNhap
            {
                SoPhieuNhap = O.soPhieuNhapKho,
                NgayNhap = O.ngayNhapKho,
                MaNhanVien = O.maNhanVien,
                MaNhaCungCap = O.maNhaCungCap,
                TongTien = O.tongTien,
                Ghichu = O.ghiChu,
                TrangThai = true,
                NgayChinhSua = DateTime.Now,
            };
            DateTime today = DateTime.Now;
            int thang = today.Month;
            int nam = today.Year;
            foreach (var i in O.chiTietPhieuNhap)
            {
                phieuNhap.ChiTietPhieuNhaps.Add(i);
                _hangHoaBus.CapNhatHangHoaKhiTaoPhieuNhap(i.MaHangHoa, i.SoLuong, i.GiaNhap);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuNhap(i.MaHangHoa, i.SoLuong, thang, nam);               
            }
            await _phieuNhapKhoRepo.InsertAsync(phieuNhap);
        }

        public IList<PhieuNhapKhoViewModel> thongTinChiTietPhieuNhapKhoTheoMa(int soPhieuNhapKho)
        {
            IQueryable<ChiTietPhieuNhap> dsChiTietPhieuNhapKho = _chiTietPhieuNhapKhoRepo.GetAll();
            var all = (from chitietphieunhapkho in dsChiTietPhieuNhapKho
                       join hanghoa in _hangHoaRepo.GetAll()
                       on chitietphieunhapkho.MaHangHoa equals hanghoa.MaHangHoa
                       select new
                       {
                           SoPhieuNhapKho = chitietphieunhapkho.SoPhieuNhap,
                           MaHangHoa = hanghoa.MaHangHoa,
                           TenHangHoa = hanghoa.TenHangHoa,
                           DonViTinh = hanghoa.DonViTinh,
                           SoLuong = chitietphieunhapkho.SoLuong,
                           Gia = chitietphieunhapkho.GiaNhap,
                           ThanhTien = chitietphieunhapkho.ThanhTien,

                       }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                       {
                           soPhieuNhapKho = x.SoPhieuNhapKho,
                           maHangHoa = x.MaHangHoa,
                           tenHangHoa = x.TenHangHoa,
                           donViTinh = x.DonViTinh,
                           soLuong = x.SoLuong,
                           gia = x.Gia,
                           thanhTien = x.ThanhTien,
                       }).ToList();

            var information = (from i in all
                               where (soPhieuNhapKho == null || i.soPhieuNhapKho == soPhieuNhapKho)
                               select i).ToList();
            return information.ToList();
        }

        public IEnumerable<PhieuNhapKhoViewModel> thongTinPhieuNhapKhoTheoMa(int soPhieuNhapKho)
        {
            IQueryable<PhieuNhap> danhSachPhieuNhapKho = _phieuNhapKhoRepo.GetAll();
            List<PhieuNhapKhoViewModel> all = new List<PhieuNhapKhoViewModel>();

            all = (from phieunhapkho in danhSachPhieuNhapKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieunhapkho.MaNhanVien equals nhanvien.MaNhanVien
                   join nhacungcap in _nhaCungCapRepo.GetAll()
                   on phieunhapkho.MaNhaCungCap equals nhacungcap.MaNhaCungCap
                   where (phieunhapkho.SoPhieuNhap.Equals(soPhieuNhapKho))
                   select new
                   {
                       SoPhieuNhapKho = phieunhapkho.SoPhieuNhap,
                       NgayNhapKho = phieunhapkho.NgayNhap,
                       TenNhaCungCap = nhacungcap.TenNhaCungCap,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TongTien = phieunhapkho.TongTien,
                       GhiChu = phieunhapkho.Ghichu,
                   }).AsEnumerable().Select(x => new PhieuNhapKhoViewModel()
                   {
                       soPhieuNhapKho = x.SoPhieuNhapKho,
                       ngayNhapKho = x.NgayNhapKho,
                       tenNhaCungCap = x.TenNhaCungCap,
                       tenNhanVien = x.TenNhanVien,
                       tongTien = x.TongTien,
                       ghiChu = x.GhiChu,
                   }).ToList();
            return all;
        }

        public async Task HuyPhieuNhapKho(object editModel)
        {
            try
            {
                PhieuNhap editPhieuNhapKho = (PhieuNhap)editModel;
                editPhieuNhapKho.TrangThai = false;
                await _phieuNhapKhoRepo.EditAsync(editPhieuNhapKho);

                var phieuNhapKho = dbContext.ChiTietPhieuNhaps.Where(x => x.SoPhieuNhap == editPhieuNhapKho.SoPhieuNhap);
                int thang = dbContext.PhieuNhaps.SingleOrDefault(x => x.SoPhieuNhap == editPhieuNhapKho.SoPhieuNhap).NgayNhap.Month;
                int nam = dbContext.PhieuNhaps.SingleOrDefault(x => x.SoPhieuNhap == editPhieuNhapKho.SoPhieuNhap).NgayNhap.Year;

                foreach (var i in phieuNhapKho)
                {
                    _hangHoaBus.CapNhatHangHoaKhiXoaPhieuNhap(i.SoPhieuNhap, i.MaHangHoa, i.SoLuong, i.GiaNhap);
                    _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuNhap(i.MaHangHoa, i.SoLuong, thang, nam);
                }

            }
            catch (Exception)
            {

            }
        }

        public IEnumerable<ThongTinHoatDongViewModel> ThongTinHoatDong()
        {
            IQueryable<PhieuNhap> danhSachPhieuNhapKho = _phieuNhapKhoRepo.GetAll();
            List<ThongTinHoatDongViewModel> all = new List<ThongTinHoatDongViewModel>();

            all = (from phieunhapkho in danhSachPhieuNhapKho
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieunhapkho.MaNhanVien equals nhanvien.MaNhanVien
                   orderby phieunhapkho.NgayChinhSua descending
                   select new
                   {
                       SoPhieuNhapKho = phieunhapkho.SoPhieuNhap,
                       NgayChinhSua = phieunhapkho.NgayChinhSua,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieunhapkho.TrangThai,
                   }).AsEnumerable().Select(x => new ThongTinHoatDongViewModel()
                   {
                       soPhieuNhapKho = x.SoPhieuNhapKho,
                       ngayChinhSuaNhapKho = x.NgayChinhSua,
                       tenNhanVienNhapKho = x.TenNhanVien,
                       trangThaiNhapKho = x.TrangThai,
                   }).Take(2).ToList();
            return all;
        }
        public Object LayTongTienPhieuNhap(int maPhieuNhap)
        {
            var phieuNhapInfor = from phieunhap in dbContext.PhieuNhaps
                                 where (phieunhap.SoPhieuNhap == maPhieuNhap && phieunhap.TrangThai == true)
                                 select new
                                 {
                                     phieunhap.TongTien
                                 };
            return phieuNhapInfor;
        }
    }
}