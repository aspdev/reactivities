using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Helpers
{
    public class RavenId
    {
        public string Value { get; set; }
        public virtual Type ModelType { get; set; }
    }

    [JsonConverter(typeof(RavenIdConverter))]
    [ModelBinder(BinderType = typeof(RavenIdModelBinder))]
    public class RavenId<TModel> : RavenId
    {
        public RavenId(string value)
        {
            Value = value;
        }
        public override Type ModelType => typeof(TModel);
    }
}