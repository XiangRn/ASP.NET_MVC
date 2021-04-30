using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using Common;
using PagedList;
namespace Models.DAO
{
   public class ContentDAO
    {
        OnlineShopDBContext db = null;
        public ContentDAO()
        {
            db = new OnlineShopDBContext();
        }
        public IEnumerable<Content> ListAll()
        {
            return db.Contents.SqlQuery("select * from Content").ToList();
        }
      
        public IEnumerable<Content> ListEx(int page, int pagesize)
        {
            return db.Contents.SqlQuery("select * from [Content]").ToPagedList(page, pagesize);
        }
        public IEnumerable<Content> DetailTags(string tagID,int page, int pagesize)
        {
            return db.Contents.SqlQuery("select * from [Content] where ID= any(select ContentID from ContentTag where TagID=@p0)",tagID).ToPagedList(page, pagesize);
        }
        public Content GetById(long id)
        {
            return db.Contents.SqlQuery("select * from Content where ID=@p0",id).SingleOrDefault();
        }
        public long Edit(Content content)
        {
            //xử lý Alias
            if (string.IsNullOrEmpty(content.MetaTittle))
            {
                content.MetaTittle = StringHelper.ToUnsignString(content.Name);
            }
            //Xử lý Tags
            content.CreatedDate = DateTime.Now;      
            db.SaveChanges();
            if (!string.IsNullOrEmpty(content.Tags))
            {
                this.RemoveAllContentTag(content.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var item in tags)
                {
                    var tagID = StringHelper.ToUnsignString(item);
                    var tagexisted = this.CheckTags(tagID);
                    if (!tagexisted)
                    {
                        this.InsertTags(tagID, item);
                    }

                    //insert to content tag
                    this.InsertContentTags(content.ID, tagID);

                }
            }
            return content.ID;
        }
        public void RemoveAllContentTag(long contentID)
        {
            db.ContentTags.Remove(db.ContentTags.Find(contentID));
            db.SaveChanges();
        }
        public bool Create(Content content)
        {
            List<object> lst = new List<object>();
            lst.Add(content.Name);
            lst.Add(content.MetaTittle);
            lst.Add(content.Description);
            lst.Add(content.Image);
            lst.Add(content.CategoryID);
            lst.Add(content.Detail);
            object[] ls = lst.ToArray();
            var result = db.Database.ExecuteSqlCommand("insert into Content(Name,MetaTittle,Description,Image,CategoryID,Detail) values(@p0,@p1,@p2,@p3,@p4,@p5)", ls);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public long Insert(Content content)
        {
            //xử lý Alias
            if (string.IsNullOrEmpty(content.MetaTittle))
            {
                content.MetaTittle = StringHelper.ToUnsignString(content.Name);
            }
            //Xử lý Tags
            content.CreatedDate = DateTime.Now;
            //content.CreatedBy = Session["username"];
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var item in tags)
                {
                    var tagID =StringHelper.ToUnsignString(item);
                    var tagexisted = this.CheckTags(tagID);
                    if (!tagexisted)
                    {
                        this.InsertTags(tagID, item);
                    }

                    //insert to content tag
                    this.InsertContentTags(content.ID, tagID);

                }
            }
            return content.ID;
        }
        public void InsertTags(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();

        }
        public void InsertContentTags(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTags(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }
        public Content getID(long id)
        {
            return db.Contents.Find(id);
        }
        public List<Tag> ListTags(long contentID)
        {
            return db.Tags.SqlQuery("select * from Tag where ID= ANY (select TagID from ContentTag where ContentID = @p0)",contentID).ToList();
        }
    }
}
