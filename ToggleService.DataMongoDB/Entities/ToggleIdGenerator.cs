using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace ToggleService.DataMongoDB.Entities
{
    public class ToggleIdGenerator: IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            return "Toggle_" + Guid.NewGuid();
        }

        public bool IsEmpty(object id)
        {
            return string.IsNullOrEmpty(id?.ToString());
        }
    }
}
