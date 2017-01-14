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
    public class HangHoaBusiness
    {
        SMSEntities dbContext;
        private readonly PhieuKiemKhoRepository _phieuKiemKhoRepo;
        private readonly ChiTietPhieuBanHangRepository _chiTietPhieuBanHangRepo;
        private readonly PhieuBanHangRepository _phieuBanHangRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;
        private NhanVienBusiness _nhanVienBus;
        ConvertToUnSign unSign = new ConvertToUnSign();

        public HangHoaBusiness()
        {
            dbContext = new SMSEntities();
            _phieuKiemKhoRepo = new PhieuKiemKhoRepository(dbContext);
            _nhanVienRepo = new NhanVienRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _phieuBanHangRepo = new PhieuBanHangRepository(dbContext);
            _chiTietPhieuBanHangRepo = new ChiTietPhieuBanHangRepository(dbContext);
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
            _nhanVienBus = new NhanVienBusiness();
        }

        public List<Object> LoadSanhSachHangHoa()
        {
            var list = (from hanghoa in dbContext.HangHoas
                        where (hanghoa.TrangThai == true && hanghoa.SoLuongTon > 0)
                        select new SelectListItem
                        {
                            Text = hanghoa.TenHangHoa,
                            Value = hanghoa.MaHangHoa.ToString(),
                        }).Distinct().ToList();

            return new List<Object>(list);
        }

        public List<Object> LoadSanhSachModelName()
        {
            var list = (from hanghoa in dbContext.HangHoas
                        //where (hanghoa.TrangThai == true && hanghoa.SoLuongTon > 0)
                        select new SelectListItem
                        {
                            //Text = hanghoa.TenHangHoa,
                            Text = hanghoa.ModelName,
                            Value = hanghoa.MaHangHoa.ToString(),
                        }).Distinct().ToList();

            return new List<Object>(list);
        }

        public List<Object> LoadSanhSachHangHoaKho()
        {
            var list = (from hanghoa in dbContext.HangHoas
                        where (hanghoa.TrangThai == true)
                        select new SelectListItem
                        {
                            Text = hanghoa.TenHangHoa,
                            Value = hanghoa.MaHangHoa.ToString(),
                        }).Distinct().ToList();

            return new List<Object>(list);
        }

        public Object LayThongTinHangHoa(int maHangHoa)
        {
            var producInfor = from hanghoa in dbContext.HangHoas
                              where (hanghoa.MaHangHoa == maHangHoa && hanghoa.TrangThai == true)
                              select new
                              {
                                  hanghoa.TenHangHoa,
                                  hanghoa.DonViTinh,
                                  hanghoa.SoLuongTon,
                                  hanghoa.GiaBan,
                                  hanghoa.GiamGia,
                                  hanghoa.ModelName
                              };
            return producInfor;
        }

        public List<string> ListName(string keyword)
        {
            return dbContext.HangHoas.Where(x => x.TenHangHoa.Contains(keyword)).Select(x => x.TenHangHoa).ToList();
        }

        public IEnumerable<HangHoa> DanhSachHangHoaMoiNhat()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.TrangThai == true) && (hanghoa.GiamGia == 0 && hanghoa.GiaBan > 0)
                   orderby hanghoa.MaHangHoa descending
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       XuatXu = hanghoa.XuatXu,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       XuatXu = x.XuatXu,
                   }).Take(8).ToList();
            return all;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaBanChayNhat()
        {
            var orders = (from od in _chiTietPhieuBanHangRepo.GetAll().GroupBy(m => m.MaHangHoa)
                          join hanghoa in _hangHoaRepo.GetAll()
                          on od.Key equals hanghoa.MaHangHoa
                          where (hanghoa.TrangThai == true)
                          select new
                          {
                              MaHangHoa = od.Key,
                              SoLuong = od.Sum(m => m.SoLuong),
                              HinhAnh = hanghoa.HinhAnh,
                              TenHangHoa = hanghoa.TenHangHoa,
                              GiaBan = hanghoa.GiaBan,
                              GiamGia = hanghoa.GiamGia,
                              XuatXu = hanghoa.XuatXu,

                          }).AsEnumerable().Select(x => new HangHoa()
                          {
                              MaHangHoa = x.MaHangHoa,
                              SoLuongTon = x.SoLuong,
                              TenHangHoa = x.TenHangHoa,
                              HinhAnh = x.HinhAnh,
                              GiamGia = x.GiamGia,
                              GiaBan = x.GiaBan,
                              XuatXu = x.XuatXu,
                          }).Distinct().Take(8).ToList();

            return orders;
        }

        public IEnumerable<HangHoa> DanhSachHangHoaGiamGia()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.TrangThai == true && hanghoa.GiamGia > 0)
                   orderby hanghoa.MaHangHoa descending
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       XuatXu = hanghoa.XuatXu,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       XuatXu = x.XuatXu,
                   }).Take(8).ToList();
            return all;
        }

        public IEnumerable<HangHoa> SanPhamKhuyenMai()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.TrangThai == true && hanghoa.GiamGia > 0)
                   orderby hanghoa.MaHangHoa descending
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       XuatXu = hanghoa.XuatXu,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       XuatXu = x.XuatXu,
                   }).ToList();
            return all;
        }

        public IList<HangHoa> TimKiemHangHoa(string key)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where hanghoa.TenHangHoa.ToLower().Contains(key)
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       XuatXu = hanghoa.XuatXu,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       XuatXu = x.XuatXu,
                   }).ToList();
            return all;
        }

        public IEnumerable<HangHoa> LoadHangHoaTheoMa(int maHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoa> all = new List<HangHoa>();

            all = (from hanghoa in danhSachHangHoa
                   where (hanghoa.MaHangHoa.Equals(maHangHoa) && hanghoa.TrangThai == true)
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       SoLuongTon = hanghoa.SoLuongTon,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                       Mota = hanghoa.MoTa,
                   }).AsEnumerable().Select(x => new HangHoa()
                   {
                       MaHangHoa = x.MaHangHoa,
                       TenHangHoa = x.TenHangHoa,
                       HinhAnh = x.HinhAnh,
                       GiaBan = x.GiaBan,
                       GiamGia = x.GiamGia,
                       ThongSoKyThuat = x.ThongSoKyThuat,
                       SoLuongTon = x.SoLuongTon,
                       ThoiGianBaoHanh = x.ThoiGianBaoHanh,
                       MoTa = x.Mota,
                   }).ToList();
            return all;
        }

        public IList<HangHoaViewModel> DanhSachHangHoaTheoMaLoaiHangHoa(int maLoaiHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa) && hanghoa.TrangThai == true)
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       HinhAnh = hanghoa.HinhAnh,
                       GiaBan = hanghoa.GiaBan,
                       GiaKhuyenMai = hanghoa.GiamGia,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                       XuatXu = hanghoa.XuatXu,
                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       hinhAnh = x.HinhAnh,
                       giaBan = x.GiaBan,
                       giamGia = x.GiaKhuyenMai,
                       tenLoaiHangHoa = x.TenLoaiHangHoa,
                       xuatXu = x.XuatXu,
                   }).ToList();
            return all;
        }

        public string TenLoaiHangHoaTheoMaLoaiHangHoa(int maLoaiHangHoa)
        {
            IQueryable<LoaiHangHoa> loaiHangHoa = _loaiHangHoaRepo.GetAll();
            return loaiHangHoa.FirstOrDefault(x => x.MaLoaiHangHoa.Equals(maLoaiHangHoa)).TenLoaiHangHoa;
        }
     
        //Đúng rồi
        public bool CapNhatHangHoaKhiTaoPhieuNhap(int maHangHoa, int soLuongNhap, decimal giaNhap)
        {
            try
            {
                var loinhuan = from loaihanghoa in dbContext.LoaiHangHoas
                               join hanghoa in _hangHoaRepo.GetAll()
                               on loaihanghoa.MaLoaiHangHoa equals hanghoa.MaLoaiHangHoa
                               where hanghoa.MaHangHoa.Equals(maHangHoa)
                               select new
                               {
                                    loaihanghoa.PhanTramLoiNhuan
                               };
                //int phantramloinhuan = loinhuan.FirstOrDefault().PhanTramLoiNhuan;

                decimal phantramloinhuan = loinhuan.FirstOrDefault().PhanTramLoiNhuan;

                decimal phantram = 1 + phantramloinhuan / 100;

                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);
         
                if (result != null)
                {
                    decimal giaChuaTinhLoiNhuan = Math.Round(result.GiaBan / phantram);

                    decimal giaBinhQuan = Math.Round((result.SoLuongTon * giaChuaTinhLoiNhuan + soLuongNhap * giaNhap) / (result.SoLuongTon + soLuongNhap));
                    result.GiaBan = Math.Round(giaBinhQuan + giaBinhQuan * phantramloinhuan / 100);
                    result.SoLuongTon += soLuongNhap;
                    dbContext.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiXoaPhieuNhap(int soPhieuNhap, int maHangHoa, int soLuongNhap, decimal giaNhap)
        {
            try
            {
                var loinhuan = from loaihanghoa in dbContext.LoaiHangHoas
                               join hanghoa in _hangHoaRepo.GetAll()
                               on loaihanghoa.MaLoaiHangHoa equals hanghoa.MaLoaiHangHoa
                               where hanghoa.MaHangHoa.Equals(maHangHoa)
                               select new
                               {
                                   loaihanghoa.PhanTramLoiNhuan
                               };
                decimal phantramloinhuan = loinhuan.FirstOrDefault().PhanTramLoiNhuan;

                decimal phantram = 1 + phantramloinhuan / 100;

                var result = dbContext.ChiTietPhieuNhaps.FirstOrDefault(x => x.SoPhieuNhap == soPhieuNhap && x.MaHangHoa == maHangHoa);

                var a = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);

                if (result != null)
                {
                    int sl = a.SoLuongTon - soLuongNhap;
                    if(sl != 0)
                    {
                        decimal giaChuaTinhLoiNhuan = Math.Round(a.GiaBan / phantram);

                        a.GiaBan = phantram * Math.Round((giaChuaTinhLoiNhuan * (a.SoLuongTon) - giaNhap * soLuongNhap) / (a.SoLuongTon - soLuongNhap));

                        a.SoLuongTon = a.SoLuongTon - soLuongNhap;

                        dbContext.SaveChanges();
                    }
                    else
                    {
                        a.GiaBan = 0;
                        a.SoLuongTon = 0;
                        dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiTaoPhieuBanHang(int maHangHoa, int soLuongXuat)
        {
            try
            {
                var a = _hangHoaRepo.GetAll().Where(x => x.MaHangHoa == maHangHoa);
                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);

                if (result != null)
                {
                    result.SoLuongTon -= soLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int LaySoLuongTonCuoiCuaThangTruoc(int maHangHoa, int thang, int nam)
        {
            if (thang == 1)
            {
                var result = dbContext.BaoCaoTonKhoes.FirstOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == 12 && x.Nam == (nam - 1));
                if (result != null)
                {
                    return result.SoLuongTonCuoi;
                }
                else
                {
                    return 0;
                }
            }

            var result1 = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == (thang - 1) && x.Nam == nam);
            if (result1 != null)
            {
                return result1.SoLuongTonCuoi;
            }
            else
            {
                return 0;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuBanHang(int maHangHoa, int soLuongXuat, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongXuat += soLuongXuat;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                else
                {
                    BaoCaoTonKho baoCaoTonKho = new BaoCaoTonKho
                    {
                        Thang = thang,
                        Nam = nam,
                        MaHangHoa = maHangHoa,
                        SoLuongTonDau = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam),
                        SoLuongNhap = 0,
                        SoLuongXuat = soLuongXuat,
                        SoLuongTonCuoi = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam) + 0 - soLuongXuat,
                    };

                    dbContext.BaoCaoTonKhoes.Add(baoCaoTonKho);
                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiTaoPhieuNhap(int maHangHoa, int soLuongNhap, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongNhap += soLuongNhap;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                else
                {
                    BaoCaoTonKho baoCaoTonKho = new BaoCaoTonKho
                    {
                        Thang = thang,
                        Nam = nam,
                        MaHangHoa = maHangHoa,
                        SoLuongTonDau = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam),
                        SoLuongNhap = soLuongNhap,
                        SoLuongXuat = 0,
                        SoLuongTonCuoi = LaySoLuongTonCuoiCuaThangTruoc(maHangHoa, thang, nam) + soLuongNhap - 0,
                    };

                    dbContext.BaoCaoTonKhoes.Add(baoCaoTonKho);
                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaKhiXoaPhieuBanHang(int maHangHoa, int soLuongXuat)
        {
            try
            {
                var result = dbContext.HangHoas.FirstOrDefault(x => x.MaHangHoa == maHangHoa);
                if (result != null)
                {
                    result.SoLuongTon += soLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuBanHang(int maHangHoa, int soLuongXuat, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongXuat -= soLuongXuat;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CapNhatHangHoaVaoBaoCaoTonKhoKhiXoaPhieuNhap(int maHangHoa, int soLuongNhap, int thang, int nam)
        {
            try
            {
                var result = dbContext.BaoCaoTonKhoes.SingleOrDefault(x => x.MaHangHoa == maHangHoa && x.Thang == thang && x.Nam == nam);
                if (result != null)
                {
                    result.SoLuongNhap -= soLuongNhap;
                    result.SoLuongTonCuoi = result.SoLuongTonDau + result.SoLuongNhap - result.SoLuongXuat;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public HangHoa ViewDetail(int id)
        {
            return dbContext.HangHoas.Find(id);
        }
        public IList<HangHoaViewModel> SearchDanhSachHangHoa(String key, string trangthai, string maloaihanghoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.TrangThai.ToString().Equals(trangthai)
                        || hanghoa.MaLoaiHangHoa.ToString().Equals(maloaihanghoa)
                        || hanghoa.TenHangHoa.ToString().Contains(key)
                        || hanghoa.XuatXu.ToString().Contains(key))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       SoLuongTon = hanghoa.SoLuongTon,
                       DonViTinh = hanghoa.DonViTinh,
                       MoTa = hanghoa.MoTa,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       XuatXu = hanghoa.XuatXu,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                       HinhAnh = hanghoa.HinhAnh,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                       TrangThai = hanghoa.TrangThai,

                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       giaBan = x.GiaBan,
                       giamGia = x.GiamGia,
                       soLuongTon = x.SoLuongTon,
                       donViTinh = x.DonViTinh,
                       moTa = x.MoTa,
                       thongSoKyThuat = x.ThongSoKyThuat,
                       xuatXu = x.XuatXu,
                       thoiGianBaoHanh = x.ThoiGianBaoHanh,
                       hinhAnh = x.HinhAnh,
                       tenLoaiHangHoa = x.TenLoaiHangHoa,
                       trangThai = x.TrangThai
                   }).OrderByDescending(x => x.maHangHoa).ToList();
            return all;
        }
        public IEnumerable<HangHoaViewModel> LoadDanhSachHangHoa()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            List<HangHoaViewModel> all = new List<HangHoaViewModel>();

            all = (from hanghoa in danhSachHangHoa
                   join loaihanghoa in _loaiHangHoaRepo.GetAll()
                   on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                   where (hanghoa.TrangThai.Equals(true))
                   select new
                   {
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       GiaBan = hanghoa.GiaBan,
                       GiamGia = hanghoa.GiamGia,
                       SoLuongTon = hanghoa.SoLuongTon,
                       DonViTinh = hanghoa.DonViTinh,
                       MoTa = hanghoa.MoTa,
                       ThongSoKyThuat = hanghoa.ThongSoKyThuat,
                       XuatXu = hanghoa.XuatXu,
                       ThoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                       HinhAnh = hanghoa.HinhAnh,
                       TrangThai = hanghoa.TrangThai,
                       TenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                   }).AsEnumerable().Select(x => new HangHoaViewModel()
                   {
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       giaBan = x.GiaBan,
                       giamGia = x.GiamGia,
                       soLuongTon = x.SoLuongTon,
                       donViTinh = x.DonViTinh,
                       moTa = x.MoTa,
                       thongSoKyThuat = x.ThongSoKyThuat,
                       xuatXu = x.XuatXu,
                       thoiGianBaoHanh = x.ThoiGianBaoHanh,
                       hinhAnh = x.HinhAnh,
                       trangThai = x.TrangThai,
                       tenLoaiHangHoa = x.TenLoaiHangHoa
                   }).OrderByDescending(x => x.maHangHoa).ToList();
            return all;

        }
        public async Task Create(object model)
        {
            var hangHoa = new HangHoa();
            HangHoaViewModel input = (HangHoaViewModel)model;

            hangHoa.TenHangHoa = input.tenHangHoa;
            hangHoa.ModelName = input.modelName;
            hangHoa.DonViTinh = input.donViTinh;
            hangHoa.MaLoaiHangHoa = input.maLoaiHangHoa;
            hangHoa.XuatXu = input.xuatXu;
            hangHoa.ThoiGianBaoHanh = input.thoiGianBaoHanh;
            hangHoa.MoTa = input.moTa;
            hangHoa.ThongSoKyThuat = input.thongSoKyThuat;
            hangHoa.HinhAnh = input.hinhAnh;
            hangHoa.TrangThai = true;

            await _hangHoaRepo.InsertAsync(hangHoa);
        }
        public IEnumerable<HangHoaViewModel> LoadDanhSachHangHoaTheoMa(int maHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();

            var all = (from hanghoa in danhSachHangHoa
                       join loaihanghoa in _loaiHangHoaRepo.GetAll()
                       on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                       where (hanghoa.MaHangHoa.Equals(maHangHoa))
                       select new HangHoaViewModel
                       {
                           maHangHoa = hanghoa.MaHangHoa,
                           tenHangHoa = hanghoa.TenHangHoa,
                           giaBan = hanghoa.GiaBan,
                           giamGia = hanghoa.GiamGia,
                           soLuongTon = hanghoa.SoLuongTon,
                           donViTinh = hanghoa.DonViTinh,
                           moTa = hanghoa.MoTa,
                           thongSoKyThuat = hanghoa.ThongSoKyThuat,
                           xuatXu = hanghoa.XuatXu,
                           thoiGianBaoHanh = hanghoa.ThoiGianBaoHanh,
                           hinhAnh = hanghoa.HinhAnh,
                           tenLoaiHangHoa = loaihanghoa.TenLoaiHangHoa,
                           trangThai = hanghoa.TrangThai,
                           modelName = hanghoa.ModelName,
                       }).ToList();

            return all;
        }
        public async Task<object> Find(int ID)
        {
            return await _hangHoaRepo.GetByIdAsync(ID);
        }
        public async Task Update(object inputModel, object editModel)
        {
            HangHoaViewModel input = (HangHoaViewModel)inputModel;
            HangHoa editHangHoa = (HangHoa)editModel;

            editHangHoa.TenHangHoa = input.tenHangHoa;
            editHangHoa.ModelName = input.modelName;
            editHangHoa.GiaBan = input.giaBan;
            editHangHoa.GiamGia = input.giamGia;
            editHangHoa.DonViTinh = input.donViTinh;
            editHangHoa.MoTa = input.moTa;
            editHangHoa.ThongSoKyThuat = input.thongSoKyThuat;
            editHangHoa.XuatXu = input.xuatXu;
            editHangHoa.ThoiGianBaoHanh = input.thoiGianBaoHanh;
            editHangHoa.HinhAnh = input.hinhAnh;
            editHangHoa.MaLoaiHangHoa = input.maLoaiHangHoa;
            editHangHoa.TrangThai = input.trangThai;

            await _hangHoaRepo.EditAsync(editHangHoa);
        }

        public async Task Delete(object editModel)
        {
            HangHoa editHangHoa = (HangHoa)editModel;

            editHangHoa.TrangThai = false;

            await _hangHoaRepo.EditAsync(editHangHoa);
        }

        public object SanPhamHetHang()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       where hanghoa.SoLuongTon.Equals(0) && hanghoa.TrangThai.Equals(true)
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Count();
            return all;
        }

        public object SanPhamSapHetHang()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       where hanghoa.SoLuongTon < 5 && hanghoa.TrangThai.Equals(true)
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Count();
            return all;
        }

        public object SanPhamDangKinhDoanh()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       where hanghoa.TrangThai.Equals(true)
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Count();
            return all;
        }

        public object SanPhamNgungKinhDoanh()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       where hanghoa.TrangThai.Equals(false)
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Count();
            return all;
        }

        public object TongSanPham()
        {
            return _hangHoaRepo.GetAll().Count();
        }

        public object SanPhamBanChayNhat()
        {
            IQueryable<ChiTietPhieuBanHang> danhSachPhieuBanHang = _chiTietPhieuBanHangRepo.GetAll();
            IQueryable<HangHoa> hangHoa = _hangHoaRepo.GetAll();
            var query =
                          (from p in hangHoa
                           let totalQuantity = (from op in danhSachPhieuBanHang
                                                where op.MaHangHoa == p.MaHangHoa && p.TrangThai.Equals(true)
                                                select op.SoLuong).Sum()
                           where totalQuantity > 0
                           orderby totalQuantity descending
                           select p).Take(15).ToList();
            return query;
        }

        public object TongSanPhamTheoLoaiHang(int maLoaiHangHoa)
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       join loaihanghoa in _loaiHangHoaRepo.GetAll()
                       on hanghoa.MaLoaiHangHoa equals loaihanghoa.MaLoaiHangHoa
                       where hanghoa.TrangThai.Equals(true) && hanghoa.MaLoaiHangHoa.Equals(maLoaiHangHoa)
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Distinct().Count();
            return all;
        }

        public object TongSanPhamKhuyenMai()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       where hanghoa.TrangThai.Equals(true) && hanghoa.GiamGia > 0
                       select new
                       {
                           MaHangHoa = hanghoa.MaHangHoa,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           MaHangHoa = x.MaHangHoa,
                       }).Distinct().Count();
            return all;
        }

        public IEnumerable<HangHoa> GetAllModelName()
        {
            IQueryable<HangHoa> danhSachHangHoa = _hangHoaRepo.GetAll();
            var all = (from hanghoa in danhSachHangHoa
                       select new
                       {
                           ModelName = hanghoa.ModelName,
                       }).AsEnumerable().Select(x => new HangHoa()
                       {
                           ModelName = x.ModelName,
                       }).ToList();
            return all;
        }
    }
}