using System.Data.Entity.Migrations;


namespace DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.DataBaseMigrator>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // #if DEBUG 
            AutomaticMigrationDataLossAllowed = true;
        }

        //The purpose of the Seed method is to enable you to insert or update test data after Code First creates or updates the database.
        //This method is called when the database is created and every time the database schema is updated after a data model change.
        protected override void Seed(DataAccess.DataBaseMigrator context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
