using Common.Models;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.Implements
{
    public class NhaCungCapBusiness
    {
        private SMSEntities _dbContext;
        private readonly NhaCungCapRepository _nhaCungCapRepo;

        public NhaCungCapBusiness()
        {
            _dbContext = new SMSEntities();
            _nhaCungCapRepo = new NhaCungCapRepository(_dbContext);
        }

        public List<Object> LoadNhaCungCap()
        {
            IQueryable<NhaCungCap> dsChucVu = _nhaCungCapRepo.GetAll();

            var list = (from chucvu in dsChucVu
                        select new SelectListItem
                        {
                            Text = chucvu.TenNhaCungCap,
                            Value = chucvu.MaNhaCungCap.ToString(),
                        });
            return new List<Object>(list);
        }
        public IList<NhaCungCap> SearchDanhSachNhaCungCap(string key)
        {          
            IQueryable<NhaCungCap> danhSachNhaCungCap = _nhaCungCapRepo.GetAll();

            //Find by keyword
            if (!string.IsNullOrEmpty(key))
            {
                danhSachNhaCungCap = from nhacungcap in danhSachNhaCungCap
                                     where (nhacungcap.TenNhaCungCap.Contains(key)
                                  || nhacungcap.SoDienThoai.Contains(key)
                                  || nhacungcap.Email.Contains(key)
                                  || nhacungcap.DiaChi.Contains(key))
                                     select nhacungcap;
            }

            return danhSachNhaCungCap.OrderByDescending(x => x.MaNhaCungCap).ToList();
        }

        public IList<NhaCungCap> LoadDanhSachNhaCungCapTheoMa(int maNhaCungCap)
        {
            IQueryable<NhaCungCap> danhSachNhaCungCap = _nhaCungCapRepo.SearchFor(i => i.MaNhaCungCap == maNhaCungCap);
            return danhSachNhaCungCap.ToList();
        }

        public async Task<object> Find(int ID)
        {
            return await _nhaCungCapRepo.GetByIdAsync(ID);
        }

        public async Task Create(object model)
        {
            var nhaCungCap = new NhaCungCap();
            NhaCungCap input = (NhaCungCap)model;

          //  nhaCungCap.NhaCungCapCode = "a";
            nhaCungCap.TenNhaCungCap = input.TenNhaCungCap;
            nhaCungCap.DiaChi = input.DiaChi;
            nhaCungCap.SoDienThoai = input.SoDienThoai;
            nhaCungCap.Email = input.Email;

            await _nhaCungCapRepo.InsertAsync(nhaCungCap);
        }

        public async Task Update(object inputModel, object editModel)
        {
            NhaCungCap input = (NhaCungCap)inputModel;
            NhaCungCap editNhaCungCap = (NhaCungCap)editModel;

            editNhaCungCap.TenNhaCungCap = input.TenNhaCungCap;
            editNhaCungCap.DiaChi = input.DiaChi;
            editNhaCungCap.Email = input.Email;
            editNhaCungCap.SoDienThoai = input.SoDienThoai;

            await _nhaCungCapRepo.EditAsync(editNhaCungCap);
        }
    }
}
