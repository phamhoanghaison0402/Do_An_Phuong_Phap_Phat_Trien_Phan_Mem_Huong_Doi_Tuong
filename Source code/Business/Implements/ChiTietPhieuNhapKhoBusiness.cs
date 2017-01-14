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
    public class ChiTietPhieuNhapKhoBusiness
    {
        private readonly ChiTietPhieuNhapKhoRepository _chiTietPhieuNhapRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        FormatNumber format = new FormatNumber();

        public ChiTietPhieuNhapKhoBusiness()
        {
            SMSEntities dbContext = new SMSEntities();
            _chiTietPhieuNhapRepo = new ChiTietPhieuNhapKhoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
        }

        public IList<ChiTietPhieuNhapKhoViewModel> DanhSachPhieuNhapKhoTheoMa(int soPhieuNhapKho)
        {
            IQueryable<ChiTietPhieuNhap> dsChiTietPhieuNhapKho = _chiTietPhieuNhapRepo.GetAll();
            List<ChiTietPhieuNhapKhoViewModel> all = new List<ChiTietPhieuNhapKhoViewModel>();
            all = (from chitietphieunhapkho in dsChiTietPhieuNhapKho
                   join hanghoa in _hangHoaRepo.GetAll()
                   on chitietphieunhapkho.MaHangHoa equals hanghoa.MaHangHoa               
                   select new
                   {
                       SoPhieuKiemKho = chitietphieunhapkho.SoPhieuNhap,
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       DonViTinh = hanghoa.DonViTinh,
                       SoLuong = chitietphieunhapkho.SoLuong,
                       GiaNhap = chitietphieunhapkho.GiaNhap,
                       ThanhTien = chitietphieunhapkho.ThanhTien,
                   }).AsEnumerable().Select(x => new ChiTietPhieuNhapKhoViewModel()
                   {                      
                       soPhieuNhapKho = x.SoPhieuKiemKho,
                       maHangHoa = x.MaHangHoa,                      
                       tenHangHoa = x.TenHangHoa,
                       donViTinh = x.DonViTinh,
                       soLuong = x.SoLuong,
                       giaNhap = x.GiaNhap,
                       thanhTien = x.ThanhTien,
                   }).ToList();
     
            var information = (from i in all
                               where (soPhieuNhapKho == null || i.soPhieuNhapKho == soPhieuNhapKho)
                               select i).ToList();
            return information.ToList();
        }
    }
}
