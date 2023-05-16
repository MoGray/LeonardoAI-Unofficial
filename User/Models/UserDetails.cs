using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leonardo.User.Models
{
    public class UserDetailsWrapper
    {
        [JsonProperty("user_details")]
        public List<UserDetails> UserDetails { get; set; }
    }
    public class UserDetails
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("showNsfw")]
        public bool? ShowNsfw { get; set; }

        [JsonProperty("tokenRenewalDate")]
        public DateTime? TokenRenewalDate { get; set; }

        [JsonProperty("subscriptionTokens")]
        public int SubscriptionTokens { get; set; }

        [JsonProperty("subscriptionGptTokens")]
        public int SubscriptionGptTokens { get; set; }

        [JsonProperty("subscriptionModelTokens")]
        public int SubscriptionModelTokens { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}
