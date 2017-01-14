using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class BaoCaoTonKhoViewModel
    {
        public int maBaoCaoTonKho { get; set; }
        public int thang { get; set; }
        public int nam { get; set; }
        public int maHangHoa { get; set; }
        public string tenHangHoa { get; set; }
        public string donViTinh { get; set; }
        public int soLuongTonDau { get; set; }
        public int soLuongNhap { get; set; }
        public int soLuongXuat { get; set; }
        public int soLuongTonCuoi { get; set; }
    }
}
