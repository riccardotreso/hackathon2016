using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using NTTTube.Model;

namespace NTTTube.Elastic
{
    public class Helper
    {
        ElasticClient _client;
        string endpoint = ConfigurationManager.AppSettings["ElasticSearch"].ToString();

        public void Connect()
        {
            var node = new Uri(endpoint);

            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("ntt-tube");

            _client = new ElasticClient(settings);
            
        }

        public void Index(Video source)
        {
            var index = _client.Index(source);
        }

        public List<Video> Search(string query)
        {
            var searchResults = _client.Search<Video>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                     .Term(p => p.description, query)
                )
            );

            return searchResults.Documents.ToList();
        }



    }
}
