namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trackingaction")]
    public partial class Trackingaction
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(5)]
        public string MaSP { get; set; }

        [StringLength(50)]
        public string Controller { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        public DateTime? Ngaythuchien { get; set; }
    }
}
