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
        private WebsiteDTUDbContext db;

        public ChuyenMucDao()
        {
            db = new WebsiteDTUDbContext();
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
    }
}
