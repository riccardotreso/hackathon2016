using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NTTTube.DB;
using NTTTube.Model;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            var contextDB = MongoDBHelper.GetContext("mongodb://localhost", "NTTTube");
            IMongoQuery query = Query.Null;

            var collection = MongoDBHelper.GetCollection<User>(contextDB, "users", query, Fields.Null);

            Console.Read();

        
        }
    }
}
