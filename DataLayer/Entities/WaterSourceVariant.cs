﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class WaterSourceVariant : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public List<WaterSourcePicture> Pictures { get; set; }

        public WaterSourceVariant()
        {
            Pictures = new List<WaterSourcePicture>();
        }

    }
}