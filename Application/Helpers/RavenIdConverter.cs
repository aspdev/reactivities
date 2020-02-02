
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Application.Helpers
{
    public class RavenIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType != typeof(RavenId<>))
                return false;

            var genericArguments = objectType.GetGenericArguments();
            return genericArguments.Any();
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dbModelType = objectType.GetGenericArguments()[0];
            var shortId = reader.Value as string;

            var outputType = typeof(RavenId<>).MakeGenericType(dbModelType);
            return Activator.CreateInstance(outputType, IdHelper.ForModel(dbModelType, shortId));

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var objectType = value.GetType();
            var dbModelType = objectType.GetGenericArguments()[0];

            var ravenId = value as RavenId;
            var fullId = ravenId.Value;
            var shortId = IdHelper.ForDto(dbModelType, fullId);

            writer.WriteValue(shortId);
        }
    }
}