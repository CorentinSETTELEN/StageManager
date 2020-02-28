using StageManager.Models;
using System.Collections.Generic;

namespace StageManager.DAL
{
    public class StageManagerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StageManagerContext>
    {
        protected override void Seed(StageManagerContext context)
        {
            var Users = new List<User>
            {
                new User{Username= "admin",Password= "admin"}
            };

            Users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}