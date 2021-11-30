namespace ModelEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonVi")]
    public partial class DonVi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonVi()
        {
            ChuyenMucs = new HashSet<ChuyenMuc>();
        }

        [Key]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiDonVi { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDonVi { get; set; }

        public bool TrangThai { get; set; }

        public virtual LoaiDonVi LoaiDonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenMuc> ChuyenMucs { get; set; }
    }
}
