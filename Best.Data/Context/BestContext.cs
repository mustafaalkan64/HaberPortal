using Best.Core.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Best.Data.Context
{
    public partial class BestContext : DbContext
    {
        public BestContext()
            : base("BestContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Galery> Galery { get; set; }
        public virtual DbSet<GaleryImage> GaleryImage { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsPosition> NewsPosition { get; set; }
        public virtual DbSet<NewsType> NewsType { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public class SeedBestContext : CreateDatabaseIfNotExists<BestContext>
        {
            protected override void Seed(BestContext context)
            {
                User user = new User
                {
                    UserName = "Admin",
                    Password = "123456",
                    Email = "admin@gmail.com",
                    ConfirmationId = Guid.NewGuid(),
                    IsActive = true,
                    IsConfirmed = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0,
                    LastLoginDate = DateTime.Now,
                    Point = 0
                };

                List<Role> roles = new List<Role>
                {
                    new Role { Name = "Admin",IsActive = true,InsertDate = DateTime.Now,UpdateDate = DateTime.Now,InsertUserId = 0,UpdateUserId = 0 },
                    new Role { Name = "Moderator" ,IsActive = true,InsertDate = DateTime.Now,UpdateDate = DateTime.Now,InsertUserId = 0,UpdateUserId = 0},
                    new Role { Name = "Editor",IsActive = true,InsertDate = DateTime.Now,UpdateDate = DateTime.Now,InsertUserId = 0,UpdateUserId = 0 },
                    new Role { Name = "Author",IsActive = true,InsertDate = DateTime.Now,UpdateDate = DateTime.Now,InsertUserId = 0,UpdateUserId = 0 }
                };

                // roller
                foreach (var item in roles)
                {
                    user.Roles.Add(item);
                }

                // kullanıcı
                context.User.Add(user);

                // haber tipleri
                context.NewsType.Add(new NewsType
                {
                    Name = "Haber",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.NewsType.Add(new NewsType
                {
                    Name = "Köşe Yazısı",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });

                // haber posizyonları
                context.NewsPosition.Add(new NewsPosition
                {
                    Name = "Sol Manşet",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.NewsPosition.Add(new NewsPosition
                {
                    Name = "Orta Manşet",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.NewsPosition.Add(new NewsPosition
                {
                    Name = "Sağ Manşet Sol",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });

                // kategoriler

                context.Category.Add(new Category
                {
                    Name = "GÜNDEM",
                    SeoName = "gundem",
                    Order = 0,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "DÜNYA",
                    SeoName = "dunya",
                    Order = 1,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "EKONOMİ",
                    SeoName = "ekonomi",
                    Order = 2,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "SİYASET",
                    SeoName = "siyaset",
                    Order = 3,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "SPOR",
                    SeoName = "spor",
                    Order = 4,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "EĞİTİM",
                    SeoName = "egitim",
                    Order = 5,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "TEKNOLOJİ",
                    SeoName = "teknoloji",
                    Order = 6,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "KÜLTÜR",
                    SeoName = "kultur",
                    Order = 7,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "AİLE-SAĞLIK",
                    SeoName = "aile-saglik",
                    Order = 8,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Category.Add(new Category
                {
                    Name = "MAGAZİN",
                    SeoName = "magazin",
                    Order = 9,
                    ParentId = 0,
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });

                // örnek etiketler
                context.Tag.Add(new Tag
                {
                    Name = "haber",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "yazılım",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "bilişim",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "gündem",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "istanbul",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "politika",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "ekonomi",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });
                context.Tag.Add(new Tag
                {
                    Name = "enerji",
                    IsActive = true,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    InsertUserId = 0,
                    UpdateUserId = 0
                });

                base.Seed(context);
            }
        }
    }
}
