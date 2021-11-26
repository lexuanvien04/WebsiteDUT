using ModelEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class QuyenNguoiDungDao
    {
        private WebsiteDTUDbContext db;

        public QuyenNguoiDungDao()
        {
            db = new WebsiteDTUDbContext();
        }
        public IEnumerable<QuyenNguoiDung> ListWhereAll(string keysearch, int page, int pagesize)
        {
            IQueryable<QuyenNguoiDung> model = db.QuyenNguoiDungs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.MaQuyen.Contains(keysearch));
            }

            return model.OrderBy(x => x.MaQuyen).ToPagedList(page, pagesize);
        }
    }
}
