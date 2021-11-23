namespace ModelEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienKet")]
    public partial class LienKet
    {
        [Key]
        [StringLength(10)]
        public string MaLienKet { get; set; }

        public byte[] AnhDaiDien { get; set; }

        public string Duongdan { get; set; }

        [StringLength(100)]
        public string TrangThai { get; set; }
    }
}
