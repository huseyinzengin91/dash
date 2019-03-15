using System;
using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Access
{
    public class DSRMAdd
    {
        [Required]
        public Guid SiteId { get; set; }
        [Required]
        public Guid AccessibleSiteId { get; set; }
    }
}