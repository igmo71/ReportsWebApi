using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using ReportsWebApi.Data;
using ReportsWebApi.Models;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace ReportsWebApi.Services
{
    public interface IQueryService
    {

        List<Response> GetMdxResponse();
        IEnumerable<dynamic> HandleOpenquery(string db, string query, Dictionary<string, StringValues> parms, int? take = null);

    }

    public class QueryService : IQueryService
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapper;

        public QueryService(AppDbContext context, DapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public List<Response> GetMdxResponse()
        {
            List<Response> result = _context.MdxResponse.FromSql(@$"
                SELECT top (100)
	                [[Календарь]].[Год]].[Год]].[MEMBER_CAPTION]]] as Год
	                ,[[Основные Менеджеры]].[Пользователи]].[Пользователи]].[MEMBER_CAPTION]]] as Пользователь
	                ,[[Партнеры - Основные Менеджеры]].[Партнеры - Пользователи Список]].[Партнеры - Пользователи Список]].[MEMBER_CAPTION]]] as Партнер
	                ,[[Measures]].[Сумма Выручки]]] as [СуммаВыручки]
	                ,[[Measures]].[Маржа]]] as [Маржа]
                FROM [WH-dev].[dbo].[v_mdx]").ToList();

            return result;
        }

        public IEnumerable<dynamic> HandleOpenquery(string db, string request, Dictionary<string, StringValues> parms, int? take = null)
        {
            var query = ParseOpenquery(db, request, parms);

            using var connection = _dapper.CreateConnection();

            IEnumerable<dynamic> result = connection.Query(query);

            if (take != null)
                result = result.Take((int)take);

            return result;
        }

        private string ParseOpenquery(string db, string request, Dictionary<string, StringValues> parms)
        {
            var query = $"select * from openquery([{db}],'{request}')";
            foreach (var parameter in parms)
            {
                query = Regex.Replace(query, $"@{parameter.Key}", parameter.Value.ToString());
            }
            return query;
        }
    }
}
