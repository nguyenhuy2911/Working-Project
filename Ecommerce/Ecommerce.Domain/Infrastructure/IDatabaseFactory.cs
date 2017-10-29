using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Infrastructure
{
    public interface IDatabaseFactory
    {
         EcommerceModel_DbContext Get();
    }
}
