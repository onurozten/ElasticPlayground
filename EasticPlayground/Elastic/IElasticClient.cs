using EasticPlayground.Data;
using Elastic.Clients.Elasticsearch;

namespace ElasticPlayground.Elastic;

public interface IElasticClient
{
    List<Employee> CreateSampleData();
    Task<DeleteByQueryResponse> DeleteAll(string indexName);
    Task<IndexResponse> IndexAsync(object obj, string indexName);
    BulkResponse IndexMany(IEnumerable<Employee> employees);
    Task<SearchResponse<Employee>> SearchWithHighlights(string searchTerm);
}
