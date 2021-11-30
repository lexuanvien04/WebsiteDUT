namespace ModelEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuangCao")]
    public partial class QuangCao
    {
        [Key]
        [StringLength(10)]
        public string MaQuangCao { get; set; }

        [Required]
        [StringLength(100)]
        public string TenQuangCao { get; set; }

        public string DuongDan { get; set; }

        public byte[] AnhDaiDien { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public bool TrangThai { get; set; }
    }
}
