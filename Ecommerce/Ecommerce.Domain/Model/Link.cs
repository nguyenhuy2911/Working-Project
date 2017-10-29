namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Link")]
    public partial class Link
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string Url { get; set; }

        [Column(TypeName = "text")]
        public string Image { get; set; }

        [Required]
        [StringLength(128)]
        public string Text { get; set; }

        [StringLength(50)]
        public string Group { get; set; }
    }
}
