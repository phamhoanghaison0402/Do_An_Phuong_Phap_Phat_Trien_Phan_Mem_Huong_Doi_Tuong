using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class NhanVien_QuyenViewModel
    {
        public int maChucVu { get; set; }
        public string maQuyen { get; set; }
        public string parent { get; set; }
        public string controller { get; set; }
        public string tenQuyen { get; set; }
    }
}
