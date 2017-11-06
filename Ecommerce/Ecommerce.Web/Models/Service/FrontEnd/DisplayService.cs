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

        internal int AddDisPlay(Display data)
        {
            dataContext.Displays.Add(data);
            dataContext.SaveChanges();
            return data.Id;
        }

        internal Display UpdateDisPlay(Display data)
        {
            var _editDisplay = dataContext.Displays.Find(data.Id);
            _editDisplay.Value = data.Value;
            _editDisplay.Type = data.Type;
            dataContext.Entry(_editDisplay).State = EntityState.Modified;
            dataContext.SaveChanges();
            return _editDisplay;
        }

        internal Display FindById(int Id)
        {
           var data =  dataContext.Displays.Find(Id);
            return data;
        }
    }
}