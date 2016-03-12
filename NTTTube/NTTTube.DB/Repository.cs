using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
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
        private static string ChannelsCollection = "channels";


        #region USER

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

        public static string InsertUser(User entity)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            return MongoDBHelper.Insert<User>(contextDB, UserCollection, entity);

        }

        public static bool UpdateUser(string id, User value)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);

            IMongoUpdate update = Update
                .Set("name", value.name)
                .Set("nickname", value.nickname)
                .Set("foto", value.foto);

            return MongoDBHelper.Modify(contextDB, UserCollection, id, update);
        }

        #endregion

        #region VIDEO

        public static string InsertVideo(Video entity)
        {

            if (entity.comments == null)
                entity.comments = new List<Comment>();
            if (entity.like == null)
                entity.like = new List<string>();
            if (entity.unlike == null)
                entity.unlike = new List<string>();

            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            
            var id = MongoDBHelper.Insert<Video>(contextDB, VideoCollection, entity);
            var elasticHelper = new Elastic.Helper();
            elasticHelper.Connect();
            elasticHelper.Index(entity);

            return id;
            
        }

        public static Video GetVideo(string id)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            return MongoDBHelper.GetById<Video>(contextDB, VideoCollection, id);
        }

        /// <summary>
        /// Aggiorna la descrizione del video
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateVideo(string id, Video value)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);

            IMongoUpdate update = Update
                .Set("description", value.description);

            return MongoDBHelper.Modify(contextDB, VideoCollection, id, update);
        }


        /// <summary>
        /// Inserisce il commento per il video
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static bool InsertComment(string id, Comment comment)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            var insComm = new BsonDocument {
                { "text",comment.text },
                { "username", comment.username },
                { "data", comment.data }
            };

            IMongoUpdate update = Update.Push("comments", insComm);
            return MongoDBHelper.Modify(contextDB, VideoCollection, id, update);
        }


        /// <summary>
        /// Inserisce il like per il video
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static bool InsertLike(string id, string username)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);

            IMongoUpdate update = Update.Push("like", new BsonString(username));
            return MongoDBHelper.Modify(contextDB, VideoCollection, id, update);
        }

        /// <summary>
        /// Inserisce il like per il video
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static bool InsertUnlike(string id, string username)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);

            IMongoUpdate update = Update.Push("unlike", new BsonString(username));
            return MongoDBHelper.Modify(contextDB, VideoCollection, id, update);
        }



        public static string InsertChannel(string name)
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            return MongoDBHelper.Insert<Channel>(contextDB, ChannelsCollection, new Channel(name));
        }


        public static IEnumerable<Channel> GetAllChannel()
        {
            var contextDB = MongoDBHelper.GetContext(ConnectionString, DB);
            return MongoDBHelper.GetCollection<Channel>(contextDB, ChannelsCollection, Query.Null, Fields.Null);
        }

        #endregion


    }
}
