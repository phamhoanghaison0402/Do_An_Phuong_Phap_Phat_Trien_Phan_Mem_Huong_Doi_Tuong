namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaoCaoTonKho")]
    public partial class BaoCaoTonKho
    {
        [Key]
        public int MaBaoCaoTonKho { get; set; }

        public int Thang { get; set; }

        public int Nam { get; set; }

        public int MaHangHoa { get; set; }

        public int SoLuongTonDau { get; set; }

        public int SoLuongNhap { get; set; }

        public int SoLuongXuat { get; set; }

        public int SoLuongTonCuoi { get; set; }
    }
}
