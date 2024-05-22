using Avro.Specific;
using Avro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Infra.IntegrationEvents
{
    public partial class AnswerDeleted : ISpecificRecord
    {
        public static Schema _SCHEMA = Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"AnswerDeleted\",\"namespace\":\"Magic8Ball.Infra.IntegrationEvents" +
                "\",\"fields\":[{\"name\":\"Id\",\"default\":\"\",\"type\":\"string\"}]}");
        private string _Id;
        public virtual Schema Schema
        {
            get
            {
                return AnswerDeleted._SCHEMA;
            }
        }
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }
        public virtual object Get(int fieldPos)
        {
            switch (fieldPos)
            {
                case 0: return this.Id;
                default: throw new AvroRuntimeException($"Bad index ${fieldPos} in Get()");
            };
        }
        public virtual void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: this.Id = (System.String)fieldValue; break;
                default: throw new AvroRuntimeException($"Bad index ${fieldPos} in Put()");
            };
        }
    }
}
