using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class WaterSourcePicture : BaseEntity
    {
        [MaxLength(100)]
        public string Uri { get; set; }
        public Guid? WaterSourceVariantId { get; set; }
        public WaterSourceVariant WaterSourceVariant { get; set; }
        public Guid? WaterSourcePlaceId { get; set; }
        public WaterSourcePlace WaterSourcePlace { get; set; }
    }
}
