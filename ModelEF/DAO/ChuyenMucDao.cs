using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class ChuyenMucDao
    {
        private WebsiteDUTDbContext db;

        public ChuyenMucDao()
        {
            db = new WebsiteDUTDbContext();
        }
        public IEnumerable<ChuyenMuc> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<ChuyenMuc> model = db.ChuyenMucs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenChuyenMuc.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenChuyenMuc).ToPagedList(page, pagesize);
        }

        public ChuyenMuc Find(string TenChuyenMuc)
        {
            return db.ChuyenMucs.Find(TenChuyenMuc);
        }
        public bool ChuyenMucTrangThai(string id)
        {
            var cm = db.ChuyenMucs.Find(id);
            cm.Trangthai = !cm.Trangthai;
            db.SaveChanges();
            return cm.Trangthai;
        }
        public string Insert(ChuyenMuc entityCMuc)
        {
            var dao = Find(entityCMuc.MaChuyenMuc);
            if (dao == null)
            {
                db.ChuyenMucs.Add(entityCMuc);
            }
            else
            {
                dao.TenChuyenMuc = entityCMuc.TenChuyenMuc;
            }
            db.SaveChanges();
            return entityCMuc.MaNguoiDung;
        }

        public string Edit(ChuyenMuc entity)
        {
            var dao = Find(entity.MaChuyenMuc);
            if (dao == null)
            {
                db.ChuyenMucs.Add(entity);
            }
            else
            {
                dao.TenChuyenMuc = entity.TenChuyenMuc;
            }
            db.SaveChanges();
            return entity.MaChuyenMuc;
        }
    }
}
