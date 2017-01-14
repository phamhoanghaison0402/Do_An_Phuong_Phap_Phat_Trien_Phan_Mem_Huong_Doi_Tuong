namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangHoa")]
    public partial class HangHoa
    {
        [Key]
        public int MaHangHoa { get; set; }

        [StringLength(50)]
        public string TenHangHoa { get; set; }

        public decimal GiaBan { get; set; }

        public decimal GiamGia { get; set; }

        public int SoLuongTon { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string ThongSoKyThuat { get; set; }

        [StringLength(200)]
        public string XuatXu { get; set; }

        public int ThoiGianBaoHanh { get; set; }

        public string HinhAnh { get; set; }

        public int MaLoaiHangHoa { get; set; }

        public bool TrangThai { get; set; }

        public string ModelName { get; set; }

        public string Value { get; set; }

        public int Value1 { get; set; }

        public decimal Value2 { get; set; }
    }
}
