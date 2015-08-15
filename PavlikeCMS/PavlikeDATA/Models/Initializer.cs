using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace PavlikeDATA.Models
{
    internal class Initializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

          
        }
    }


}
