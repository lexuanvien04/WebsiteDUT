using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class LoaiChuyenMucDao
    {
        private WebsiteDTUDbContext db;

        public LoaiChuyenMucDao()
        {
            db = new WebsiteDTUDbContext();
        }
        public IEnumerable<LoaiChuyenMuc> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<LoaiChuyenMuc> model = db.LoaiChuyenMucs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.TenLoaiChuyenMuc.Contains(keysearch));
            }

            return model.OrderBy(x => x.TenLoaiChuyenMuc).ToPagedList(page, pagesize);
        }
    }
}
