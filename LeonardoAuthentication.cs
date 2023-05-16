

using System.IO;
using System;

namespace Leonardo
{
    public sealed class LeonardoAuthentication
    {
        private const string LEONARDO_API_KEY = "LEONARDO_API_KEY";

        public string ApiKey { get; set; }

        /// <summary>
		/// Allows implicit casting from a string, so that a simple string API key can be provided in place of an instance of <see cref="APIAuthentication"/>
		/// </summary>
		/// <param name="key">The API key to convert into a <see cref="APIAuthentication"/>.</param>
		public static implicit operator LeonardoAuthentication(string key)
        {
            return new LeonardoAuthentication(key);
        }

        /// <summary>
		/// Instantiates a new Authentication object with the given <paramref name="apiKey"/>, which may be <see langword="null"/>.
		/// </summary>
		/// <param name="apiKey">The API key, required to access the API endpoint.</param>
		public LeonardoAuthentication(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        private static LeonardoAuthentication cachedDefault = null;

        /// <summary>
        /// The default authentication to use when no other auth is specified.  This can be set manually, or automatically loaded via environment variables or a config file.  <seealso cref="LoadFromEnv"/><seealso cref="LoadFromPath(string, string, bool)"/>
        /// </summary>
        public static LeonardoAuthentication Default
        {
            get
            {
                if (cachedDefault != null)
                    return cachedDefault;

                LeonardoAuthentication auth = LoadFromEnv();
                if (auth == null)
                    auth = LoadFromPath();
                if (auth == null)
                    auth = LoadFromPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

                cachedDefault = auth;
                return auth;
            }
            set
            {
                cachedDefault = value;
            }
        }

        /// <summary>
        /// Attempts to load api key from environment variables, as "LEONARDO_API_KEY".  Also loads org if from "OPENAI_ORGANIZATION" if present.
        /// </summary>
        /// <returns>Returns the loaded <see cref="LeonardoAuthentication"/> any api keys were found, or <see langword="null"/> if there were no matching environment vars.</returns>
        public static LeonardoAuthentication LoadFromEnv()
        {
            string key = Environment.GetEnvironmentVariable("LEONARDO_API_KEY");


            return string.IsNullOrEmpty(key) ? null : new LeonardoAuthentication(key);
        }

        /// <summary>
        /// Attempts to load api keys from a configuration file, by default ".leonardoai" in the current directory, optionally traversing up the directory tree
        /// </summary>
        /// <param name="directory">The directory to look in, or <see langword="null"/> for the current directory</param>
        /// <param name="filename">The filename of the config file</param>
        /// <param name="searchUp">Whether to recursively traverse up the directory tree if the <paramref name="filename"/> is not found in the <paramref name="directory"/></param>
        /// <returns>Returns the loaded <see cref="APIAuthentication"/> any api keys were found, or <see langword="null"/> if it was not successful in finding a config (or if the config file didn't contain correctly formatted API keys)</returns>
        public static LeonardoAuthentication LoadFromPath(string directory = null, string filename = ".leonardoai", bool searchUp = true)
        {
            if (directory == null)
                directory = Environment.CurrentDirectory;

            string key = null;
            var curDirectory = new DirectoryInfo(directory);

            while (key == null && curDirectory.Parent != null)
            {
                if (File.Exists(Path.Combine(curDirectory.FullName, filename)))
                {
                    var lines = File.ReadAllLines(Path.Combine(curDirectory.FullName, filename));
                    foreach (var l in lines)
                    {
                        var parts = l.Split('=', ':');
                        if (parts.Length == 2)
                        {
                            switch (parts[0].ToUpper())
                            {
                                case LEONARDO_API_KEY:
                                    key = parts[1].Trim();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (searchUp)
                {
                    curDirectory = curDirectory.Parent;
                }
                else
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(key))
                return null;

            return new LeonardoAuthentication(key);
        }
    }
}
