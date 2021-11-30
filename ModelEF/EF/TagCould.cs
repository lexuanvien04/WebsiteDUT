namespace ModelEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TagCould")]
    public partial class TagCould
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TagCould()
        {
            ChuyenMucs = new HashSet<ChuyenMuc>();
        }

        [Key]
        [StringLength(10)]
        public string MaTagCould { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCould { get; set; }

        public bool TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenMuc> ChuyenMucs { get; set; }
    }
}
