using AutoMapper;
using Mehdime.Entity;
using System;
using Unity;
// Used modules and interfaces in the project
using BusinessLogic;
using DataAccess;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Presentation.App_Start;

namespace Presentation
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();


            // Admin
            container.RegisterType<IAdminRepository, AdminRepository>();
            container.RegisterType<IAdminControl, AdminControl>();


            // Client
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IClientProfileControl, ClientProfileControl>();
            container.RegisterType<ICountryRepository, CountryRepository>();


            // Operations with Files
            container.RegisterType<IFileControl, FileControl>();
            container.RegisterType<IImportControl, ImportControl>();


            // Everything associated with items in the shop
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IItemDistributionControl, ItemDistributionControl>();
            container.RegisterType<IItemControl, ItemControl>();


            // Everything associated with categories and properties
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IItemCategoryControl, ItemCategoryControl>();
            container.RegisterType<IPropertyControl, PropertyControl>();
            container.RegisterType<IPropertyRepository, PropertyRepository>();


            // Everything associated with  orders in the shop
            container.RegisterType<IOrderControl, OrderControl>();
            container.RegisterType<IOrderRepository, OrderRepository>();


            // Cart
            container.RegisterType<IClientCartControl, ClientCartControl>();


            // Payment
            container.RegisterType<IHttpPaymentControl, HttpPaymentControl>();
            container.RegisterType<IClientPaymentDatabaseControl, ClientPaymentDatabaseControl>();


            //Mapping DB Context
            container.RegisterInstance<IMapper>(MappingProfile.InitializeAutoMapper().CreateMapper());
            container.RegisterInstance<IDbContextScopeFactory>(new DbContextScopeFactory());
            container.RegisterInstance<IAmbientDbContextLocator>(new AmbientDbContextLocator());
        }
    }
}