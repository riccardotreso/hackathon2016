using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NTTTube.Model;

namespace NTTTube.DB
{
    public class Repository
    {
        private static string ConnectionString = ConfigurationManager.AppSettings["ConnectionStringDB"].ToString();
        private static string DB = ConfigurationManager.AppSettings["Database"].ToString();
        private static string UserCollection = "users";
        private static string VideoCollection = "videos";


        public static User GetUser(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            IMongoQuery query = Query.EQ("username", username);

            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            var list = MongoDBHelper.GetCollection<User>(contextDB, UserCollection, query, Fields.Null);
            if (list != null)
                return list.FirstOrDefault();
            else
                return null;

        }
    }
}
