using DataLayer.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class WaterSourceContribution : BaseEntity
    {
        public Guid WaterUserId { get; set; }
        public WaterUser WaterUser { get; set; }
        public Guid WaterSourcePlaceId { get; set; }
        public WaterSourcePlace WaterSourcePlace { get; set; }
        public ContributionType ContributionType { get; set; }
        [MaxLength(200)]
        public string Details { get; set; }
        public Guid? RelatedContributionId { get; set; }
        public WaterSourceContribution RelatedContribution { get; set; }
    }
}
