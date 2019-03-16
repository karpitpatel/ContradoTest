using Contrado.Services.Core.Exceptions;
using Contrado.Services.Core.Models.Error;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Contrado.Services.Core.Services
{
    /// <summary>
    /// Extends JsonRequestClient to support a shared request/response format.
    /// </summary>
    /// <typeparam name="T">the type of object passed in requests and responses</typeparam>
    public class JsonRequestClient<T> : JsonRequestClient<T, T>, IHttpRequestClient<T>
    {
        public JsonRequestClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }

    /// <summary>
    /// A base implementation of IHttpRequestClient that uses an underlying JSON
    /// message format to support all of the request methods required.
    /// </summary>
    /// <typeparam name="TReq">the type of object passed in requests</typeparam>
    /// <typeparam name="TRes">the type of object expected in responses</typeparam>
    public class JsonRequestClient<TReq, TRes> : IHttpRequestClient<TReq, TRes>
    {
        protected HttpClient httpClient { get; set; }

        public JsonRequestClient()
        {
            this.httpClient = ServiceLocator.Current.GetInstance<HttpClient>();
        }

        public JsonRequestClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Performs a GET request against the given URI. The set of optional
        /// header values are applied to the request if supplied. On success,
        /// a decoded JSON object of the specified type is returned.
        /// </summary>
        /// <param name="uri">the uri to invoke</param>
        /// <param name="headers">an optional set of header to add to the request</param>
        /// <returns>the response returned to the request</returns>
        public virtual async Task<TRes> GetRequest(string uri, IDictionary<string, string> headers = null)
        {
            return await GetRequest<TRes>(uri, headers);
        }

        /// <summary>
        /// Performs a GET request against the given URI. The set of optional
        /// header values are applied to the request if supplied. On success,
        /// a decoded JSON object of the specified type is returned.
        /// </summary>
        /// <param name="uri">the uri to invoke</param>
        /// <param name="headers">an optional set of header to add to the request</param>
        /// <returns>the response returned to the request</returns>
        public virtual async Task<TCustom> GetRequest<TCustom>(string uri, IDictionary<string, string> headers = null)
        {
            try
            {
                var response = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<TCustom>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            throw new Exception();
        }

        /// <summary>
        /// Performs a POST request against the given URI, passing the request
        /// model supplied as post data. The set of optional header values are
        /// applied to the request if supplied. On success, a decoded JSON object
        /// of the specified type is returned.
        /// </summary>
        /// <param name="uri">the uri to invoke</param>
        /// <param name="requestModel">the payload for this request</param>
        /// <param name="headers">an optional set of header to add to the request</param>
        /// <returns>the response returned to the request</returns>
        public virtual async Task<TRes> PostRequest(string uri, TReq requestModel, IDictionary<string, string> headers = null)
        {
            var response = await httpClient.PostAsJsonAsync(uri, requestModel);
            return await ProcessResponse(response);
        }

        /// <summary>
        /// Performs a POST request against the given URI, using the set of form
        /// values provided. In this method, TReq is ignored, and only TRes is
        /// used to format the response. The set of optional header values are
        /// applied to the request if supplied. On success, a decoded JSON object
        /// of the specified type is returned.
        /// </summary>
        /// <param name="uri">the uri to invoke</param>
        /// <param name="formValues">an optional set of form values to pass</param>
        /// <param name="headers">an optional set of header to add to the request</param>
        /// <returns>the result of the request</returns>
        public virtual async Task<TRes> PostRequest(string uri, IDictionary<string, string> formValues = null, IDictionary<string, string> headers = null)
        {
            var response = await httpClient.PostAsync(uri, new FormUrlEncodedContent(formValues));
            return await ProcessResponse(response);
        }

        /// <summary>
        /// Performs a PUT request against the given URI, passing the request
        /// model supplied as post data. The set of optional header values are
        /// applied to the request if supplied. On success, a decoded JSON object
        /// of the specified type is returned.
        /// </summary>
        /// <param name="uri">the uri to invoke</param>
        /// <param name="requestModel">the payload for this request</param>
        /// <param name="headers">an optional set of header to add to the request</param>
        /// <returns>the response returned to the request</returns>
        public virtual async Task<TRes> PutRequest(string uri, TReq requestModel, IDictionary<string, string> headers = null)
        {
            var response = await httpClient.PutAsJsonAsync(uri, requestModel);
            return await ProcessResponse(response);
        }

        /// <summary>
        /// Given a response to one of the POST/PUT requests executed by this
        /// client, this method will attempt to extract and return the content
        /// according to the result. If the response is a success, the response
        /// type of this class is deserialized and returned from it. Otherwise,
        /// if the response contains model state errors, they are extracted and
        /// thrown inside a ModelStateException. If neither of the cases above
        /// hold, an exception is thrown containing the error code returned.
        /// </summary>
        /// <param name="response">the response to be processed</param>
        /// <returns>the deserialised response object, on success</returns>
        protected async Task<TRes> ProcessResponse(HttpResponseMessage response)
        {
            if (response != null)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Deserialise the result on a successful response.
                    return JsonConvert.DeserializeObject<TRes>(content);
                }
                else
                {
                    ModelStateErrorModel errorModel = null;
                    var httpError = JsonConvert.DeserializeObject<HttpError>(content);

                    if (httpError != null)
                    {
                        var modelStateErrors = httpError.ContainsKey("ModelState") ? httpError["ModelState"] as JObject : null;

                        if (modelStateErrors != null)
                        {
                            // The response contains model state error information.
                            // Extract this data into a local representation, ready
                            // to be included in an exception below.
                            errorModel = new ModelStateErrorModel
                            {
                                ModelState = new List<ModelStateItemModel>()
                            };
                            foreach (var modelStateError in modelStateErrors)
                            {
                                if (modelStateError.Value != null)
                                {
                                    foreach (var message in modelStateError.Value.Values<string>())
                                    {
                                        errorModel.ModelState.Add(new ModelStateItemModel
                                        {
                                            Key = modelStateError.Key,
                                            Message = message
                                        });
                                    }
                                }
                            }
                        }
                    }

                    // If model state error information was extracted from the
                    // response, throw a new exception containing that info.
                    // Otherwise, invoke the standard exception (Ensure... will
                    // throw an exception, since the success check above failed).
                    if (errorModel != null)
                    {
                        throw new ModelStateException { ModelStateError = errorModel };
                    }
                    else
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }

            // This should typically not happen (we should receive a non-null
            // response to all requests), but fail gracefully if it does.
            return default(TRes);
        }

       

        
    }
}
