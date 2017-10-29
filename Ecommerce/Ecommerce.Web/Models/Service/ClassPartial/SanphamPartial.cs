using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace  Ecommerce.Web.Models
{
    [MetadataTypeAttribute(typeof(SanphamPartial.Metadata))]
    public partial class SanphamPartial
    {
        internal sealed class Metadata
        {
            [AllowHtml]
            public string MoTa { get; set; }
        }
    }

    
    
}