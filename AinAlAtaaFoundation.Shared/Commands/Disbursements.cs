using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public class Disbursements
    {
        public record GetByFamilyId(int FamilyId) : IRequest<IEnumerable<Disbursement>>;
        public record GetByRationCard(string RationCard) : IRequest<IEnumerable<Disbursement>>;
        public record CommandGetLastDisbursementByFamilyId(int FamilyId) : IRequest<Disbursement>;
        public record CommandGetLastDisbursementByRationCard(string RationCard) : IRequest<Disbursement>;

        public record GetHistory(
            Clan Clan, 
            Branch Branch, 
            BranchRepresentative BranchRepresentative,
            District District,
            DistrictRepresentative DistrictRepresentative,
            FamilyType FamilyType,
            SocialStatus SocialStatus,
            OrphanType OrphanType,
            bool? IsSponsored = null
            ) : IFilterParameters, IRequest<IEnumerable<Disbursement>>;

        public record CommandRemove(Disbursement Disbursement) : IRequest<bool>;
        public record ShowFamilyDisbursementsHistory(Family Family) : IRequest;
        public record GetDisbursementsHistoryByDateCommand(DateTime Date) : IRequest<IEnumerable<Disbursement>>;
    }
}
