using AinAlAtaaFoundation.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal interface IFamilyGetter
    {
        Task<Family> GetFamily();
        Task<Disbursement> GetDisbursement();
        event PropertyChangedEventHandler PropertyChanged;
        void Clear();
        int FamilyId { get; set; }
        string RationCard { get; set; }
    }
}
