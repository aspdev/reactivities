using System;
using System.Collections.Generic;
using Domain;

namespace Application.Helpers
{
    public class IdHelper
    {
        private static readonly Dictionary<Type, string> ModelPrefixes = new Dictionary<Type, string>
        {
            {typeof(Employee), "Employees/"}
        };

        public static string ForModel(Type modelType, string id)
        {
            if (id == null)
            {
                return null;
            }

            var prefix = ModelPrefixes[modelType];

            return id.StartsWith(prefix) ? id : $"{prefix}{id}";
        }

        public static string ForDto(Type modelType, string id)
        {
            var prefix = ModelPrefixes[modelType];

            if (string.IsNullOrEmpty(id))
            {
                return id;
            }

            return id.StartsWith(prefix) ? id.Substring(prefix.Length) : id;
        }

    }
}