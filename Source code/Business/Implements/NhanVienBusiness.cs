using Common.Models;
using Common.Ultil;
using Data.Base;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using Business.Interfaces;
using System.Web.Mvc;
using Common.ViewModels;
using PagedList;

namespace Business.Implements
{
    public class NhanVienBusiness : INhanVienBusiness
    {
        private SMSEntities _dbContext;
        private readonly ChucVuRepository _chucVuRepo;
        private readonly NhanVienRepository _nhanVienRepo;
        private readonly NhanVien_QuyenRepository _nhanVienQuyenRepo;

        public NhanVienBusiness()
        {
            _dbContext = new SMSEntities();
            _chucVuRepo = new ChucVuRepository(_dbContext);
            _nhanVienRepo = new NhanVienRepository(_dbContext);
            _nhanVienQuyenRepo = new NhanVien_QuyenRepository(_dbContext);
        }

        public NhanVienViewModel Login(string userName, string password)
        {
            NhanVien account = (NhanVien)_nhanVienRepo.SearchFor(i => i.UserName.Equals(userName) && i.PassWord.Equals(password)).SingleOrDefault();
            if (account != null)
            {
                var thongTinNhanVien = (from nhanvien in _nhanVienRepo.GetAll()
                                        join chucvu in _chucVuRepo.GetAll()
                                        on nhanvien.MaChucVu equals chucvu.MaChucVu
                                        orderby nhanvien.MaNhanVien descending
                                        where nhanvien.MaNhanVien.Equals(account.MaNhanVien)
                                        select new NhanVienViewModel
                                        {
                                            maNhanVien = nhanvien.MaNhanVien,
                                            tenNhanVien = nhanvien.TenNhanvien,
                                            diaChi = nhanvien.DiaChi,
                                            soDienThoai = nhanvien.SoDienThoai,
                                            email = nhanvien.Email,
                                            CMND = nhanvien.CMND,
                                            avatar = nhanvien.Avatar,
                                            maChucVu = nhanvien.MaChucVu,
                                            tenChucVu = chucvu.TenChucVu,
                                            trangThai = nhanvien.TrangThai,
                                        }).First();
                return (NhanVienViewModel)thongTinNhanVien;
            }
            else
            {
                return null;
            }
        }
    
        public string Authority(NhanVienViewModel account)
        {
            if (account != null)
            {
                var lstAut = _nhanVienQuyenRepo.SearchFor(i => i.MaChucVu == account.maChucVu);
                string Aut = "";
                if (lstAut.Count() != 0)
                {
                    foreach (var item in lstAut)
                    {
                        //Aut += item.Authority.AuthorityID + ",";
                        Aut += item.MaQuyen + ",";
                    }
                    Aut = Aut.Substring(0, Aut.Length - 1);
                }
                return Aut;
            }
            else
            {
                return null;
            }
        }

        public int? layMaChucVu(String userName)
        {
            IQueryable<NhanVien> danhSachNhanVien = _nhanVienRepo.GetAll();
            return danhSachNhanVien.FirstOrDefault(x => x.UserName.Equals(userName)).MaChucVu;
        }

        public string LoadTenNhanVien(string userName)
        {
            IQueryable<NhanVien> tatCaNhanVien = _nhanVienRepo.GetAll();
            return tatCaNhanVien.FirstOrDefault(x => x.UserName.Equals(userName)).TenNhanvien;
        }

        public int LoadMaNhanVien(string userName)
        {
            IQueryable<NhanVien> tatCaNhanVien = _nhanVienRepo.GetAll();
            return tatCaNhanVien.FirstOrDefault(x => x.UserName.Equals(userName)).MaNhanVien;
        }

        public IList<NhanVienViewModel> SearchDanhSachNhanVien(String key, string trangthai, string machucvu)
        {
            IQueryable<NhanVien> danhSachNhanVien = _nhanVienRepo.GetAll();
            List<NhanVienViewModel> all = new List<NhanVienViewModel>();

            all = (from nhanvien in danhSachNhanVien
                   join chucvu in _chucVuRepo.GetAll()
                   on nhanvien.MaChucVu equals chucvu.MaChucVu
                   where (nhanvien.TrangThai.ToString().Equals(trangthai) 
                        ||chucvu.MaChucVu.ToString().Equals(machucvu)
                        || nhanvien.TenNhanvien.ToString().Contains(key)
                        || nhanvien.SoDienThoai.ToString().Contains(key)
                        || nhanvien.Email.ToString().Contains(key)
                        || nhanvien.DiaChi.ToString().Contains(key)
                        || nhanvien.CMND.ToString().Contains(key))
                   select new
                   {
                       MaNhanVien = nhanvien.MaNhanVien,
                       TenNhanVien = nhanvien.TenNhanvien,
                       DiaChi = nhanvien.DiaChi,
                       SoDienThoai = nhanvien.SoDienThoai,
                       Email = nhanvien.Email,
                       CMND = nhanvien.CMND,
                       TrangThai = nhanvien.TrangThai,
                       TenChucVu = chucvu.TenChucVu,

                   }).AsEnumerable().Select(x => new NhanVienViewModel()
                   {
                       maNhanVien = x.MaNhanVien,
                       tenNhanVien = x.TenNhanVien,
                       diaChi = x.DiaChi,
                       soDienThoai = x.SoDienThoai,
                       email = x.Email,
                       CMND = x.CMND,
                       trangThai = x.TrangThai,
                       tenChucVu = x.TenChucVu,
                   }).ToList();
            return all;
        }   

        public IEnumerable<NhanVienViewModel> LoadDanhSachNhanVien()
        {
            IQueryable<NhanVien> danhSachNhanVien = _nhanVienRepo.GetAll();
            List<NhanVienViewModel> all = new List<NhanVienViewModel>();

            all = (from nhanvien in danhSachNhanVien
                   join chucvu in _chucVuRepo.GetAll()
                   on nhanvien.MaChucVu equals chucvu.MaChucVu
                   where (nhanvien.TrangThai.Equals(true))
                   select new
                   {
                       MaNhanVien = nhanvien.MaNhanVien,
                       TenNhanVien = nhanvien.TenNhanvien,
                       DiaChi = nhanvien.DiaChi,
                       SoDienThoai = nhanvien.SoDienThoai,
                       Email = nhanvien.Email,
                       CMND = nhanvien.CMND,
                       TrangThai = nhanvien.TrangThai,
                       TenChucVu = chucvu.TenChucVu,
                       Avatar = nhanvien.Avatar,
                       UserName = nhanvien.UserName,
                   }).AsEnumerable().Select(x => new NhanVienViewModel()
                   {
                       maNhanVien = x.MaNhanVien,
                       tenNhanVien = x.TenNhanVien,
                       diaChi = x.DiaChi,
                       soDienThoai = x.SoDienThoai,
                       email = x.Email,
                       CMND = x.CMND,
                       trangThai = x.TrangThai,
                       tenChucVu = x.TenChucVu,
                       avatar = x.Avatar,
                       userName = x.UserName,
                   }).ToList();
            
            return all;

        }

        public async Task Create(object model)
        {
            var nhanVien = new NhanVien();
            NhanVienViewModel input = (NhanVienViewModel)model;

            nhanVien.TenNhanvien = input.tenNhanVien;
            nhanVien.DiaChi = input.diaChi;
            nhanVien.SoDienThoai = input.soDienThoai;
            nhanVien.Email = input.email;
            nhanVien.CMND = input.CMND;
            nhanVien.UserName = input.userName;
            nhanVien.PassWord = Md5Encode.EncodePassword(input.password);
            nhanVien.TrangThai = true;
            nhanVien.MaChucVu = input.maChucVu;
            nhanVien.Avatar = input.avatar;
         
            await _nhanVienRepo.InsertAsync(nhanVien);
        }

        public IEnumerable<NhanVienViewModel> LoadDanhSachNhanVienTheoMa(int maNhanVien)
        {
            IQueryable<NhanVien> danhSachNhanVien = _nhanVienRepo.GetAll();
          
            var all = (from nhanvien in danhSachNhanVien
                       join chucvu in _chucVuRepo.GetAll()
                       on nhanvien.MaChucVu equals chucvu.MaChucVu
                       where (nhanvien.MaNhanVien.Equals(maNhanVien))
                       select new NhanVienViewModel
                       {
                           maNhanVien = nhanvien.MaNhanVien,
                           tenNhanVien = nhanvien.TenNhanvien,
                           userName = nhanvien.UserName,
                           diaChi = nhanvien.DiaChi,
                           soDienThoai = nhanvien.SoDienThoai,
                           email = nhanvien.Email,
                           CMND = nhanvien.CMND,
                           trangThai = nhanvien.TrangThai,
                           tenChucVu = chucvu.TenChucVu,
                           avatar = nhanvien.Avatar,
                       }).ToList();

            return all;
        }

        public async Task<object> Find(int ID)
        {
            return await _nhanVienRepo.GetByIdAsync(ID);
        }
        public string MatKhau(int ID)
        {
            IQueryable<NhanVien> tatCaNhanVien = _nhanVienRepo.GetAll();
            return tatCaNhanVien.FirstOrDefault(x => x.MaNhanVien.Equals(ID)).PassWord;
        }


        public async Task Update(object inputModel, object editModel)
        {
            NhanVienViewModel input = (NhanVienViewModel)inputModel;
            NhanVien editNhanVien = (NhanVien)editModel;

            editNhanVien.TenNhanvien = input.tenNhanVien;
            editNhanVien.CMND = input.CMND;
            editNhanVien.DiaChi = input.diaChi;
            editNhanVien.Email = input.email;
            editNhanVien.MaChucVu = input.maChucVu;
            editNhanVien.SoDienThoai = input.soDienThoai;
            editNhanVien.TrangThai = input.trangThai;
            editNhanVien.UserName = input.userName;
            editNhanVien.Avatar = input.avatar;

            await _nhanVienRepo.EditAsync(editNhanVien);
        }

        public async Task UpdatePassword(NhanVien editEmployee, String PassWord)
        {
            editEmployee.PassWord = PassWord;
            await _nhanVienRepo.EditAsync(editEmployee);
        }

        public IEnumerable<NhanVien> GetAllUserName()
        {
            IQueryable<NhanVien> danhSachNhanVien = _nhanVienRepo.GetAll();
            var all = (from nhanvien in danhSachNhanVien
                       select new
                       {
                           UserName = nhanvien.UserName,
                           SoDienThoai = nhanvien.SoDienThoai,
                           Email = nhanvien.Email,
                           CMDN = nhanvien.CMND,
                       }).AsEnumerable().Select(x => new NhanVien()
                       {
                           UserName = x.UserName,
                           SoDienThoai = x.SoDienThoai,
                           Email = x.Email,
                           CMND = x.CMDN,
                       }).ToList();
            return all;
        }

        public IEnumerable<NhanVien> GetAllPassword(int maNhanVien)
        {
            IQueryable<NhanVien> danhSachNhanVien= _nhanVienRepo.GetAll();
            var all = (from nhanvien in danhSachNhanVien
                       where nhanvien.MaNhanVien.Equals(maNhanVien)
                       select new
                       {
                           PassWord = nhanvien.PassWord,
                       }).AsEnumerable().Select(x => new NhanVien()
                       {
                           PassWord = x.PassWord,
                       }).ToList();
            return all;
        }

    }
}
