//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteDUT
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChuyenMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenMuc()
        {
            this.DonVis = new HashSet<DonVi>();
            this.TagCoulds = new HashSet<TagCould>();
        }
    
        public string MaChuyenMuc { get; set; }
        public string MaLoaiChuyenMuc { get; set; }
        public string TenChuyenMuc { get; set; }
        public string TieuDe { get; set; }
        public byte[] AnhDaiDien { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public string MaNguoiDung { get; set; }
        public string Trangthai { get; set; }
    
        public virtual LoaiChuyenMuc LoaiChuyenMuc { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonVi> DonVis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagCould> TagCoulds { get; set; }
    }
}
