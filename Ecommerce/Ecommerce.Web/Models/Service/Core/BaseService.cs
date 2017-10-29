
using Ecommerce.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Models.Core
{
    public class BaseService
    {
        private EcommerceModel_DbContext _dataContext;
        private IDatabaseFactory _databaseFactory { get; set; }
        protected IDatabaseFactory DatabaseFactory
        {
            get
            {
                return _databaseFactory ?? (_databaseFactory = new DatabaseFactory());
            }
        }

        protected EcommerceModel_DbContext dataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }


    }
}