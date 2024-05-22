using Avro;
using Avro.Specific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Infra.IntegrationEvents
{
    public partial class AnswerCreated : ISpecificRecord
    {
        public static Schema _SCHEMA = Avro.Schema.Parse(@"{""type"":""record"",""name"":""AnswerCreated"",""namespace"":""Magic8Ball.Infra.IntegrationEvents"",""
                fields"":[
                    {""name"":""AnswerId"",""type"":""string""},
                    {""name"":""Message"",""type"":""string""},
                    {""name"":""MessageType"",""type"":[""null"",""string""]}
                ]}");

        private string _AnswerId;
        private string _Message;
        private string _MessageType;
        public virtual Schema Schema
        {
            get
            {
                return AnswerCreated._SCHEMA;
            }
        }

        public string AnswerId
        {
            get { return _AnswerId; }
            set { _AnswerId = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }

        public virtual object Get(int fieldPos)
        {
            switch (fieldPos)
            {
                case 0: return _AnswerId;
                case 1: return _Message;
                case 2: return _MessageType;
                default: throw new AvroRuntimeException($"Bad index ${fieldPos} in Get()");
            }
        }

        public virtual void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: this.AnswerId = (string)fieldValue; break;
                case 1: this.Message = (string)fieldValue; break;
                case 2: this.MessageType = (string)fieldValue; break;
                default: throw new AvroRuntimeException($"Bad index ${fieldPos} in Put()");
            }
        }
    }
}
