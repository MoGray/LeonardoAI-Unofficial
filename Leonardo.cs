using Leonardo.Dataset;
using Leonardo.Dataset.Interfaces;
using Leonardo.Generation;
using Leonardo.Generation.Interfaces;
using Leonardo.InitImage;
using Leonardo.InitImage.Interfaces;
using Leonardo.Model;
using Leonardo.Model.Interfaces;
using Leonardo.User;
using Leonardo.User.Interfaces;
using Leonardo.Variation;
using Leonardo.Variation.Interfaces;
using System.Net.Http;
using System.Security.Authentication;

namespace Leonardo
{
    public sealed class LeonardoAPI
    {
        /// <summary>
		/// Base url for Leonardo
		/// for Leonardo, should be "https://cloud.leonardo.ai/api/rest/{0}/{1}"
		/// </summary>
		public string ApiUrlFormat { get; set; } = "https://cloud.leonardo.ai/api/rest/{0}/{1}";

        /// <summary>
        /// Version of the Rest Api
        /// </summary>
        public string ApiVersion { get; set; } = "v1";

        /// <summary>
        /// <see cref="HttpClient"/> to use when making calls to the API.
        /// </summary>
        internal HttpClient Client { get; }

        /// <summary>
		/// Optionally provide an IHttpClientFactory to create the client to send requests.
		/// </summary>
		public IHttpClientFactory HttpClientFactory { get; set; }

        /// <summary>
        /// The API authentication information to use for API calls
        /// </summary>
        public LeonardoAuthentication LeonardoAuthentication { get; }

        public IGenerationEndPoint Generate { get; }

        public IUserEndPoint User { get; }

        public IInitImageEndPoint InitImage { get; }

        public IVariationEndPoint Variation { get; }

        public IDatasetEndPoint Dataset { get; }

        public IModelEndPoint Model { get; }

        /// <summary>
        /// Creates a new client for the Leonardo API, handling auth and allowing for access to various API endpoints.
        /// </summary>
        /// <param name="leonardoAuthentication">The API authentication information to use for API calls,
        /// potentially loading from environment vars or from a config file.</param>
        /// <param name="httpClient">Optional, <see cref="HttpClient"/>.</param>
        /// <exception cref="AuthenticationException">Raised when authentication details are missing or invalid.</exception>
        public LeonardoAPI(LeonardoAuthentication leonardoAuthentication = null)
        {
            LeonardoAuthentication = leonardoAuthentication ?? LeonardoAuthentication.Default;

            if (string.IsNullOrWhiteSpace(LeonardoAuthentication?.ApiKey))
            {
                throw new AuthenticationException("You must provide API authentication.  Please set APIKey.");
            }

            Generate = new GenerationEndPoint(this);
            User = new UserEndPoint(this);
            InitImage = new InitImageEndPoint(this);
            Variation = new VariationEndPoint(this);
            Dataset = new DatasetEndPoint(this);
            Model = new ModelEndPoint(this);
        }
    }
}
