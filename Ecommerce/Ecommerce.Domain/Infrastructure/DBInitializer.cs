using Ecommerce.Domain.Constant;
using Ecommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Infrastructure
{
    public class DBInitializer : CreateDatabaseIfNotExists<EcommerceModel_DbContext>
    {
        protected override void Seed(EcommerceModel_DbContext context)
        {
            var role = new AspNetRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = RoleName.Admin
            };
            context.AspNetRoles.Add(role);
            base.Seed(context);
        }
    }
}
