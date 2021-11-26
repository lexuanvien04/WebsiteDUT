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
        private WebsiteDTUDbContext db;

        public LoaiDonViDao()
        {
            db = new WebsiteDTUDbContext();
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
    }
}
