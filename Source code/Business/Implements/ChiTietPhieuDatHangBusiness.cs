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
    public class ChiTietPhieuDatHangBusiness
    {
        SMSEntities dbContext;
        private readonly ChiTietPhieuDatHangRepository _chiTietPhieuDatHangRepo;
        private readonly HangHoaRepository _hangHoaRepo;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;

        public ChiTietPhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();
            _chiTietPhieuDatHangRepo = new ChiTietPhieuDatHangRepository(dbContext);
            _hangHoaRepo = new HangHoaRepository(dbContext);
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
        }

        public bool Insert(ChiTietPhieuDatHang detail)
        {
            try
            {
                dbContext.ChiTietPhieuDatHangs.Add(detail);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public IList<ChiTietPhieuDatHangViewModel> danhSachPhieuDatHangTheoMa(int soPhieuDatHang)
        {
            IQueryable<ChiTietPhieuDatHang> dsChiTietPhieuDatHang = _chiTietPhieuDatHangRepo.GetAll();
            List<ChiTietPhieuDatHangViewModel> all = new List<ChiTietPhieuDatHangViewModel>();


            all = (from chitietphieudathang in dsChiTietPhieuDatHang
                   join hanghoa in _hangHoaRepo.GetAll()
                   on chitietphieudathang.MaHangHoa equals hanghoa.MaHangHoa
                   select new
                   {
                       SoPhieuDatHang = chitietphieudathang.SoPhieuDatHang,
                       MaHangHoa = chitietphieudathang.MaHangHoa,
                       SoLuong = chitietphieudathang.SoLuong,
                       Gia = chitietphieudathang.Gia,
                       ThanhTien = chitietphieudathang.ThanhTien,
                       tenHangHoa = hanghoa.TenHangHoa,
                   }).AsEnumerable().Select(x => new ChiTietPhieuDatHangViewModel()
                   {
                       soPhieuDatHang = x.SoPhieuDatHang,
                       maHangHoa = x.MaHangHoa,
                       soLuong = x.SoLuong,
                       gia = x.Gia,
                       thanhTien = x.ThanhTien,
                       tenHangHoa = x.tenHangHoa,
                   }).ToList();

            var information = (from i in all
                               where (i.soPhieuDatHang == soPhieuDatHang)
                               select i).ToList();

            return information.ToList();
        }
    }
}
