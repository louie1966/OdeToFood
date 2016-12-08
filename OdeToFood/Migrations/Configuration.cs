using OdeToFood.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

namespace OdeToFood.Migrations {


    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFoodDb> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFoodDb context) {
            context.Restaurants.AddOrUpdate(r => r.Name,
               new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
               new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
               new Restaurant {
                   Name = "Smaka",
                   City = "Gothenburg",
                   Country = "Sweden",
                   Reviews =
                       new List<RestaurantReview> {
                       new RestaurantReview { Rating = 9, Body="Great food!", ReviewerName="Scott" }
                   }
               });

            for (int i = 0; i < 1000; i++) {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant { Name = i.ToString(), City = "Nowhere", Country = "USA" });
            }

            SeedMembership();
        }

        private void SeedMembership() {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin")) {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("rleeuw", false) == null) {
                membership.CreateUserAndAccount("rleeuw", "password");
            }
            if (!roles.GetRolesForUser("rleeuw").Contains("Admin")) {
                roles.AddUsersToRoles(new[] { "rleeuw" }, new[] { "admin" });
            }
        }
    }
}
