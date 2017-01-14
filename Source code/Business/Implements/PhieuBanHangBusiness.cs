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
    public class PhieuBanHangBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuBanHangRepository _phieuBanHangRepo;
        //private readonly ChiTietPhieuBanHang _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private HangHoaBusiness _hangHoaBus;

        private NhanVienBusiness _nhanVienBus;

        public PhieuBanHangBusiness()
        {
            dbContext = new SMSEntities();
            _phieuBanHangRepo = new PhieuBanHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);           
            _nhanVienBus = new NhanVienBusiness();
            _hangHoaBus = new HangHoaBusiness();
        }

        public async Task Create(PhieuBanHangViewModel obj)
        {
            PhieuBanHang order = new PhieuBanHang
            {
                SoPhieuBanHang = obj.soPhieuBanHang,
                NgayBan = obj.ngayBan,
                MaNhanVien = obj.maNhanVien,
                Ghichu = obj.ghiChu,
                TenKhachHang = obj.tenKhachHang,
                SoDienThoai = obj.soDienThoai,
                TongTien = obj.tongTien,   
                TrangThai = true,
                NgayChinhSua = DateTime.Now            
            };

            order.ChiTetPhieuBanHangs = new List<ChiTietPhieuBanHang>();

            DateTime today = DateTime.Now;
            int thang = today.Month;
            int nam = today.Year;

            foreach(var i in obj.chiTietPhieuBanHang)
            {
                order.ChiTetPhieuBanHangs.Add(i);
                //Sơn
                _hangHoaBus.CapNhatHangHoaKhiTaoPhieuBanHang(i.MaHangHoa, i.SoLuong);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuBanHang(i.MaHangHoa, i.SoLuong, thang, nam);
            }

            await _phieuBanHangRepo.InsertAsync(order);
        }

        public IList<PhieuBanHangViewModel> ListView(string userName)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();
            List<PhieuBanHangViewModel> allForManager = new List<PhieuBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 4)
            {
                all = (from phieuBanHang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName) 
                                && phieuBanHang.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuBanHang = phieuBanHang.SoPhieuBanHang,
                           NgayBan = phieuBanHang.NgayBan,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TenKhachHang = phieuBanHang.TenKhachHang,
                           SoDienThoai = phieuBanHang.SoDienThoai,
                           TongTien = phieuBanHang.TongTien,
                           GhiChu = phieuBanHang.Ghichu,

                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                           ngayBan = x.NgayBan,
                           tenNhanVien = x.TenNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           tongTien = x.TongTien,
                           ghiChu = x.GhiChu,
                       }).ToList();
                return all;
            }
            else
            {
                allForManager = (from phieuBanHang in danhSachPhieuBanHang
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuBanHang.MaNhanVien equals nhanvien.MaNhanVien
                                 select new
                                 {
                                     SoPhieuBanHang = phieuBanHang.SoPhieuBanHang,
                                     NgayBan = phieuBanHang.NgayBan,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TenKhachHang = phieuBanHang.TenKhachHang,
                                     SoDienThoai = phieuBanHang.SoDienThoai,
                                     TongTien = phieuBanHang.TongTien,
                                     GhiChu = phieuBanHang.Ghichu,

                                 }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                 {
                                     soPhieuBanHang = x.SoPhieuBanHang,
                                     ngayBan = x.NgayBan,
                                     tenNhanVien = x.TenNhanVien,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     tongTien = x.TongTien,
                                     ghiChu = x.GhiChu,

                                 }).ToList();
                return allForManager;
            }
        }

        public IList<PhieuBanHangViewModel> SearchDanhSachPhieuBanHang(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();
            List<PhieuBanHangViewModel> allForManager = new List<PhieuBanHangViewModel>();

            if (_nhanVienBus.layMaChucVu(userName) == 4)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieubanhang in danhSachPhieuBanHang
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && (
                                     phieubanhang.SoPhieuBanHang.ToString().Contains(key)
                                  || phieubanhang.TenKhachHang.Contains(key)
                                  || phieubanhang.SoDienThoai.Contains(key)))
                           select new
                           {
                               SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                               NgayBan = phieubanhang.NgayBan,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieubanhang.TrangThai,
                               ChuThich = phieubanhang.Ghichu,
                               TongTien = phieubanhang.TongTien,
                               TenKhachHang = phieubanhang.TenKhachHang,
                               SoDienThoai = phieubanhang.SoDienThoai,

                           }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                           {
                               soPhieuBanHang = x.SoPhieuBanHang,
                               ngayBan = x.NgayBan,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                               tongTien = x.TongTien,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                           }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieubanhang in danhSachPhieuBanHang
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(userName) && phieubanhang.TrangThai.ToString().Equals(trangthai))
                           select new
                           {
                               SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                               NgayBan = phieubanhang.NgayBan,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieubanhang.TrangThai,
                               ChuThich = phieubanhang.Ghichu,
                               TongTien = phieubanhang.TongTien,
                               TenKhachHang = phieubanhang.TenKhachHang,
                               SoDienThoai = phieubanhang.SoDienThoai,

                           }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                           {
                               soPhieuBanHang = x.SoPhieuBanHang,
                               ngayBan = x.NgayBan,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                               tongTien = x.TongTien,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                           }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return all;
                }

                all = (from phieubanhang in danhSachPhieuBanHang
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(userName) && phieubanhang.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                           NgayBan = phieubanhang.NgayBan,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieubanhang.TrangThai,
                           ChuThich = phieubanhang.Ghichu,
                           TongTien = phieubanhang.TongTien,
                           TenKhachHang = phieubanhang.TenKhachHang,
                           SoDienThoai = phieubanhang.SoDienThoai,

                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                           ngayBan = x.NgayBan,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                           tongTien = x.TongTien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                       }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                return all;
            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieubanhang.NgayBan >= tungay.Date && phieubanhang.NgayBan <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,
                                         TongTien = phieubanhang.TongTien,
                                         TenKhachHang = phieubanhang.TenKhachHang,
                                         SoDienThoai = phieubanhang.SoDienThoai,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         tongTien = x.TongTien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieubanhang.SoPhieuBanHang.ToString().Contains(key)
                                            || phieubanhang.TenKhachHang.Contains(key)
                                            || phieubanhang.SoDienThoai.Contains(key))
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,
                                         TongTien = phieubanhang.TongTien,
                                         TenKhachHang = phieubanhang.TenKhachHang,
                                         SoDienThoai = phieubanhang.SoDienThoai,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         tongTien = x.TongTien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieubanhang in danhSachPhieuBanHang
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieubanhang.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                         NgayBan = phieubanhang.NgayBan,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieubanhang.TrangThai,
                                         ChuThich = phieubanhang.Ghichu,
                                         TongTien = phieubanhang.TongTien,
                                         TenKhachHang = phieubanhang.TenKhachHang,
                                         SoDienThoai = phieubanhang.SoDienThoai,

                                     }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                     {
                                         soPhieuBanHang = x.SoPhieuBanHang,
                                         ngayBan = x.NgayBan,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         tongTien = x.TongTien,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                     }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                    return allForManager;
                }

                allForManager = (from phieubanhang in danhSachPhieuBanHang
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieubanhang.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                                     NgayBan = phieubanhang.NgayBan,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieubanhang.TrangThai,
                                     ChuThich = phieubanhang.Ghichu,
                                     TongTien = phieubanhang.TongTien,
                                     TenKhachHang = phieubanhang.TenKhachHang,
                                     SoDienThoai = phieubanhang.SoDienThoai,

                                 }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                                 {
                                     soPhieuBanHang = x.SoPhieuBanHang,
                                     ngayBan = x.NgayBan,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                     tongTien = x.TongTien,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                 }).OrderByDescending(x => x.soPhieuBanHang).ToList();
                return allForManager;
            }
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuBanHangRepo.GetByIdAsync(ID);
        }

        public async Task Delete(object deleteModel)
        {
            PhieuBanHang xoaPhieuBanHang = (PhieuBanHang)deleteModel;
            xoaPhieuBanHang.NgayChinhSua = DateTime.Now;
            xoaPhieuBanHang.TrangThai = false;

            //Sơn
            var phieuBanHang = dbContext.ChiTietPhieuBanHangs.Where(x => x.SoPhieuBanHang == xoaPhieuBanHang.SoPhieuBanHang);
            int thang = dbContext.PhieuBanHangs.SingleOrDefault(x => x.SoPhieuBanHang == xoaPhieuBanHang.SoPhieuBanHang).NgayBan.Month;
            int nam = dbContext.PhieuBanHangs.SingleOrDefault(x => x.SoPhieuBanHang == xoaPhieuBanHang.SoPhieuBanHang).NgayBan.Year;

            foreach (var i in phieuBanHang)
            {
                _hangHoaBus.CapNhatHangHoaKhiXoaPhieuBanHang(i.MaHangHoa, i.SoLuong);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuBanHang(i.MaHangHoa, i.SoLuong, thang, nam);
            }

            await _phieuBanHangRepo.EditAsync(xoaPhieuBanHang);
            //await _phieuBanHangRepo.DeleteAsync(xoaPhieuBanHang);
        }

        public IEnumerable<PhieuBanHangViewModel> thongTinPhieuBanHangTheoMa(int soPhieuBanHang)
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<PhieuBanHangViewModel> all = new List<PhieuBanHangViewModel>();

            all = (from phieubanhang in danhSachPhieuBanHang
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieubanhang.SoPhieuBanHang.Equals(soPhieuBanHang))
                   select new
                   {
                       SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       NgayBan = phieubanhang.NgayBan,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TenKhachHang = phieubanhang.TenKhachHang,
                       SoDienThoai = phieubanhang.SoDienThoai,
                       TongTien = phieubanhang.TongTien,
                       GhiChu = phieubanhang.Ghichu,
                       TrangThai = phieubanhang.TrangThai,

                   }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       ngayBan = x.NgayBan,
                       tenNhanVien = x.TenNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       tongTien = x.TongTien,
                       ghiChu = x.GhiChu,
                       trangThai = x.TrangThai
                       
                   }).ToList();        

            return all;
        }

        public int LoadSoPhieuBanHang()
        {
            var soPhieuKiemKho = from phieuBanHang in _phieuBanHangRepo.GetAll()
                                 orderby phieuBanHang.SoPhieuBanHang descending
                                select phieuBanHang.SoPhieuBanHang;

            int demSoPhieu = _phieuBanHangRepo.GetAll().Count();
            if(demSoPhieu == 0)
            {
                return 1;
            }

            return (soPhieuKiemKho.First() + 1);
        }

        public IEnumerable<ThongTinHoatDongViewModel> ThongTinHoatDong()
        {
            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            List<ThongTinHoatDongViewModel> all = new List<ThongTinHoatDongViewModel>();

            all = (from phieubanhang in danhSachPhieuBanHang
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieubanhang.MaNhanVien equals nhanvien.MaNhanVien
                   orderby phieubanhang.NgayChinhSua descending
                   select new
                   {
                       SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       NgayChinhSua = phieubanhang.NgayChinhSua,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieubanhang.TrangThai,
                   }).AsEnumerable().Select(x => new ThongTinHoatDongViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       ngayChinhSuaBanHang = x.NgayChinhSua,
                       tenNhanVienBanHang = x.TenNhanVien,
                       trangThaiBanHang = x.TrangThai,
                   }).Take(2).ToList();
            return all;
        }
        public object TongTienBanHang()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            var all = (from phieubanhang in danhSachPhieuBanHang
                   orderby phieubanhang.NgayChinhSua descending
                  where phieubanhang.NgayBan.Day.Equals(ngay) 
                        && phieubanhang.NgayBan.Month.Equals(thang) 
                        && phieubanhang.NgayBan.Year.Equals(nam)
                        && phieubanhang.TrangThai.Equals(true)
                   select new
                   {
                       TongTien = phieubanhang.TongTien,
                   }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                   {
                       tongTien = x.TongTien,
                   }).Sum(x => x.tongTien);
            return all;
        }

        public object SoDonBanHang()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            var all = (from phieubanhang in danhSachPhieuBanHang
                       where phieubanhang.NgayBan.Day.Equals(ngay)
                             && phieubanhang.NgayBan.Month.Equals(thang)
                             && phieubanhang.NgayBan.Year.Equals(nam)
                       select new
                       {
                           SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                       }).Count();
            return all;
        }

        public object SoDonBanHangHuy()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuBanHang> danhSachPhieuBanHang = _phieuBanHangRepo.GetAll();
            var all = (from phieubanhang in danhSachPhieuBanHang
                       where phieubanhang.NgayBan.Day.Equals(ngay)
                             && phieubanhang.NgayBan.Month.Equals(thang)
                             && phieubanhang.NgayBan.Year.Equals(nam)
                             && phieubanhang.TrangThai.Equals(false)
                       select new
                       {
                           SoPhieuBanHang = phieubanhang.SoPhieuBanHang,
                       }).AsEnumerable().Select(x => new PhieuBanHangViewModel()
                       {
                           soPhieuBanHang = x.SoPhieuBanHang,
                       }).Count();
            return all;
        }
    }
}
