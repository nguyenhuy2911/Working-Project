namespace Ecommerce.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Oauth")]
    public partial class Oauth
    {
        [Key]
        [StringLength(5)]
        public string ID { get; set; }

        [StringLength(30)]
        public string Consumer_key { get; set; }

        [StringLength(100)]
        public string Callback { get; set; }

        [StringLength(100)]
        public string Request_token { get; set; }

        [StringLength(100)]
        public string Verifier_token { get; set; }

        public DateTime? Date_comsumer { get; set; }

        [StringLength(5)]
        public string MaNCC { get; set; }

        [StringLength(120)]
        public string Token { get; set; }

        public DateTime? ExpiresTime { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
