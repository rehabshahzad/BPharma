using System.Data.Entity.Migrations;
using MySql.Data.EntityFramework;
using Pharma.DAL.Context;

namespace Pharma.DAL.Migrations
{
    internal sealed class Configuration
        : DbMigrationsConfiguration<PharmacyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            SetSqlGenerator(
                "MySql.Data.MySqlClient",
                new MySqlMigrationSqlGenerator()
            );
        }

        protected override void Seed(PharmacyDbContext context)
        {
            // Initial/default records can be added here later.
        }
    }
}