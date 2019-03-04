using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Entity.Objects
{
    [Table("Site")]
    public class DSDSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string SiteAddress { get; set; }
        public string AccessCode { get; set; }
        public DateTime AccessCodeEndDate { get; set; }
        public DSDSiteStatusTypes Status { get; set; }
    }

    public enum DSDSiteStatusTypes : short
    {
        Deactive = 0,
        Active = 1
    }
}