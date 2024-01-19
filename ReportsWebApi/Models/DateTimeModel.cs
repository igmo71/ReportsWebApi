using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ReportsWebApi.Models
{
    [Keyless]
    public class DateTimeModel
    {
        public DateTime Value { get; set; }
    }
}
