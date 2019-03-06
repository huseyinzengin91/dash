using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Transporter
{
    public class DSRMGetTransportData
    {
        [Required]
        [MaxLength(50)]
        public string FromSiteAddress { get; set; }
        [Required]
        public string AccessCode { get; set; }
        [Required]
        public string DataShareCode { get; set; }
    }
}