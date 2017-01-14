namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuChi")]
    public partial class PhieuChi
    {
        [Key]
        public int SoPhieuChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayChi { get; set; }

        public int MaNhanVien { get; set; }

        public int MaPhieuNhap { get; set; }

        public decimal TongTienChi { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        public DateTime NgayChinhSua { get; set; }

        public bool TrangThai { get; set; }
    }
}
