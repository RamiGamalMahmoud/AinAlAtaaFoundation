using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;

namespace AinAlAtaaFoundation.Shared.Commands
{
    public class Disbursements
    {
        public record GetByFamily(int FamilyId) : IRequest<IEnumerable<Disbursement>>;
    }
}
