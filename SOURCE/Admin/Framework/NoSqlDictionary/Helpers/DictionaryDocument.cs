using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlDictionary.Helpers
{
    public class DictionaryDocument<TKey,TValue>
    {
        public ObjectId Id { get; set; }
        
        public TKey Key { get; set; }

        public TValue Values { get; set; }

       
    }
}
