using Ecommerce.Domain.Infrastructure;
using Ecommerce.Domain.Model;
using Ecommerce.Web.Models.Core;
using System.Data.Entity;
using System.Linq;

namespace  Ecommerce.Web.Models.Service
{
    public class DisplayService : BaseService
    {    
        internal IQueryable<Display> GetDisPlays()
        {
            return dataContext.Displays;
        }

        internal IQueryable<Link> GetSlideShow()
        {
            return dataContext.Links.Where(m => m.Group.Contains("SlideShow"));
        }
    }
}