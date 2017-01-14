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
    public class ChiTietPhieuKiemKhoBusiness
    {
        private readonly ChiTietPhieuKiemKhoRepository _chiTietPhieuKiemKhoRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;

        public ChiTietPhieuKiemKhoBusiness()
        {
            SMSEntities dbContext = new SMSEntities();
            _chiTietPhieuKiemKhoRepo = new ChiTietPhieuKiemKhoRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
        }

         public IList<ChiTietPhieuKiemKhoViewModel> danhSachPhieuKiemKhoTheoMa(int soPhieuKiemKho)
        {
            IQueryable<ChiTietPhieuKiemKho> dsChiTietPhieuKiemKho = _chiTietPhieuKiemKhoRepo.GetAll();
            List<ChiTietPhieuKiemKhoViewModel> all = new List<ChiTietPhieuKiemKhoViewModel>();
            all = (from chitietphieukiemkho in dsChiTietPhieuKiemKho
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
                   }).AsEnumerable().Select(x => new ChiTietPhieuKiemKhoViewModel()
                   {
                       soPhieuKiemKho = x.SoPhieuKiemKho,
                       maHangHoa = x.MaHangHoa,
                       soLuongHienTai = x.SoLuongHienTai,
                       soLuongKiemTra = x.SoLuongKiemTra,
                       tenHangHoa = x.TenHangHoa,
                       donViTinh = x.DonViTinh,
                   }).ToList();

            var information = (from i in all
                               where (soPhieuKiemKho == null || i.soPhieuKiemKho == soPhieuKiemKho)
                               select i).ToList();
            return information.ToList();
        }
    }
}
