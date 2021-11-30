using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class QuangCaoDao
    {
        private WebsiteDUTDbContext db;

        public QuangCaoDao()
        {
            db = new WebsiteDUTDbContext();
        }
        public IEnumerable<QuangCao> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<QuangCao> model = db.QuangCaos;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenQuangCao.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenQuangCao).ToPagedList(page, pagesize);
        }
        public LienKet Find(string id)
        {
            return db.LienKets.Find(id);
        }
        public bool QuangCaoTrangThai(string id)
        {
            var user = db.QuangCaos.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public string Insert(QuangCao entityQuangCao)
        {
            var dao = Find(entityQuangCao.MaQuangCao);
            if (dao == null)
            {
                db.QuangCaos.Add(entityQuangCao);
            }
            else
            {
                dao.Duongdan = entityQuangCao.TenQuangCao;
            }
            db.SaveChanges();
            return entityQuangCao.MaQuangCao;
        }
        public string Edit(QuangCao entity)
        {
            var dao = Find(entity.MaQuangCao);
            if (dao == null)
            {
                db.QuangCaos.Add(entity);
            }
            else
            {
                dao.Duongdan = entity.DuongDan;
            }
            db.SaveChanges();
            return entity.MaQuangCao;
        }
    }
}
