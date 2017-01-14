namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanQuyen")]
    public partial class PhanQuyen
    {
        [Key]
        [StringLength(50)]
        public string MaQuyen { get; set; }

        [StringLength(100)]
        public string TenQuyen { get; set; }
    }
}
