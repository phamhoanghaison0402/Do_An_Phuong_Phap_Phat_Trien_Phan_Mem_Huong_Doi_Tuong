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
    public class PhieuDatHangBusiness
    {
        private SMSEntities dbContext;

        private readonly PhieuDatHangRepository _phieuDatHangRepo;
        //private readonly ChiTietPhieuDatHang _chiTietPhieuKiemKhoRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;

        private NhanVienBusiness _nhanVienBus;
        private HangHoaBusiness _hangHoaBus;

        public PhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();

            _phieuDatHangRepo = new PhieuDatHangRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
            _hangHoaBus = new HangHoaBusiness();
        }

        public int Insert(PhieuDatHang order)
        {
            dbContext.PhieuDatHangs.Add(order);
            dbContext.SaveChanges();
            return order.SoPhieuDatHang;
        }

        public async Task Update(PhieuDatHang entity)
        {
            DateTime today = DateTime.Now;
            int thang = today.Month;
            int nam = today.Year;

            foreach (var i in entity.ChiTetPhieuDatHangs)
            {
                //Sơn
                _hangHoaBus.CapNhatHangHoaKhiTaoPhieuBanHang(i.MaHangHoa, i.SoLuong);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuBanHang(i.MaHangHoa, i.SoLuong, thang, nam);
            }

            await _phieuDatHangRepo.EditAsync(entity);
        }

        public bool UpdateTongTien(PhieuDatHang entity)
        {
            try
            {
                var user = dbContext.PhieuDatHangs.Find(entity.SoPhieuDatHang);
                user.TongTien = entity.TongTien;

                dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }

            return true;
        }

        public async Task Update(PhieuDatHangViewModel model, PhieuDatHang phieu)
        {
            PhieuDatHang edit = phieu;

            phieu.DaXacNhan = model.daXacNhan;
            phieu.DaThanhToan = model.daThanhToan;
            phieu.NgayChinhSua = DateTime.Now;
            phieu.TrangThai = model.trangThai;

            await _phieuDatHangRepo.EditAsync(edit);
        }

        public async Task<object> Find(int ID)
        {
            return await _phieuDatHangRepo.GetByIdAsync(ID);
        }

        public PhieuDatHang LayPhieuDatHang(int soPhieuDatHang)
        {
            return dbContext.PhieuDatHangs.Find(soPhieuDatHang);
        }

        public async Task Create(PhieuDatHangViewModel obj)
        {
            PhieuDatHang order = new PhieuDatHang
            {
                SoPhieuDatHang = obj.soPhieuDatHang,
                NgayDat = obj.ngayDat,
                NgayGiao = obj.ngayGiao,
                MaNhanVien = obj.maNhanVien,
                TenKhachHang = obj.tenKhachHang,
                SoDienThoai = obj.soDienThoai,
                Email = obj.email,
                HinhThucThanhToan = obj.hinhThucThanhToan,
                Ghichu = obj.ghiChu,
                DaXacNhan = obj.daXacNhan,
                DaThanhToan = obj.daThanhToan,
                Diachi = obj.diaChi,
                TrangThai = true,
                NgayChinhSua = DateTime.Now                
            };

            order.ChiTetPhieuDatHangs = new List<ChiTietPhieuDatHang>();

            foreach(var i in obj.chiTietPhieuDatHang)
            {
                order.ChiTetPhieuDatHangs.Add(i);
            }

            await _phieuDatHangRepo.InsertAsync(order);
        }

        public IList<PhieuDatHangViewModel> ListView(string nhanVienCode)
        {
            var phieuBanHang = (new PhieuBanHangRepository(dbContext)).GetAll();
            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();
            List<PhieuDatHangViewModel> allForManager = new List<PhieuDatHangViewModel>();

            if(_nhanVienBus.layMaChucVu(nhanVienCode) != 4 &&
                _nhanVienBus.layMaChucVu(nhanVienCode) != 3)
            {
                return all;
            }

            if(_nhanVienBus.layMaChucVu(nhanVienCode) == 4)
            {
                all = (from phieuDatHang in danhSachPhieuDatHang
                       where phieuDatHang.TrangThai == true
                       select new
                       {
                           SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                           NgayDat = phieuDatHang.NgayDat,
                           MaNhanVien = phieuDatHang.MaNhanVien,
                           TenKhachHang = phieuDatHang.TenKhachHang,
                           SoDienThoai = phieuDatHang.SoDienThoai,
                           DiaChi = phieuDatHang.Diachi,
                           Email = phieuDatHang.Email,
                           TongTien = phieuDatHang.TongTien,
                           HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                           GhiChu = phieuDatHang.Ghichu,
                           NgayGiao = phieuDatHang.NgayGiao,
                           DaXacNhan = phieuDatHang.DaXacNhan,
                           DaThanhToan = phieuDatHang.DaThanhToan,
                           TrangThai = phieuDatHang.TrangThai,

                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                           ngayDat = x.NgayDat,
                           maNhanVien = x.MaNhanVien,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           diaChi = x.DiaChi,
                           email = x.Email,
                           tongTien = x.TongTien,
                           hinhThucThanhToan = x.HinhThucThanhToan,
                           ghiChu = x.GhiChu,
                           ngayGiao = x.NgayGiao,
                           daXacNhan = x.DaXacNhan,
                           daThanhToan = x.DaThanhToan,
                           trangThai = x.TrangThai
                       }).ToList();

                return all;
            }
            else
            {
                allForManager = (from phieuDatHang in danhSachPhieuDatHang
                                 select new
                                 {
                                     SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                                     NgayDat = phieuDatHang.NgayDat,
                                     MaNhanVien = phieuDatHang.MaNhanVien,
                                     TenKhachHang = phieuDatHang.TenKhachHang,
                                     SoDienThoai = phieuDatHang.SoDienThoai,
                                     DiaChi = phieuDatHang.Diachi,
                                     Email = phieuDatHang.Email,
                                     TongTien = phieuDatHang.TongTien,
                                     HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                                     GhiChu = phieuDatHang.Ghichu,
                                     NgayGiao = phieuDatHang.NgayGiao,
                                     DaXacNhan = phieuDatHang.DaXacNhan,
                                     DaThanhToan = phieuDatHang.DaThanhToan

                                 }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                 {
                                     soPhieuDatHang = x.SoPhieuDatHang,
                                     ngayDat = x.NgayDat,
                                     maNhanVien = x.MaNhanVien,                                     
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     diaChi = x.DiaChi,
                                     email = x.Email,
                                     tongTien = x.TongTien,
                                     hinhThucThanhToan = x.HinhThucThanhToan,
                                     ghiChu = x.GhiChu,
                                     ngayGiao = x.NgayGiao,
                                     daXacNhan = x.DaXacNhan,
                                     daThanhToan = x.DaThanhToan

                                 }).ToList();

                return allForManager;
            }
        }

        public IEnumerable<PhieuDatHangViewModel> thongTinPhieuDatHangTheoMa(int soPhieuDatHang)
        {
            IQueryable<PhieuDatHang> danhSachPhieuBanHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();

            all = (from phieuDatHang in danhSachPhieuBanHang
                   where (phieuDatHang.SoPhieuDatHang == soPhieuDatHang && phieuDatHang.TrangThai == true)
                   select new
                   {
                       SoPhieuDatHang = phieuDatHang.SoPhieuDatHang,
                       NgayDat = phieuDatHang.NgayDat,
                       MaNhanVien = phieuDatHang.MaNhanVien,
                       TenKhachHang = phieuDatHang.TenKhachHang,
                       SoDienThoai = phieuDatHang.SoDienThoai,
                       DiaChi = phieuDatHang.Diachi,
                       Email = phieuDatHang.Email,
                       TongTien = phieuDatHang.TongTien,
                       HinhThucThanhToan = phieuDatHang.HinhThucThanhToan,
                       GhiChu = phieuDatHang.Ghichu,
                       NgayGiao = phieuDatHang.NgayGiao,
                       DaXacNhan = phieuDatHang.DaXacNhan,
                       DaThanhToan = phieuDatHang.DaThanhToan

                   }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                   {
                       soPhieuDatHang = x.SoPhieuDatHang,
                       ngayDat = x.NgayDat,
                       maNhanVien = x.MaNhanVien,
                       tenKhachHang = x.TenKhachHang,
                       soDienThoai = x.SoDienThoai,
                       diaChi = x.DiaChi,
                       email = x.Email,
                       tongTien = x.TongTien,
                       hinhThucThanhToan = x.HinhThucThanhToan,
                       ghiChu = x.GhiChu,
                       ngayGiao = x.NgayGiao,
                       daXacNhan = x.DaXacNhan,
                       daThanhToan = x.DaThanhToan
                   }).ToList();

            return all;
        }

        public IList<PhieuDatHangViewModel> SearchDanhSachPhieuDatHang(String key, string trangthai, DateTime tungay, DateTime denngay, string userName)
        {
            IQueryable<PhieuDatHang> danhSachPhieuBanHang = _phieuDatHangRepo.GetAll();
            List<PhieuDatHangViewModel> all = new List<PhieuDatHangViewModel>();
            List<PhieuDatHangViewModel> allForManager = new List<PhieuDatHangViewModel>();

            if(_nhanVienBus.layMaChucVu(userName) == 4)
            {
                if(!string.IsNullOrEmpty(key))
                {
                    all = (from phieudathang in danhSachPhieuBanHang
                           //join nhanvien in _nhanVienRepo.GetAll()
                           //on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                           where (
                                     phieudathang.SoPhieuDatHang.ToString().Contains(key)
                                    || phieudathang.TrangThai.ToString().Equals(trangthai)
                                    || phieudathang.TenKhachHang.Contains(key)
                                     || phieudathang.SoDienThoai.Contains(key)
                                     || phieudathang.Email.Contains(key)
                                     || phieudathang.Diachi.Contains(key))
                           select new
                           {
                               SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                               NgayDat = phieudathang.NgayDat,
                               //TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieudathang.TrangThai,
                               ChuThich = phieudathang.Ghichu,
                               DaXacNhan = phieudathang.DaXacNhan,
                               DaThanhToan = phieudathang.DaThanhToan,
                               TenKhachHang = phieudathang.TenKhachHang,
                               SoDienThoai = phieudathang.SoDienThoai,
                               TongTien = phieudathang.TongTien,
                               HinhThucThanhToan = phieudathang.HinhThucThanhToan

                           }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                           {
                               soPhieuDatHang = x.SoPhieuDatHang,
                               ngayDat = x.NgayDat,
                               //tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                               daXacNhan = x.DaXacNhan,
                               daThanhToan = x.DaThanhToan,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                               tongTien = x.TongTien,
                               hinhThucThanhToan = x.HinhThucThanhToan
                           }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                    return all;
                }
                if(!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieudathang in danhSachPhieuBanHang
                           //join nhanvien in _nhanVienRepo.GetAll()
                           //on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                           where phieudathang.TrangThai.ToString().Equals(trangthai)
                           select new
                           {
                               SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                               NgayDat = phieudathang.NgayDat,
                               //TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieudathang.TrangThai,
                               ChuThich = phieudathang.Ghichu,
                               DaXacNhan = phieudathang.DaXacNhan,
                               DaThanhToan = phieudathang.DaThanhToan,
                               TenKhachHang = phieudathang.TenKhachHang,
                               SoDienThoai = phieudathang.SoDienThoai,
                               TongTien = phieudathang.TongTien,
                               HinhThucThanhToan = phieudathang.HinhThucThanhToan

                           }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                           {
                               soPhieuDatHang = x.SoPhieuDatHang,
                               ngayDat = x.NgayDat,
                              // tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                               daXacNhan = x.DaXacNhan,
                               daThanhToan = x.DaThanhToan,
                               tenKhachHang = x.TenKhachHang,
                               soDienThoai = x.SoDienThoai,
                               tongTien = x.TongTien,
                               hinhThucThanhToan = x.HinhThucThanhToan
                           }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                    return all;
                }

                all = (from phieudathang in danhSachPhieuBanHang
                       //join nhanvien in _nhanVienRepo.GetAll()
                      // on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                       where phieudathang.TrangThai.Equals(true)
                       select new
                       {
                           SoPhieudatHang = phieudathang.SoPhieuDatHang,
                           NgayDat = phieudathang.NgayDat,
                           //TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieudathang.TrangThai,
                           ChuThich = phieudathang.Ghichu,
                           DaXacNhan = phieudathang.DaXacNhan,
                           DaThanhToan = phieudathang.DaThanhToan,
                           TenKhachHang = phieudathang.TenKhachHang,
                           SoDienThoai = phieudathang.SoDienThoai,
                           TongTien = phieudathang.TongTien,
                           HinhThucThanhToan = phieudathang.HinhThucThanhToan

                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieudatHang,
                           ngayDat = x.NgayDat,
                          // tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                           daXacNhan = x.DaXacNhan,
                           daThanhToan = x.DaThanhToan,
                           tenKhachHang = x.TenKhachHang,
                           soDienThoai = x.SoDienThoai,
                           tongTien = x.TongTien,
                           hinhThucThanhToan = x.HinhThucThanhToan
                       }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                return all;
            }
            else
            {
                if((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieudathang in danhSachPhieuBanHang
                                     //join nhanvien in _nhanVienRepo.GetAll()
                                     //on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieudathang.NgayDat >= tungay.Date && phieudathang.NgayDat <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                                         NgayDat = phieudathang.NgayDat,
                                         //TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieudathang.TrangThai,
                                         ChuThich = phieudathang.Ghichu,
                                         DaXacNhan = phieudathang.DaXacNhan,
                                         DaThanhToan = phieudathang.DaThanhToan,
                                         TenKhachHang = phieudathang.TenKhachHang,
                                         SoDienThoai = phieudathang.SoDienThoai,
                                         TongTien = phieudathang.TongTien,
                                         HinhThucThanhToan = phieudathang.HinhThucThanhToan

                                     }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                     {
                                         soPhieuDatHang = x.SoPhieuDatHang,
                                         ngayDat = x.NgayDat,
                                        // tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         daXacNhan = x.DaXacNhan,
                                         daThanhToan = x.DaThanhToan,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         tongTien = x.TongTien,
                                         hinhThucThanhToan = x.HinhThucThanhToan
                                     }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                    return allForManager;
                }
                if(!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieudathang in danhSachPhieuBanHang
                                     //join nhanvien in _nhanVienRepo.GetAll()
                                    // on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieudathang.SoPhieuDatHang.ToString().Contains(key)
                                     || phieudathang.TenKhachHang.Contains(key)
                                     || phieudathang.SoDienThoai.Contains(key)
                                     || phieudathang.Email.Contains(key)
                                     || phieudathang.Diachi.Contains(key)
                  
                                     select new
                                     {
                                         SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                                         NgayDat = phieudathang.NgayDat,
                                         //TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieudathang.TrangThai,
                                         ChuThich = phieudathang.Ghichu,
                                         DaXacNhan = phieudathang.DaXacNhan,
                                         DaThanhToan = phieudathang.DaThanhToan,
                                         TenKhachHang = phieudathang.TenKhachHang,
                                         SoDienThoai = phieudathang.SoDienThoai,
                                         TongTien = phieudathang.TongTien,
                                         HinhThucThanhToan = phieudathang.HinhThucThanhToan

                                     }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                     {
                                         soPhieuDatHang = x.SoPhieuDatHang,
                                         ngayDat = x.NgayDat,
                                        // tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         daXacNhan = x.DaXacNhan,
                                         daThanhToan = x.DaThanhToan,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         tongTien = x.TongTien,
                                         hinhThucThanhToan = x.HinhThucThanhToan
                                     }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                    return allForManager;
                }
                if(!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieudathang in danhSachPhieuBanHang
                                    // join nhanvien in _nhanVienRepo.GetAll()
                                    // on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieudathang.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                                         NgayDat = phieudathang.NgayDat,
                                         //TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieudathang.TrangThai,
                                         ChuThich = phieudathang.Ghichu,
                                         DaXacNhan = phieudathang.DaXacNhan,
                                         DaThanhToan = phieudathang.DaThanhToan,
                                         TenKhachHang = phieudathang.TenKhachHang,
                                         SoDienThoai = phieudathang.SoDienThoai,
                                         TongTien = phieudathang.TongTien,
                                         HinhThucThanhToan = phieudathang.HinhThucThanhToan

                                     }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                     {
                                         soPhieuDatHang = x.SoPhieuDatHang,
                                         ngayDat = x.NgayDat,
                                         //tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                         daXacNhan = x.DaXacNhan,
                                         daThanhToan = x.DaThanhToan,
                                         tenKhachHang = x.TenKhachHang,
                                         soDienThoai = x.SoDienThoai,
                                         tongTien = x.TongTien,
                                         hinhThucThanhToan = x.HinhThucThanhToan
                                     }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                    return allForManager;
                }

                allForManager = (from phieudathang in danhSachPhieuBanHang
                                // join nhanvien in _nhanVienRepo.GetAll()
                                // on phieudathang.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieudathang.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                                     NgayDat = phieudathang.NgayDat,
                                     //TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieudathang.TrangThai,
                                     ChuThich = phieudathang.Ghichu,
                                     DaXacNhan = phieudathang.DaXacNhan,
                                     DaThanhToan = phieudathang.DaThanhToan,
                                     TenKhachHang = phieudathang.TenKhachHang,
                                     SoDienThoai = phieudathang.SoDienThoai,
                                     TongTien = phieudathang.TongTien,
                                     HinhThucThanhToan = phieudathang.HinhThucThanhToan

                                 }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                                 {
                                     soPhieuDatHang = x.SoPhieuDatHang,
                                     ngayDat = x.NgayDat,
                                     //tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                     daXacNhan = x.DaXacNhan,
                                     daThanhToan = x.DaThanhToan,
                                     tenKhachHang = x.TenKhachHang,
                                     soDienThoai = x.SoDienThoai,
                                     tongTien = x.TongTien,
                                     hinhThucThanhToan = x.HinhThucThanhToan
                                 }).OrderByDescending(x => x.soPhieuDatHang).ToList();
                return allForManager;
            }
        }

        public async Task DeletePhieuDatHang(object deleteModel)
        {
            PhieuDatHang xoaPhieuDatHang = (PhieuDatHang)deleteModel;
            xoaPhieuDatHang.TrangThai = false;
            xoaPhieuDatHang.NgayChinhSua = DateTime.Now;

            //Sơn
            var phieuDatHang = dbContext.ChiTietPhieuDatHangs.Where(x => x.SoPhieuDatHang == xoaPhieuDatHang.SoPhieuDatHang);
            int thang = dbContext.PhieuDatHangs.SingleOrDefault(x => x.SoPhieuDatHang == xoaPhieuDatHang.SoPhieuDatHang).NgayDat.Month;
            int nam = dbContext.PhieuDatHangs.SingleOrDefault(x => x.SoPhieuDatHang == xoaPhieuDatHang.SoPhieuDatHang).NgayDat.Year;

            foreach (var i in phieuDatHang)
            {
                _hangHoaBus.CapNhatHangHoaKhiXoaPhieuBanHang(i.MaHangHoa, i.SoLuong);
                _hangHoaBus.CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuBanHang(i.MaHangHoa, i.SoLuong, thang, nam);
            }

            await _phieuDatHangRepo.EditAsync(xoaPhieuDatHang);
           
        }

        public int LaySoDonDatHang()
        {
            return _phieuDatHangRepo.GetAll().Where(i => i.DaXacNhan == false).Count();
        }

        public object TongTienDatHang()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            var all = (from phieudathang in danhSachPhieuDatHang
                       where phieudathang.NgayDat.Day.Equals(ngay)
                             && phieudathang.NgayDat.Month.Equals(thang)
                             && phieudathang.NgayDat.Year.Equals(nam)
                             && phieudathang.TrangThai.Equals(true)
                       select new
                       {
                           TongTien = phieudathang.TongTien,
                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           tongTien = x.TongTien,
                       }).Sum(x => x.tongTien);
            return all;
        }

       
        public object SoDonDatHang()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            var all = (from phieudathang in danhSachPhieuDatHang
                       where phieudathang.NgayDat.Day.Equals(ngay)
                             && phieudathang.NgayDat.Month.Equals(thang)
                             && phieudathang.NgayDat.Year.Equals(nam)
                       select new
                       {
                           SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                       }).Count();
            return all;
        }
        public object SoDonDatHangHuy()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            var all = (from phieudathang in danhSachPhieuDatHang
                       where phieudathang.NgayDat.Day.Equals(ngay)
                             && phieudathang.NgayDat.Month.Equals(thang)
                             && phieudathang.NgayDat.Year.Equals(nam) 
                             && phieudathang.TrangThai.Equals(false)
                       select new
                       {
                           SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                       }).Count();
            return all;
        }

        public object DonHangDaXacNhan()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            var all = (from phieudathang in danhSachPhieuDatHang
                       where phieudathang.NgayDat.Day.Equals(ngay)
                             && phieudathang.NgayDat.Month.Equals(thang)
                             && phieudathang.NgayDat.Year.Equals(nam) && phieudathang.DaXacNhan.Equals(true)
                       select new
                       {
                           SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                       }).Count();
            return all;
        }

        public object DonHangDaThanhToan()
        {
            DateTime a = DateTime.Now;
            int ngay = a.Day;
            int thang = a.Month;
            int nam = a.Year;

            IQueryable<PhieuDatHang> danhSachPhieuDatHang = _phieuDatHangRepo.GetAll();
            var all = (from phieudathang in danhSachPhieuDatHang
                       where phieudathang.NgayDat.Day.Equals(ngay)
                             && phieudathang.NgayDat.Month.Equals(thang)
                             && phieudathang.NgayDat.Year.Equals(nam) && phieudathang.DaThanhToan.Equals(true)
                       select new
                       {
                           SoPhieuDatHang = phieudathang.SoPhieuDatHang,
                       }).AsEnumerable().Select(x => new PhieuDatHangViewModel()
                       {
                           soPhieuDatHang = x.SoPhieuDatHang,
                       }).Count();
            return all;
        }
    }
}
