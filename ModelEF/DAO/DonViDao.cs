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
        private WebsiteDTUDbContext db;

        public DonViDao()
        {
            db = new WebsiteDTUDbContext();
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
    }
}
