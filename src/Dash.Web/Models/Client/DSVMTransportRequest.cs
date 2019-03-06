using System.ComponentModel.DataAnnotations;

namespace Dash.Web.Models.Client
{
    public class DSVMTransportRequest
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



/* 
Siteadress:abc.com
              AccessibleSite:xyx.com
              AccessCode: xxxxxx (Tabloda ilgili site için tanımlanan erişim kodu)
              SessionValue: xyz.com portalında login için kullanılacak parametreler json formatta buraya kaydedilir {username:gnb\hzengin}
*/