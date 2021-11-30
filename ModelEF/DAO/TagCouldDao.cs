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
        private WebsiteDUTDbContext db;

        public TagCouldDao()
        {
            db = new WebsiteDUTDbContext();
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

        public TagCould Find(string id)
        {
            return db.TagCoulds.Find(id);
        }
        public bool TagCouldTrangThai(string id)
        {
            var user = db.TagCoulds.Find(id);
            user.TrangThai = !user.TrangThai;
            db.SaveChanges();
            return user.TrangThai;
        }
        public string Insert(TagCould entityTagCloud)
        {
            var dao = Find(entityTagCloud.MaTagCould);
            if (dao == null)
            {
                db.TagCoulds.Add(entityTagCloud);
            }
            else
            {
                dao.TenCould = entityTagCloud.TenCould;
            }
            db.SaveChanges();
            return entityTagCloud.MaTagCould;
        }

        public string Edit(TagCould entity)
        {
            var dao = Find(entity.MaTagCould);
            if (dao == null)
            {
                db.TagCoulds.Add(entity);
            }
            else
            {
                dao.TenCould = entity.TenCould;
            }
            db.SaveChanges();
            return entity.MaTagCould;
        }
    }
}
