using Bogus;
using EasticPlayground.Data;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace EasticPlayground.Elastic;

public class ElasticSearchClientBuilder
{
    private ElasticsearchClientSettings _elasticSettings;
    private ElasticsearchClient? _elasticsearchClient;
    private readonly string _indexName;

    public ElasticSearchClientBuilder(string indexName)
    {
        _elasticSettings??= new ElasticsearchClientSettings(new Uri("https://localhost:9200"));
        _indexName = indexName;
    }
    public ElasticSearchClientBuilder AddCertificateFingerprint(string certificateFingerprint)
    {
        _elasticSettings.CertificateFingerprint(certificateFingerprint);
        return this;
    }
    public ElasticSearchClientBuilder AddAuthentication(string userName, string password)
    {
        _elasticSettings.Authentication(new BasicAuthentication(userName, password));
        return this;
    }
    public async Task<ElasticsearchClient> Build()
    {
        _elasticsearchClient??= new ElasticsearchClient(_elasticSettings);

        if(!_elasticsearchClient.Indices.Exists(_indexName).Exists)
        {
            var response = await _elasticsearchClient.Indices.CreateAsync(_indexName);
            if(!response.IsValidResponse) 
                throw new Exception($"Error occured during index create: {_indexName}");
            
        }

        return _elasticsearchClient;
    }

}
