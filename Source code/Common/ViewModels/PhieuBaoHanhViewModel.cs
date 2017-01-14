using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuBaoHanhViewModel
    {
        public int soPhieuBaoHanh { get; set; }
        public DateTime ngayLap { get; set; }
        public DateTime ngayGiao { get; set; }
        public int maNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string tenKhachHang { get; set; }
        public string soDienThoai { get; set; }
        public string ghiChu { get; set; }
        public bool trangThai { get; set; }
        public bool daGiao { get; set; }
        public string modelName { get; set; }
    }
}
