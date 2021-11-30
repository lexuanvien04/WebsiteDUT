namespace ModelEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenMuc")]
    public partial class ChuyenMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenMuc()
        {
            DonVis = new HashSet<DonVi>();
            TagCoulds = new HashSet<TagCould>();
        }

        [Key]
        [StringLength(10)]
        public string MaChuyenMuc { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiChuyenMuc { get; set; }

        [Required]
        [StringLength(100)]
        public string TenChuyenMuc { get; set; }

        [Required]
        [StringLength(100)]
        public string TieuDe { get; set; }

        public byte[] AnhDaiDien { get; set; }

        [StringLength(200)]
        public string NoiDung { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? LuotXem { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNguoiDung { get; set; }

        public bool Trangthai { get; set; }

        public virtual LoaiChuyenMuc LoaiChuyenMuc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonVi> DonVis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagCould> TagCoulds { get; set; }
    }
}
