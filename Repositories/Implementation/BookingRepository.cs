using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Data;
using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Repositories.Interface;

namespace PinesExecutiveTravelApi.Repositories.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly UserContext userContext;

        public BookingRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<TaxiOrder?> DeleteAsync(Guid id)
        {

            var existingBooking = await userContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBooking is null)
            {
                return null;
            }
            userContext.Reservations.Remove(existingBooking);
            await userContext.SaveChangesAsync();
            return existingBooking;


        }

        public async Task<TaxiOrder?> GetById(Guid id)
        {

            return await userContext.Reservations.FirstOrDefaultAsync(r => r.Id == id);


        }

       
    }
}
