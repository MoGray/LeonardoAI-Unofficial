
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using Leonardo.GenericModels;

namespace Leonardo
{
    public abstract class EndPointBase
    {
        /// <summary>
        /// The internal reference to the API, mostly used for authentication
        /// </summary>
        protected readonly LeonardoAPI _Api;

        /// <summary>
        /// Constructor of the api endpoint base, to be called from the contructor of any devived classes.  Rather than instantiating any endpoint yourself, access it through an instance of <see cref="LeonardoAPI"/>.
        /// </summary>
        /// <param name="api"></param>
        internal EndPointBase(LeonardoAPI api)
        {
            this._Api = api;
        }

        /// <summary>
        /// The name of the endpoint, which is the final path segment in the API URL.  Must be overriden in a derived class.
        /// </summary>
        protected abstract string Endpoint { get; }

        /// <summary>
        /// Gets the URL of the endpoint, based on the base Leonardo API URL followed by the endpoint name."
        /// </summary>
        protected string Url
        {
            get
            {
                return string.Format(_Api.ApiUrlFormat, _Api.ApiVersion, Endpoint);
            }
        }

        /// <summary>
        /// Gets an HTTPClient with the appropriate authorization and other headers set
        /// </summary>
        /// <returns>The fully initialized HttpClient</returns>
        /// <exception cref="AuthenticationException">Thrown if there is no valid authentication./> for details.</exception>
        protected HttpClient GetClient()
        {
            if (_Api.LeonardoAuthentication?.ApiKey is null)
            {
                throw new AuthenticationException("You must provide API authentication.");
            }

            HttpClient client;
            var clientFactory = _Api.HttpClientFactory;
            if (clientFactory != null)
            {
                client = clientFactory.CreateClient();
            }
            else
            {
                client = new HttpClient();
            }

            return client;
        }

        /// <summary>
		/// Formats a human-readable error message relating to calling the API and parsing the response
		/// </summary>
		/// <param name="resultAsString">The full content returned in the http response</param>
		/// <param name="response">The http response object itself</param>
		/// <param name="name">The name of the endpoint being used</param>
		/// <param name="description">Additional details about the endpoint of this request (optional)</param>
		/// <returns>A human-readable string error message.</returns>
		protected string GetErrorMessage(string resultAsString, HttpResponseMessage response, string name, string description = "")
        {
            return $"Error at {name} ({description}) with HTTP status code: {response.StatusCode}. Content: {resultAsString ?? "<no content>"}";
        }

        /// <summary>
		/// Sends an HTTP request and returns the response.  Does not do any parsing, but does do error handling.
		/// </summary>
		/// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
		/// <param name="verb">(optional) The HTTP verb to use, for example "<see cref="HttpMethod.Get"/>".  If omitted, then "GET" is assumed.</param>
		/// <param name="postData">(optional) A json-serializable object to include in the request body.</param>
		/// <param name="streaming">(optional) If true, streams the response.  Otherwise waits for the entire response before returning.</param>
		/// <returns>The HttpResponseMessage of the response, which is confirmed to be successful.</returns>
		/// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned</exception>
		private async Task<HttpResponseMessage> HttpRequestRaw(string url = null, HttpMethod verb = null, object postData = null, bool addAuthHeader = true, bool streaming = false)
        {
            if (string.IsNullOrEmpty(url))
                url = this.Url;

            if (verb == null)
                verb = HttpMethod.Get;

            using var client = GetClient();

            HttpResponseMessage response = null;
            string resultAsString = null;
            HttpRequestMessage req = new HttpRequestMessage(verb, url);

            if(addAuthHeader)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Api.LeonardoAuthentication.ApiKey);
            }

            if (postData != null)
            {
                if (postData is HttpContent)
                {
                    req.Content = postData as HttpContent;
                }
                else
                {
                    string jsonContent = JsonConvert.SerializeObject(postData, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");
                    req.Content = stringContent;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            response = await client.SendAsync(req, streaming ? HttpCompletionOption.ResponseHeadersRead : HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                try
                {
                    resultAsString = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    resultAsString = "Additionally, the following error was thrown when attemping to read the response content: " + e.ToString();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException("Leonardo rejected your authorization, most likely due to an invalid API Key. Full API response follows: " + resultAsString);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException("Leonardo had an internal server error, which can happen occasionally.  Please retry your request.  " + GetErrorMessage(resultAsString, response, Endpoint, url));
                }
                else
                {
                    throw new HttpRequestException(GetErrorMessage(resultAsString, response, Endpoint, url));
                }
            }
        }

        internal async Task UploadImage(ImageUpload imageUpload)
        {
            try
            {
                // If fileName does not have extension, add it.
                if (!Utilities.CheckIfImageFormat(imageUpload.FileName) && !String.IsNullOrEmpty(imageUpload.FileName))
                {
                    imageUpload.FileName = $"{imageUpload.FileName}.{imageUpload.Extension}";
                }

                var fieldsJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(imageUpload.Fields);

                var formData = new MultipartFormDataContent();

                foreach (var entry in fieldsJson)
                {
                    formData.Add(new StringContent(entry.Value), entry.Key);
                }

                formData.Add(new StreamContent(imageUpload.Stream), "file", String.IsNullOrEmpty(imageUpload.FileName) ? $"{Utilities.GenerateGuidString()}.{imageUpload.Extension}" : imageUpload.FileName);
                await HttpPost<object>(imageUpload.Url, formData, false);

            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading image to server with message: " + ex.Message, ex);
            }
        }

        /// <summary>
		/// Sends an HTTP Request and does initial parsing
		/// </summary>
		/// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
		/// <param name="verb">(optional) The HTTP verb to use, for example "<see cref="HttpMethod.Get"/>".  If omitted, then "GET" is assumed.</param>
		/// <param name="postData">(optional) A json-serializable object to include in the request body.</param>
		/// <returns>An awaitable Task with the parsed result of type <typeparamref name="T"/></returns>
		/// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned or if the result couldn't be parsed.</exception>
		private async Task<T> HttpRequest<T>(string url = null, HttpMethod verb = null, object postData = null, bool addAuthHeader = true)
        {
            var response = await HttpRequestRaw(url, verb, postData, addAuthHeader);
            string resultAsString = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<T>(resultAsString);

            return res;
        }

        /// <summary>
		/// Sends an HTTP Get request and does initial parsing
		/// </summary>
		/// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
		/// <returns>An awaitable Task with the parsed result of type <typeparamref name="T"/></returns>
		/// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned or if the result couldn't be parsed.</exception>
		internal async Task<T> HttpGet<T>(string url = null)
        {
            return await HttpRequest<T>(url, HttpMethod.Get);
        }

        /// <summary>
        /// Sends an HTTP Post request and does initial parsing
        /// </summary>
        /// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
        /// <param name="postData">(optional) A json-serializable object to include in the request body.</param>
        /// <returns>An awaitable Task with the parsed result of type <typeparamref name="T"/></returns>
        /// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned or if the result couldn't be parsed.</exception>
        internal async Task<T> HttpPost<T>(string url = null, object postData = null, bool addAuthHeader = true)
        {
            return await HttpRequest<T>(url, HttpMethod.Post, postData, addAuthHeader);
        }


        /// <summary>
        /// Sends an HTTP Delete request and does initial parsing
        /// </summary>
        /// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
        /// <param name="postData">(optional) A json-serializable object to include in the request body.</param>
        /// <returns>An awaitable Task with the parsed result of type <typeparamref name="T"/></returns>
        /// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned or if the result couldn't be parsed.</exception>
        internal async Task<T> HttpDelete<T>(string url = null, object postData = null)
        {
            return await HttpRequest<T>(url, HttpMethod.Delete, postData);
        }


        /// <summary>
        /// Sends an HTTP Put request and does initial parsing
        /// </summary>
        /// <param name="url">(optional) If provided, overrides the url endpoint for this request.  If omitted, then <see cref="Url"/> will be used.</param>
        /// <param name="postData">(optional) A json-serializable object to include in the request body.</param>
        /// <returns>An awaitable Task with the parsed result of type <typeparamref name="T"/></returns>
        /// <exception cref="HttpRequestException">Throws an exception if a non-success HTTP response was returned or if the result couldn't be parsed.</exception>
        internal async Task<T> HttpPut<T>(string url = null, object postData = null)
        {
            return await HttpRequest<T>(url, HttpMethod.Put, postData);
        }
    }
}
