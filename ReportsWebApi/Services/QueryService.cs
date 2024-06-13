using Dapper;
using Microsoft.Extensions.Primitives;
using ReportsWebApi.Data;
using System.Text.RegularExpressions;

namespace ReportsWebApi.Services
{
    public interface IQueryService
    {
        IEnumerable<dynamic> HandleSqlQuery(string query, Dictionary<string, StringValues>? parms);
        IEnumerable<dynamic> HandleOpenquery(string query, Dictionary<string, StringValues> parms, string db, int? take = null);
    }

    public class QueryService : IQueryService
    {
        private readonly DapperContext _dapper;

        public QueryService(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<dynamic> HandleSqlQuery(string query, Dictionary<string, StringValues>? parms)
        {
            if (parms != null && parms.Count > 0)
                query = HandleParameters(query, parms);

            IEnumerable<dynamic> result;

            using var connection = _dapper.CreateConnection();
            try
            {
                result = connection.Query(query);
            }
            catch (Exception ex)
            {
                result = new List<dynamic>();
                ((List<dynamic>)result).Add(new { Error = ex.Message });
            }
            return result;
        }

        public IEnumerable<dynamic> HandleOpenquery(string request, Dictionary<string, StringValues>? parms, string db, int? take = null)
        {
            var query = $"select * from openquery([{db}],'{request}')";

            var result = HandleSqlQuery(query, parms);

            if (take != null)
                result = result.Take((int)take);

            return result;
        }

        private static string HandleParameters(string query, Dictionary<string, StringValues> parms)
        {
            foreach (var parameter in parms)
            {
                query = Regex.Replace(query, $"@{parameter.Key}", parameter.Value.ToString());
            }
            return query;
        }
    }
}
