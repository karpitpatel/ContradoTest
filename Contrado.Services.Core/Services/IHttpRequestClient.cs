using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services.Core.Services
{
    /// <summary>
    /// A convenience extension to the standard IHttpRequestClient interface that
    /// assumes that the object passed in both requests and responses are
    /// the same.
    /// </summary>
    /// <typeparam name="T">the type of object passed in requests and responses</typeparam>
    public interface IHttpRequestClient<T> : IHttpRequestClient<T, T>
    {
    }

    /// <summary>
    /// Represents an HTTP client that is capable of sending and receiving
    /// requests that contain a specific type of request and response model.
    /// </summary>
    /// <typeparam name="TReq">the type of object passed in requests</typeparam>
    /// <typeparam name="TRes">the type of object expected in responses</typeparam>
    public interface IHttpRequestClient<in TReq, TRes>
    {
        Task<TRes> GetRequest(string uri, IDictionary<string, string> headers = null);

        Task<TRes> PutRequest(string uri, TReq requestModel, IDictionary<string, string> headers = null);

        Task<TRes> PostRequest(string uri, TReq requestModel, IDictionary<string, string> headers = null);
    }
}
