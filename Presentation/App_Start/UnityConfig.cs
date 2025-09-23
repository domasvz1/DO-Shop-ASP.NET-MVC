using System;

using Unity;
using Unity.Injection;

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

            // Register Business Logic dependencies
            container.RegisterType<BusinessLogic.Interfaces.IItemCategoryControl, BusinessLogic.ItemCategoryControl>();
            container.RegisterType<BusinessLogic.Interfaces.IItemDistributionControl, BusinessLogic.ItemDistributionControl>();
            container.RegisterType<BusinessLogic.Interfaces.IItemControl, BusinessLogic.ItemControl>();
            container.RegisterType<BusinessLogic.Interfaces.IOrderControl, BusinessLogic.OrderControl>();
            container.RegisterType<BusinessLogic.Interfaces.IPropertyControl, BusinessLogic.PropertyControl>();
            container.RegisterType<BusinessLogic.Interfaces.IAdminControl, BusinessLogic.AdminControl>();
            container.RegisterType<BusinessLogic.Interfaces.IClientCartControl, BusinessLogic.ClientCartControl>();
            container.RegisterType<BusinessLogic.Interfaces.IClientProfileControl, BusinessLogic.ClientProfileControl>();
            
            // Register Data Access dependencies  
            container.RegisterType<DataAccess.Interfaces.IItemRepository, DataAccess.ItemRepository>();
            container.RegisterType<DataAccess.Interfaces.IOrderRepository, DataAccess.OrderRepository>();
            container.RegisterType<DataAccess.Interfaces.IPropertyRepository, DataAccess.PropertyRepository>();
            container.RegisterType<DataAccess.Interfaces.ICategoryRepository, DataAccess.CategoryRepository>();
            container.RegisterType<DataAccess.Interfaces.IAdminRepository, DataAccess.AdminRepository>();
            container.RegisterType<DataAccess.Interfaces.IClientRepository, DataAccess.ClientRepository>();
            container.RegisterType<DataAccess.Interfaces.ICountryRepository, DataAccess.CountryRepository>();
            
            // Register Mehdime.Entity dependencies for Entity Framework scoping
            container.RegisterType<Mehdime.Entity.IDbContextScopeFactory, Mehdime.Entity.DbContextScopeFactory>();
            container.RegisterType<Mehdime.Entity.IAmbientDbContextLocator, Mehdime.Entity.AmbientDbContextLocator>();
            
            // Register custom DbContextFactory for DataBaseMigrator
            container.RegisterInstance<Mehdime.Entity.IDbContextFactory>(
                new CustomDbContextFactory());
        }
    }

    /// <summary>
    /// Custom DbContextFactory implementation for DataBaseMigrator
    /// </summary>
    public class CustomDbContextFactory : Mehdime.Entity.IDbContextFactory
    {
        public TDbContext CreateDbContext<TDbContext>() where TDbContext : System.Data.Entity.DbContext
        {
            // Create and return DataBaseMigrator instance
            if (typeof(TDbContext) == typeof(DataAccess.DataBaseMigrator) || typeof(TDbContext).IsAssignableFrom(typeof(DataAccess.DataBaseMigrator)))
            {
                return new DataAccess.DataBaseMigrator() as TDbContext;
            }
            
            // Fallback - try to create instance using Activator
            return System.Activator.CreateInstance<TDbContext>();
        }
    }
}