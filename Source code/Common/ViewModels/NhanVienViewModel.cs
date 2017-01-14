using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class NhanVienViewModel
    {
        public int maNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string diaChi { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string CMND { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public bool trangThai { get; set; }
        public int maChucVu { get; set; }
        public string avatar { get; set; }    
        public string tenChucVu { get; set; }

        public string checkImage { get; set; }
    }
}
