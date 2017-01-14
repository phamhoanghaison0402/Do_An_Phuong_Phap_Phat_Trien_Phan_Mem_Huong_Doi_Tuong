using Business.Interfaces;
using Common.Models;
using Common.Ultil;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Business.Implements
{
    public class ChucVuBusiness : IChucVuBusiness
    {
        private SMSEntities _dbContext;
        private readonly ChucVuRepository _chucVuRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly NhanVien_QuyenRepository _nhanVienQuyenRepo;
        private readonly PhanQuyenRepository _phanQuyenRepo;

        public ChucVuBusiness()
        {
            _dbContext = new SMSEntities();
            _chucVuRepo = new ChucVuRepository(_dbContext);
            _nhanVienQuyenRepo = new NhanVien_QuyenRepository(_dbContext);
            _phanQuyenRepo = new PhanQuyenRepository(_dbContext);
            _nhanVienRepo = new NhanVienRepository(_dbContext);
        }

        /// <summary>
        /// Get menu from positionID
        /// </summary>
        /// <param name="positionID">positionID</param>
        /// <returns>list menu</returns>
        public List<NhanVien_QuyenViewModel> GetMenu(int? maChucVu)
        {
            if (maChucVu != null)
            {
                var lstMenu = (from nhanvienquyen in _nhanVienQuyenRepo.SearchFor(i => i.MaChucVu == maChucVu)
                               join phanquyen in _phanQuyenRepo.GetAll()
                               on nhanvienquyen.MaQuyen equals phanquyen.MaQuyen
                               select new NhanVien_QuyenViewModel
                               {
                                   maChucVu = nhanvienquyen.MaChucVu,
                                   maQuyen = nhanvienquyen.MaQuyen,
                                   parent = nhanvienquyen.ChuThich,
                                   tenQuyen = phanquyen.TenQuyen,
                               }).ToList();
                foreach (var item in lstMenu)
                {
                    item.controller = FindController.Controller(item.tenQuyen);
                }
                return new List<NhanVien_QuyenViewModel>(lstMenu);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get parent of child menu
        /// </summary>
        /// <param name="positionID">positionID</param>
        /// <returns>List<String></returns>
        public List<String> GetListParent(int? maChucvu)
        {
            if (maChucvu != null)
            {
                return _nhanVienQuyenRepo.SearchFor(i => i.MaChucVu == maChucvu).Select(x => x.ChuThich).Distinct().ToList(); ;
            }
            else
            {
                return null;
            }
        }

        public List<Object> LoadChucVu()
        {
            IQueryable<ChucVu> dsChucVu = _chucVuRepo.GetAll();
            int[] IDs= {4,5,6,7};
            var list = (from chucvu in dsChucVu
                        where IDs.Contains(chucvu.MaChucVu)
                        select new SelectListItem
                        {
                            Text = chucvu.TenChucVu,
                            Value = chucvu.MaChucVu.ToString(),
                        });
            return new List<Object>(list);
        }

        public List<Object> LoadChucVuTheoMaNhanVien(string maNhanVien)
        {
            IQueryable<ChucVu> dsChucVu = _chucVuRepo.GetAll();

            var list = (from chucvu in dsChucVu
                        join nhanvien in _nhanVienRepo.GetAll()
                        on chucvu.MaChucVu equals nhanvien.MaChucVu
                        where (nhanvien.MaNhanVien.Equals(maNhanVien))
                        select new SelectListItem
                        {
                            Text = chucvu.TenChucVu,
                            Value = chucvu.MaChucVu.ToString(),
                        });
            return new List<Object>(list);
        }

    }
}
