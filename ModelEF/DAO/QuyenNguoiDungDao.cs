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
        private WebsiteDUTDbContext db;

        public QuyenNguoiDungDao()
        {
            db = new WebsiteDUTDbContext();
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

        public QuyenNguoiDung Find(string id)
        {
            return db.QuyenNguoiDungs.Find(id);
        }

        public string Insert(QuyenNguoiDung entityQuyenND)
        {
            var dao = Find(entityQuyenND.MaQuyen);
            if (dao == null)
            {
                db.QuyenNguoiDungs.Add(entityQuyenND);
            }
            else
            {
                dao.TenQuyen = entityQuyenND.TenQuyen;
            }
            db.SaveChanges();
            return entityQuyenND.MaQuyen;
        }

        public string Edit(QuyenNguoiDung entity)
        {
            var dao = Find(entity.MaQuyen);
            if (dao == null)
            {
                db.QuyenNguoiDungs.Add(entity);
            }
            else
            {
                dao.TenQuyen = entity.TenQuyen;
            }
            db.SaveChanges();
            return entity.MaQuyen;
        }
    }
}
