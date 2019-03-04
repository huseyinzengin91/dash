using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dash.Entity.Objects
{
    [Table("AccessibleSite")]
    public class DSDAccessibleSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual DSDSite SiteId { get; set; }
        public virtual DSDSite AccessibleSiteId { get; set; }
    }
}