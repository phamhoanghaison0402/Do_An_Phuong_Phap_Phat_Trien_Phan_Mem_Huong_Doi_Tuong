using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class ThongTinHoatDongViewModel
    {
        public int soPhieuKiemKho { get; set; }
        public DateTime ngayChinhSuaKiemKho { get; set; }
        public bool trangThaiKiemKho { get; set; }
        public string tenNhanVienKiemKho { get; set; }


        public int soPhieuXuatKho { get; set; }
        public DateTime ngayChinhSuaXuatKho { get; set; }
        public bool trangThaiXuatKho { get; set; }
        public string tenNhanVienXuatKho { get; set; }

        public int soPhieuNhapKho { get; set; }
        public DateTime ngayChinhSuaNhapKho { get; set; }
        public bool trangThaiNhapKho { get; set; }
        public string tenNhanVienNhapKho { get; set; }

        public int soPhieuBanHang { get; set; }
        public DateTime ngayChinhSuaBanHang { get; set; }
        public bool trangThaiBanHang { get; set; }
        public string tenNhanVienBanHang { get; set; }

        public int soPhieuChi { get; set; }

        public int maPhieuNhap { get; set; }

        public DateTime ngayChinhSuaChi { get; set; }
        public bool trangThaiChi { get; set; }
        public string tenNhanVienChi { get; set; }
        public decimal tongTienChi { get; set; }
    }
}
