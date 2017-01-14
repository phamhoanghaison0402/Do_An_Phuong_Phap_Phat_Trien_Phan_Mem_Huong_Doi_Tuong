using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class PhieuDatHangViewModel
    {
        public int maHangHoa { get; set; }
        public string tenHangHoa { get; set; }
        public string hinhAnh { get; set; }
        public int soLuong { get; set; }
        public decimal giamGia { get; set; }
        public decimal giaBan { get; set; }
        public int maNhanVien { get; set; }

        public int soPhieuDatHang { get; set; }

        public string diaChi { set; get; }

        public string soDienThoai { set; get; }

        public string email { set; get; }

        public string tenKhachHang { set; get; }

        public string hinhThucThanhToan { set; get; }

        public string ghiChu { set; get; }
        
        public DateTime ngayGiao { get; set; }

        public DateTime ngayDat { get; set; }

        public bool daXacNhan { get; set; }

        public bool daThanhToan { get; set; }

        public decimal tongTien { get; set; }

        public string tenNhanVien { get; set; }

        public bool trangThai { get; set; }

        public DateTime ngayChinhSua { get; set; }

        public List<ChiTietPhieuDatHang> chiTietPhieuDatHang { get; set; }
    }
}
