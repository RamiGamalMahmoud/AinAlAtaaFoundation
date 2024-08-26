using AinAlAtaaFoundation.Features.FamiliesManagement;
using AinAlAtaaFoundation.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Tests.Families
{
    public class FamilyDataModelTests
    {
        [Fact]
        public void IsOrphan_ShouldBeTrue_WhenScocialStatusIsOrphan()
        {
            Family family = new Family()
            {
                Address = new Address()
                {
                    District = new District(),
                    FeaturedPoint = new FeaturedPoint()
                },
                DistrictRepresentative = new DistrictRepresentative()
                {
                    District = new District()
                },
                Applicant = new FamilyMember(),
                SocialStatus = new SocialStatus()
            };

            FamilyDataModel dataModel = new FamilyDataModel(family);

            dataModel.SocialStatus = new SocialStatus() { Name = "أيتام" };

            dataModel.IsOrphan.Should().BeTrue();
        }

        [Fact]
        public void IsSponsered_ShouldBeFalse_WhenScocialStatusIsNotOrphan()
        {
            Family family = new Family()
            {
                Address = new Address()
                {
                    District = new District(),
                    FeaturedPoint = new FeaturedPoint()
                },
                DistrictRepresentative = new DistrictRepresentative()
                {
                    District = new District()
                },
                Applicant = new FamilyMember(),
                SocialStatus = new SocialStatus(),
                IsSponsored = true
            };

            FamilyDataModel dataModel = new FamilyDataModel(family);

            dataModel.SocialStatus = new SocialStatus();

            dataModel.IsSponsered.Should().BeFalse();
        }
    }
}
