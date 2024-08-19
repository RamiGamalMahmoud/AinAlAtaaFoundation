using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public class Disbursements
    {
        public record GetByFamilyId(int FamilyId) : IRequest<IEnumerable<Disbursement>>;
        public record GetByRationCard(string RationCard) : IRequest<IEnumerable<Disbursement>>;

        public record GetHistory(
            Clan Clan, 
            Branch Branch, 
            BranchRepresentative BranchRepresentative,
            District District,
            DistrictRepresentative DistrictRepresentative,
            FamilyType FamilyType,
            SocialStatus SocialStatus,
            OrphanType OrphanType
            ) : IFilterParameters, IRequest<IEnumerable<Disbursement>>;
    }
}
