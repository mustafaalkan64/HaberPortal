using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using Best.Service.CategoryServices;
using Best.Service.GaleryServices;
using Best.Service.NewsServices;
using Best.Service.RoleServices;
using Best.Service.TagService;
using Best.Service.UserServices;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Best.IOC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequestScope<IGenericRepository<User>, GenericRepository<User>>();
            container.BindInRequestScope<IGenericRepository<Role>, GenericRepository<Role>>();
            container.BindInRequestScope<IGenericRepository<Category>, GenericRepository<Category>>();
            container.BindInRequestScope<IGenericRepository<Tag>, GenericRepository<Tag>>();
            container.BindInRequestScope<IGenericRepository<Galery>, GenericRepository<Galery>>();
            container.BindInRequestScope<IGenericRepository<News>, GenericRepository<News>>();
            container.BindInRequestScope<IGenericRepository<NewsPosition>, GenericRepository<NewsPosition>>();
            container.BindInRequestScope<IGenericRepository<NewsType>, GenericRepository<NewsType>>();

            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();

            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IRoleService, RoleService>();
            container.BindInRequestScope<ICategoryService, CategoryService>();
            container.BindInRequestScope<ITagService, TagService>();
            container.BindInRequestScope<IGaleryService, GaleryService>();
            container.BindInRequestScope<INewsService, NewsService>();
        }
    }

    /// <summary>
    /// Bind the given interface in request scope
    /// </summary>
    public static class IOCExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }
}