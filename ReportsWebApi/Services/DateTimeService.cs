using Microsoft.EntityFrameworkCore;
using ReportsWebApi.Data;

namespace ReportsWebApi.Services
{
    public interface IDateTimeService
    {
        DateTime? GetCurrentDateTime();
    }

    public class DateTimeService(AppDbContext context) : IDateTimeService
    {
        private readonly AppDbContext _context = context;

        public DateTime? GetCurrentDateTime()
        {
            var result = _context.DateTimeModel.FromSql($"select GETDATE() as Value").FirstOrDefault();
            return result?.Value;
        }
    }
}
