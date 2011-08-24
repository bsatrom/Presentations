using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Faceplant.Models
{
    public class FaceplantContext : DbContext
    {
        public FaceplantContext()
        {
            // Instructions:
            //  * You can add custom code to this file. Changes will *not* be lost when you re-run the scaffolder.
            //  * If you want to regenerate the file totally, delete it and then re-run the scaffolder.
            //  * You can delete these comments if you wish
            //  * If you want Entity Framework to drop and regenerate your database automatically whenever you 
            //    change your model schema, uncomment the following line:
			//    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FaceplantContext>());
        }

        public DbSet<Faceplant.Core.Models.Tag> Tags { get; set; }

        public DbSet<Faceplant.Core.Models.Response> Responses { get; set; }

        public DbSet<Faceplant.Core.Models.Session> Sessions { get; set; }

        public DbSet<Faceplant.Core.Models.Speaker> Speakers { get; set; }
    }
}