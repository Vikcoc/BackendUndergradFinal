using System;
using System.Collections.Generic;
using System.Text;
using Communication.SourceContributionDto;
using Communication.SourceVariantDto;

namespace Communication.SourcePlaceDto
{
    public class WaterSourcePlaceListingWithContributionDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid Picture { get; set; }
        public WaterSourceContributionDto Contribution { get; set; }
    }
}
