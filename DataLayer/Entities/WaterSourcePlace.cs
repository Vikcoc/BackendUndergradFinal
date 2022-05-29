using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class WaterSourcePlace : BaseEntity
    {
        [MaxLength(50)]
        public string Nickname { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [Column(TypeName = "decimal(7, 5)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(7, 5)")]
        public decimal Longitude { get; set; }
        public Guid WaterSourceVariantId { get; set; }
        public WaterSourceVariant WaterSourceVariant { get; set; }
        [MinLength(1)]
        public List<WaterSourcePicture> Pictures { get; set; }
        [MinLength(1)]
        public List<WaterSourceContribution> Contributions { get; set; }


        public WaterSourcePlace()
        {
            Pictures = new List<WaterSourcePicture>();
            Contributions = new List<WaterSourceContribution>();
        }
    }
}
