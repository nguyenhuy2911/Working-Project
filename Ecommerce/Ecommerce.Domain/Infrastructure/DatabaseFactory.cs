using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Infrastructure
{
   public class DatabaseFactory: IDisposable, IDatabaseFactory
    {
        private EcommerceModel_DbContext dataContext;

        public EcommerceModel_DbContext Get()
        {
            return dataContext ?? ( dataContext = new EcommerceModel_DbContext());
        }
        public void Dispose()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
