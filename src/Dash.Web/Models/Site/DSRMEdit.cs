using System;
using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Site
{
    public class DSRMEdit
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string SiteAddress { get; set; }
        [Required]
        [MaxLength(500)]
        public string AccessCode { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public short Status { get; set; }
    }
}