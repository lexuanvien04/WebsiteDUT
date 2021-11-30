using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class LienKetDao
    {
        private WebsiteDUTDbContext db;

        public LienKetDao()
        {
            db = new WebsiteDUTDbContext();
        }

        public LienKet Find(string id)
        {
            return db.LienKets.Find(id);
        }
        public bool LienKetTrangThai(string id)
        {
            var user = db.LienKets.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public IEnumerable<LienKet> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<LienKet> model = db.LienKets;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.MaLienKet.Contains(keysearch));
            }

            return model.OrderBy(x => x.MaLienKet).ToPagedList(page, pagesize);
        }

        public string Insert(LienKet entityLienKet)
        {
            var dao = Find(entityLienKet.MaLienKet);
            if (dao == null)
            {
                db.LienKets.Add(entityLienKet);
            }
            else
            {
                dao.Duongdan = entityLienKet.Duongdan;
            }
            db.SaveChanges();
            return entityLienKet.MaLienKet;
        }

        public string Edit(LienKet entity)
        {
            var dao = Find(entity.MaLienKet);
            if (dao == null)
            {
                db.LienKets.Add(entity);
            }
            else
            {
                dao.AnhDaiDien = entity.AnhDaiDien;
            }
            db.SaveChanges();
            return entity.MaLienKet;
        }
    }
}
