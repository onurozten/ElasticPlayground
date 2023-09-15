using Bogus;
using EasticPlayground.Data;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using ElasticPlayground.Elastic;

namespace EasticPlayground.Elastic;

public class ElasticClient : IElasticClient
{
    private readonly ElasticsearchClient _elasticsearchClient;

    public ElasticClient(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }
    public async Task<IndexResponse> IndexAsync(object obj, string indexName)
    {
        return await _elasticsearchClient.IndexAsync(obj, indexName);
    }


    public BulkResponse IndexMany(IEnumerable<Employee> employees)
    {
        return _elasticsearchClient.IndexMany(employees, "people");
    }


    public async Task<DeleteByQueryResponse> DeleteAll(string indexName)
    {
        return await _elasticsearchClient.DeleteByQueryAsync(indexName, x => x.Query(x => x.MatchAll()));
    }


    public async Task<SearchResponse<Employee>> SearchWithHighlights(string searchTerm)
    {
        var result = await _elasticsearchClient.SearchAsync<Employee>(s => s
        .Index("people")
            .Query(q => q
            //.Term(m => m
            //    .Field(f => f.Firstname)
            //    .Query("Arslansungur")
            //)

            .Term(x => x.Lastname, searchTerm)
            
            )
        //.Highlight(h => h
        //    .PreTags(new List<string> { "<tag1>"})
        //    .PostTags(new List<string> { "</tag1>" })
        //    .Encoder(HighlighterEncoder.Html)
        //    .HighlightQuery(q => q
        //        .Match(m => m
        //            .Field(f => f.Firstname.Contains("standard"))
        //            .Query("Upton Sons Shield Rice Rowe Roberts")
        //        )
        //    )
        // )
        );

        return result;
    }



    public List<Employee> CreateSampleData()
    {
        var index = 0;
        var testEmployees = new Faker<Employee>(locale: "tr")
            .RuleFor(x => x.Firstname, c => c.Name.FirstName())
            .RuleFor(x => x.Lastname, c => c.Name.LastName())
            .RuleFor(x => x.Address, c => c.Address.FullAddress())
            .RuleFor(x => x.Id, c => index++)
            ;

        return testEmployees.Generate(100);
    }

}
