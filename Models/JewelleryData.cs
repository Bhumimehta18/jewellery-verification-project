using System;
using System.ComponentModel.DataAnnotations;

namespace JewelleryVerificationProject.Models
{
    public class Jewellery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CompanyCode { get; set; }

        [Required]
        public string BagNo { get; set; }

        [Required]
        public string OrderNo { get; set; }

        [Required]
        public string DesignNo { get; set; }

        [Required]
        public int BagQN { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public string CertificateType { get; set; }

        [Required]
        public string CertificateNumber { get; set; }

        [Required]
        public DateTime CertificateEnterDate { get; set; }

        [Required]
        public decimal GrossWeight { get; set; }

        [Required]
        public string DocNumber { get; set; }
    }
}
