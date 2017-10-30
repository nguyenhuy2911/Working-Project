using Ecommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Models.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Items = new List<SanPham>();
        }
        public List<SanPham> Items { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}