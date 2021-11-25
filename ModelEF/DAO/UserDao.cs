using ModelEF.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class UserDao
    {
        private WebsiteDTUDbContext db = null;
        
        public UserDao()
        {
            db = new WebsiteDTUDbContext();
        }

        public int Login(string TenTruycap, string MatKhau)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.TenTruycap.Contains(TenTruycap) && x.MatKhau.Contains(MatKhau));
            if(result == null)
            {
                return 0;
            }
            else { return 1; }
        }
        public List<NguoiDung> ListAll()
        {
            return db.NguoiDungs.ToList();
        }
        public NguoiDung Find(string TenTruycap)
        {
            return db.NguoiDungs.Find(TenTruycap);
        }
        public bool ChangeStatus(long MaNguoiDung)
        {
            var user = db.NguoiDungs.Find(MaNguoiDung);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
    }
}