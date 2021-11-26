using ModelEF.EF;
using PagedList;
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
        public IEnumerable<NguoiDung> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<NguoiDung> model = db.NguoiDungs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.HoTen.Contains(keysearch));
            }

            return model.OrderBy(x => x.HoTen).ToPagedList(page, pagesize);
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