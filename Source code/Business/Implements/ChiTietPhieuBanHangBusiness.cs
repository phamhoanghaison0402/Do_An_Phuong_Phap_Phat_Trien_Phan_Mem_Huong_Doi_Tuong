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
    public class ChiTietPhieuBanHangBusiness
    {
        private readonly ChiTietPhieuBanHangRepository _chiTietPhieuBanHangRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;

        public ChiTietPhieuBanHangBusiness()
        {
            SMSEntities dbContext = new SMSEntities();
            _chiTietPhieuBanHangRepo = new ChiTietPhieuBanHangRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
        }

        public IList<ChiTietPhieuBanHangViewModel> danhSachPhieuBanHangTheoMa(int soPhieuBanHang)
        {
            IQueryable<ChiTietPhieuBanHang> dsChiTietPhieuBanHang = _chiTietPhieuBanHangRepo.GetAll();
            List<ChiTietPhieuBanHangViewModel> all = new List<ChiTietPhieuBanHangViewModel>();

            all = (from chitietphieubanhang in dsChiTietPhieuBanHang
                   join hanghoa in _hangHoaRepo.GetAll()
                   on chitietphieubanhang.MaHangHoa equals hanghoa.MaHangHoa
                   select new
                   {
                       SoPhieuBanHang = chitietphieubanhang.SoPhieuBanHang,
                       MaHangHoa = chitietphieubanhang.MaHangHoa,
                       SoLuong = chitietphieubanhang.SoLuong,
                       Gia = chitietphieubanhang.Gia,
                       ThanhTien = chitietphieubanhang.ThanhTien,
                       tenHangHoa = hanghoa.TenHangHoa,
                   }).AsEnumerable().Select(x => new ChiTietPhieuBanHangViewModel()
                   {
                       soPhieuBanHang = x.SoPhieuBanHang,
                       maHangHoa = x.MaHangHoa,
                       soLuong = x.SoLuong,
                       gia = x.Gia,
                       thanhTien = x.ThanhTien,
                       tenHangHoa = x.tenHangHoa,
                   }).ToList();

            var information = (from i in all
                               where (soPhieuBanHang == null || i.soPhieuBanHang == soPhieuBanHang)
                               select i).ToList();
            return information.ToList();
        }

        public async Task<object> Find(int ID)
        {
            return await _chiTietPhieuBanHangRepo.GetByIdAsync(ID);
        }

        public async Task Delete(object deleteModel)
        {
            ChiTietPhieuBanHang xoaPhieuBanHang = (ChiTietPhieuBanHang)deleteModel;

            await _chiTietPhieuBanHangRepo.DeleteAsync(xoaPhieuBanHang);
        }
    }
}
