using PinesExecutiveTravelApi.Models.Domain;

namespace PinesExecutiveTravelApi.Repositories.Interface
{
    public interface IBookingRepository
    {

        Task<TaxiOrder?> GetById(Guid id);
        Task<TaxiOrder?> DeleteAsync(Guid id);

       


    }
}
