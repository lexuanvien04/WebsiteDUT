using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class LoaiDonViDao
    {
        private WebsiteDUTDbContext db;

        public LoaiDonViDao()
        {
            db = new WebsiteDUTDbContext();
        }
        public IEnumerable<LoaiDonVi> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<LoaiDonVi> model = db.LoaiDonVis;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenLoaiDonVi.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenLoaiDonVi).ToPagedList(page, pagesize);
        }

        public LoaiDonVi Find(string id)
        {
            return db.LoaiDonVis.Find(id);
        }
        public bool LoaiDonViTrangThai(string id)
        {
            var user = db.LoaiDonVis.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public string Insert(LoaiDonVi entityLoaiDV)
        {
            var dao = Find(entityLoaiDV.MaloaiDonVi);
            if (dao == null)
            {
                db.LoaiDonVis.Add(entityLoaiDV);
            }
            else
            {
                dao.TenLoaiDonVi = entityLoaiDV.TenLoaiDonVi;
            }
            db.SaveChanges();
            return entityLoaiDV.MaloaiDonVi;
        }

        public string Edit(LoaiDonVi entity)
        {
            var dao = Find(entity.MaloaiDonVi);
            if (dao == null)
            {
                db.LoaiDonVis.Add(entity);
            }
            else
            {
                dao.TenLoaiDonVi = entity.TenLoaiDonVi;
            }
            db.SaveChanges();
            return entity.MaloaiDonVi;
        }
    }
}
