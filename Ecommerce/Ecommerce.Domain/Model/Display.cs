namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Display")]
    public partial class Display
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Type { get; set; }

        //[Required]
        //[StringLength(128)]
        //[DisplayName("Loại")]
        //public string TypeName { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Giá trị")]
        public string Value { get; set; }
    }
}
