using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlDictionary.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoSqlDictionary
{
    [Serializable]
    public class MongoDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {

        private ObjectId _dicId;

        MongoHelper<DictionaryDocument<TKey,TValue>> _service;
        private ObjectId DicId { get
            {

                return ObjectId.GenerateNewId();
            }
        }

        private string collectionName { get; set; }


        

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return _service.Collection.AsQueryable().Select(x => x.Key).ToList();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var data = _service.Collection.Find(x=>true).ToList();
                return data.Select(x=>x.Values).ToList();
            }
        }

        public int Count
        {
            get
            {
                
                return (int)_service.Collection.Find(GetBuilder().ToBsonDocument()).Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private FilterDefinitionBuilder<DictionaryDocument<TKey, TValue>> GetBuilder()
        {
            var builder = Builders<DictionaryDocument<TKey, TValue>>.Filter;
             builder.Eq("_id", DicId);
            return builder;
        }

        private FilterDefinition<DictionaryDocument<TKey, TValue>> GetFilters(TKey key)
        {
            var builder = GetBuilder();
            var filter = builder.Eq("Key", key);
            return filter;
        }

        private FilterDefinition<DictionaryDocument<TKey, TValue>> GetFilters(KeyValuePair<TKey,TValue> key)
        {
            var builder = GetBuilder();
            var filter = builder.Eq("Key", key.Key) & builder.Eq("Values", key.Value);
            return filter;
        }

        public TValue this[TKey key]
        {

            get
            {
                var filter = GetFilters(key);

                var data = _service.Collection.Find(filter).Single();
                return data.Values;
            }

            set
            {
                var filter = GetFilters(key);

                var document = new DictionaryDocument<TKey, TValue>
                {
                    Id = DicId,
                    Key = key,
                    Values = value
                };
                var data = _service.Collection.FindOneAndReplace(filter, document);
            }
        }

        public MongoDictionary():base(){
            _dicId = ObjectId.GenerateNewId();
            string collectionName = string.Empty;
            if (typeof(TValue).GenericTypeArguments.Length>0)
            {
                collectionName = $"List_{typeof(TValue).GenericTypeArguments[0].FullName}";
            }
            else
            {
                collectionName = typeof(TValue).FullName;
            }
            _service = new MongoHelper<DictionaryDocument<TKey, TValue>>(collectionName);
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new System.ArgumentException("An item with same key added");
            }
            var document = new DictionaryDocument<TKey, TValue>
            {
                Id = DicId,
                Key = key,
                Values = value
            };
            _service.Collection.InsertOne(document);
            _service.Collection.Indexes.CreateOne(Builders<DictionaryDocument<TKey, TValue>>.IndexKeys.Ascending(_ => _.Key));
        }

        public bool ContainsKey(TKey key)
        {
            var filter = GetFilters(key);
            return _service.Collection.Find(filter).Any();

        }

        public bool Remove(TKey key)
        {
            try
            {
                var filter = GetFilters(key);
                _service.Collection.DeleteOne(filter);
                return true;
            }
            catch (Exception)
            {
                
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var filter = GetFilters(key);
            var data = _service.Collection.Find(filter).SingleOrDefault();
            if (data==null)
            {
                value = default(TValue);
                return false;
            }
            value = data.Values;
            return true;

        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                throw new System.ArgumentException("An item with same key added");
                
            }
            var document = new DictionaryDocument<TKey, TValue>
            {
                Id = DicId,
                Key = item.Key,
                Values = item.Value
            };
            _service.Collection.InsertOne(document);
        }

        public void Clear()
        {
            var filter = new BsonDocument();
            _service.Collection.DeleteMany(filter);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var filter = GetFilters(item);
            return _service.Collection.Find(filter).Any();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                var filter = GetFilters(item);
                _service.Collection.DeleteOne(filter);
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
