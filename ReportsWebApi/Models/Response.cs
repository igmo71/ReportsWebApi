using Microsoft.EntityFrameworkCore;

namespace ReportsWebApi.Models
{
    [Keyless]
    public class Response
    {
        public string? Год { get; set; }
        public string? Пользователь { get; set; }
        public string? Партнер { get; set; }
        public string? СуммаВыручки { get; set; }
        public string? Маржа { get; set; }
    }
}
