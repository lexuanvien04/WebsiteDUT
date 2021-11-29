using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class DonViDao
    {
        private WebsiteDUTDbContext db;

        public DonViDao()
        {
            db = new WebsiteDUTDbContext();
        }
        public IEnumerable<DonVi> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<DonVi> model = db.DonVis;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenDonVi.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenDonVi).ToPagedList(page, pagesize);
        }

        public DonVi Find(string id)
        {
            return db.DonVis.Find(id);
        }
        public string Insert(DonVi entityDonVi)
        {
            var dao = Find(entityDonVi.MaDonVi);
            if (dao == null)
            {
                db.DonVis.Add(entityDonVi);
            }
            else
            {
                dao.TenDonVi = entityDonVi.TenDonVi;
            }
            db.SaveChanges();
            return entityDonVi.MaDonVi;
        }
    }
}
