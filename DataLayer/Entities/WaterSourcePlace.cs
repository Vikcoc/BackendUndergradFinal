using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class WaterSourcePlace : BaseEntity
    {
        [MaxLength(200)]
        public string Address { get; set; }
        [Column(TypeName = "decimal(7, 5)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(7, 5)")]
        public decimal Longitude { get; set; }
        public List<WaterSourcePicture> Pictures { get; set; }


        public WaterSourcePlace()
        {
            Pictures = new List<WaterSourcePicture>();
        }
    }
}
