using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class TagCouldDao
    {
        private WebsiteDTUDbContext db;

        public TagCouldDao()
        {
            db = new WebsiteDTUDbContext();
        }
        public IEnumerable<TagCould> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<TagCould> model = db.TagCoulds;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenCould.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenCould).ToPagedList(page, pagesize);
        }
    }
}
