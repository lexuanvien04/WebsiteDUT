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
        private WebsiteDTUDbContext db;

        public LienKetDao()
        {
            db = new WebsiteDTUDbContext();
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
    }
}
