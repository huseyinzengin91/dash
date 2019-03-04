using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Entity.Objects
{
    public class DSDDataShare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual DSDSite OwnerSiteId { get; set; }
        public string DataShareCode { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
    }

    public enum DSDDataShareStatusTypes : short
    {
        Deactive = 0,
        Active = 1
    }
}