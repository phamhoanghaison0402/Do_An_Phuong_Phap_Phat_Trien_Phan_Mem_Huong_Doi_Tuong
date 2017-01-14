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
    public class ChiTietPhieuXuatKhoBusiness
    {
        private readonly ChiTietPhieuXuathoRepository _chiTietPhieuXuatKhoRepo;
        private readonly HangHoaRepository _hangHoaRepo;       
        FormatNumber format = new FormatNumber();

        public ChiTietPhieuXuatKhoBusiness()
        {
            SMSEntities dbContext = new SMSEntities();
            _chiTietPhieuXuatKhoRepo = new ChiTietPhieuXuathoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);         
        }

        public IList<ChiTietPhieuXuatKhoViewModel> DanhSachPhieuXuatKhoTheoMa(int soPhieuXuatKho)
        {
            IQueryable<ChiTietPhieuXuatKho> dsChiTietPhieuNhapKho = _chiTietPhieuXuatKhoRepo.GetAll();
            List<ChiTietPhieuXuatKhoViewModel> all = new List<ChiTietPhieuXuatKhoViewModel>();
            all = (from chitietphieuxuatkho in dsChiTietPhieuNhapKho
                   join hanghoa in _hangHoaRepo.GetAll()
                   on chitietphieuxuatkho.MaHangHoa equals hanghoa.MaHangHoa            
                   select new
                   {
                       SoPhieuKiemKho = chitietphieuxuatkho.SoPhieuXuatKho,
                       MaHangHoa = hanghoa.MaHangHoa,
                       TenHangHoa = hanghoa.TenHangHoa,
                       DonViTinh = hanghoa.DonViTinh,
                       SoLuong = chitietphieuxuatkho.SoLuong,
                       Gia = chitietphieuxuatkho.Gia,
                       ThanhTien = chitietphieuxuatkho.ThanhTien,

                   }).AsEnumerable().Select(x => new ChiTietPhieuXuatKhoViewModel()
                   {
                       soPhieuXuatKho = x.SoPhieuKiemKho,
                       maHangHoa = x.MaHangHoa,
                       tenHangHoa = x.TenHangHoa,
                       donViTinh = x.DonViTinh,
                       soLuong = x.SoLuong,
                       gia = x.Gia,
                       thanhTien = x.ThanhTien,
                   }).ToList();

            var information = (from i in all
                               where (soPhieuXuatKho == null || i.soPhieuXuatKho == soPhieuXuatKho)
                               select i).ToList();
            return information.ToList();
        }
    }
}
