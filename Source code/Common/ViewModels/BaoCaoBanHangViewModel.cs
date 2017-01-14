using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class BaoCaoBanHangViewModel
    {
        [Column("Ngày bán")]
        public DateTime ngayBan { get; set; }
        [Column("Số đơn hàng")]
        public int soDonHang { get; set; }
        [Column("Tổng tiền")]
        public decimal tongTien { get; set; }
    }
}