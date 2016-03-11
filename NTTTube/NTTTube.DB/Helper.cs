using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NTTTube.Model;

namespace NTTTube.DB
{
    public class MongoDBHelper
    {
        public static MongoDatabase GetContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);

            //get server Object;
            var server = client.GetServer();

            //get Database
            var db = server.GetDatabase(dbName);

            return db;
        }

        public static string Insert<T>(MongoDatabase contextDB, string collectionName, T Entity) where T : BaseEntity
        {
            contextDB.GetCollection(collectionName).Insert(Entity);
            return ((BaseEntity)Entity).Id;
        }


        public static IEnumerable<T> GetCollection<T>(MongoDatabase contextDB, string collectionName, IMongoQuery query, IMongoFields field)
        {
            var mc = contextDB.GetCollection<T>(collectionName).Find(query).SetFields(field);
            if (mc.Count() == 0)
                return null;
            var list = mc.AsEnumerable();
            return list;
        }


        public static T GetById<T>(MongoDatabase contextDB, string collectionName, string ID)
        {
            IMongoQuery query = Query.EQ("_id", new BsonObjectId(new ObjectId(ID)));
            return contextDB.GetCollection<T>(collectionName).FindOne(query);
        }

        public static bool Modify(MongoDatabase contextDB, string collectionName, string ID, IMongoUpdate Update)
        {
            IMongoQuery query = Query.EQ("_id", new BsonObjectId(new ObjectId(ID)));
            var result = contextDB.GetCollection(collectionName).FindAndModify(query, null, Update);

            return result.Ok;
        }

        public static bool Modify(MongoDatabase contextDB, string collectionName, IMongoQuery query, IMongoUpdate Update, UpdateFlags flag)
        {
            var result = contextDB.GetCollection(collectionName).Update(query, Update, flag);

            return result.Ok;
        }


        public static bool Delete(MongoDatabase contextDB, string collectionName, string ID)
        {
            IMongoQuery query = Query.EQ("_id", new BsonObjectId(new ObjectId(ID)));
            var result = contextDB.GetCollection(collectionName).FindAndRemove(query, null);

            return result.Ok;
        }
    }
}
