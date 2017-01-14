using Common.Models;
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
    public class LoaiHangHoaBusiness
    {
        SMSEntities dbContext = null;
        private readonly LoaiHangHoaRepository _loaiHangHoaRepo;

        public LoaiHangHoaBusiness()
        {
            dbContext = new SMSEntities();
            _loaiHangHoaRepo = new LoaiHangHoaRepository(dbContext);
        }

        public IList<LoaiHangHoa> LoadDSLoaiHangHoa()
        {
            IQueryable<LoaiHangHoa> loaiHangHoa = _loaiHangHoaRepo.GetAll();
            return loaiHangHoa.ToList();
        }

        public IList<LoaiHangHoa> LoadDanhSachLoaiHangHoa(string key)
        {
            IQueryable<LoaiHangHoa> danhSachLoaiHangHoa = _loaiHangHoaRepo.GetAll();

            //Find by keyword
            if (!string.IsNullOrEmpty(key))
            {
                danhSachLoaiHangHoa = from loaihanghoa in danhSachLoaiHangHoa
                                      where (loaihanghoa.TenLoaiHangHoa.Contains(key)
                                            || loaihanghoa.PhanTramLoiNhuan.ToString().Contains(key))
                                      select loaihanghoa;
            }
            return danhSachLoaiHangHoa.OrderByDescending(x => x.MaLoaiHangHoa).ToList();
        }

        public async Task Create(object model)
        {
            var loaihanghoa = new LoaiHangHoa();
            LoaiHangHoa input = (LoaiHangHoa)model;

            loaihanghoa.TenLoaiHangHoa = input.TenLoaiHangHoa;
            loaihanghoa.PhanTramLoiNhuan = input.PhanTramLoiNhuan;
           
            await _loaiHangHoaRepo.InsertAsync(loaihanghoa);
        }
        public List<Object> LoadLoaiHangHoa()
        {
            IQueryable<LoaiHangHoa> dsLoaiHangHoa = _loaiHangHoaRepo.GetAll();
            var list = (from loaihanghoa in dsLoaiHangHoa
                        select new SelectListItem
                        {
                            Text = loaihanghoa.TenLoaiHangHoa,
                            Value = loaihanghoa.MaLoaiHangHoa.ToString(),
                        });
            return new List<Object>(list);
        }

        public IList<LoaiHangHoa> LoadDanhSachLoaiHangHoaTheoMa(int maLoaiHangHoa)
        {
            IQueryable<LoaiHangHoa> danhSachLoaiHangHoa = _loaiHangHoaRepo.SearchFor(i => i.MaLoaiHangHoa == maLoaiHangHoa);
            return danhSachLoaiHangHoa.ToList();
        }

        public async Task<object> Find(int ID)
        {
            return await _loaiHangHoaRepo.GetByIdAsync(ID);
        }

        public async Task Update(object inputModel, object editModel)
        {
            LoaiHangHoa input = (LoaiHangHoa)inputModel;
            LoaiHangHoa editLoaiHangHoa = (LoaiHangHoa)editModel;

            editLoaiHangHoa.TenLoaiHangHoa = input.TenLoaiHangHoa;
            editLoaiHangHoa.PhanTramLoiNhuan = input.PhanTramLoiNhuan;

            await _loaiHangHoaRepo.EditAsync(editLoaiHangHoa);
        }

        public object TongLoaiSanPham()
        {
            return _loaiHangHoaRepo.GetAll().Count();
        }
    }
}
