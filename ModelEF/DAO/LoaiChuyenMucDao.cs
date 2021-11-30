using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class LoaiChuyenMucDao
    {
        private WebsiteDUTDbContext db;

        public LoaiChuyenMucDao()
        {
            db = new WebsiteDUTDbContext();
        }
        public IEnumerable<LoaiChuyenMuc> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<LoaiChuyenMuc> model = db.LoaiChuyenMucs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenLoaiChuyenMuc.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenLoaiChuyenMuc).ToPagedList(page, pagesize);
        }

        public LoaiChuyenMuc Find(string id)
        {
            return db.LoaiChuyenMucs.Find(id);
        }
        public bool LoaiChuyenMucTrangThai(string id)
        {
            var user = db.LoaiChuyenMucs.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public string Insert(LoaiChuyenMuc entityLoaiCM)
        {
            var dao = Find(entityLoaiCM.MaLoaiChuyenMuc);
            if (dao == null)
            {
                db.LoaiChuyenMucs.Add(entityLoaiCM);
            }
            else
            {
                dao.TenLoaiChuyenMuc = entityLoaiCM.TenLoaiChuyenMuc;
            }
            db.SaveChanges();
            return entityLoaiCM.MaLoaiChuyenMuc;
        }

        public string Edit(LoaiChuyenMuc entity)
        {
            var dao = Find(entity.MaLoaiChuyenMuc);
            if (dao == null)
            {
                db.LoaiChuyenMucs.Add(entity);
            }
            else
            {
                dao.TenLoaiChuyenMuc = entity.TenLoaiChuyenMuc;
            }
            db.SaveChanges();
            return entity.MaLoaiChuyenMuc;
        }
    }
}
