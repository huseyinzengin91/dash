using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Transporter
{
    public class DSRMTransportRequest
    {
        [Required]
        [MaxLength(50)]
        public string FromSiteAddress { get; set; }
        [Required]
        public string AccessCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string ToSiteAddress { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Data { get; set; }
    }
}