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
        private WebsiteDUTDbContext db = null;
        
        public UserDao()
        {
            db = new WebsiteDUTDbContext();
        }

        public int Login(string TenTruycap, string MatKhau)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.TenTruycap == TenTruycap);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MatKhau == MatKhau)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
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
        public NguoiDung FindTenTruycap(string TenTruycap, string MaNguoidung)
        {
            return db.NguoiDungs.SingleOrDefault(x => x.TenTruycap.Contains(TenTruycap) || x.MaNguoiDung.Contains(MaNguoidung));
        }
        public bool ChangeTrangThai(string id)
        {
            var user = db.NguoiDungs.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }

        public string Insert(NguoiDung entityNguoiDung)
        {
            var dao = Find(entityNguoiDung.MaNguoiDung);
            if (dao == null)
            {
                db.NguoiDungs.Add(entityNguoiDung);
            }
            else
            {
                dao.TenTruycap = entityNguoiDung.TenTruycap;
            }
            db.SaveChanges();
            return entityNguoiDung.MaNguoiDung;
        }
        
        public string Edit(NguoiDung entity)
        {
            var dao = Find(entity.MaNguoiDung);
            if (dao == null)
            {
                db.NguoiDungs.Add(entity);
            }
            else
            {
                dao.TenTruycap = entity.TenTruycap;
            }
            db.SaveChanges();
            return entity.MaNguoiDung;
        }
    }
}