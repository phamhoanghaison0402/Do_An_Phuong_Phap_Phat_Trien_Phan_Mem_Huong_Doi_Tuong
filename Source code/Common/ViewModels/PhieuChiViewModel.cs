using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuChiViewModel
    {
        public int soPhieuChi { get; set; }

        public DateTime ngayChi { get; set; }

        public int maNhanVien { get; set; }

        public string tenNhanVien { get; set; }

        public int maPhieuNhap { get; set; }

        public decimal tongTienChi { get; set; }

        public string ghiChu { get; set; }
        public bool trangThai { get; set; }
    }
}
