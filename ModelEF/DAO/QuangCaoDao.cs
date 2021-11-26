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
        private WebsiteDTUDbContext db;

        public QuangCaoDao()
        {
            db = new WebsiteDTUDbContext();
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
    }
}
