using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class KiemKhoViewModel
    {
        public int soPhieuKiemKho { get; set; }
        public DateTime ngayKiemKho { get; set; }
        public DateTime ngayChinhSua { get; set; }
        public int maNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string ghiChu { get; set; }
        public bool trangThai { get; set; }

        public int maHangHoa { get; set; }
        public string tenHangHoa { get; set; }
        public int soLuongHienTai { get; set; }
        public int soLuongKiemTra { get; set; }
        public string donViTinh { get; set; }

        public List<ChiTietPhieuKiemKho> chiTietPhieuKiemKho { get; set; }
    }
}
