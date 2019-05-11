using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace MicroNetCore
{
    public interface IHttpRequestFeature
    {
        Uri Url { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }

    public interface IHttpResponseFeature
    {
        NameValueCollection Headers { get; }
        Stream Body { get; }
        int StatusCode { get; set; }
    }

    public interface IFeatureCollection : IDictionary<Type, object> { }

    public static partial class Extensions
    {
        public static T Get<T>(this IFeatureCollection features)
        {
            return features.TryGetValue(typeof(T), out var value) ? (T)value : default;
        }

        public static IFeatureCollection Set<T>(this IFeatureCollection features,T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }
    }

    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection { }
}
