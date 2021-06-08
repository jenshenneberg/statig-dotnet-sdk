﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Statsig
{
    public class StatsigUser
    {
        internal Dictionary<string, string> properties;
        internal Dictionary<string, object> customProperties;

        [JsonProperty("userID")]
        public string UserID {
            get
            {
                return properties.TryGetValue("userID", out string value) ? value : null;
            }
            set
            {
                properties["userID"] = value;
            }
        }
        [JsonProperty("email")]
        public string Email
        {
            get
            {
                return properties.TryGetValue("email", out string value) ? value : null;
            }
            set
            {
                properties["email"] = value;
            }
        }
        [JsonProperty("ip")]
        public string IPAddress
        {
            get
            {
                return properties.TryGetValue("ip", out string value) ? value : null;
            }
            set
            {
                properties["ip"] = value;
            }
        }
        [JsonProperty("userAgent")]
        public string UserAgent
        {
            get
            {
                return properties.TryGetValue("userAgent", out string value) ? value : null;
            }
            set
            {
                properties["userAgent"] = value;
            }
        }
        [JsonProperty("country")]
        public string Country
        {
            get
            {
                return properties.TryGetValue("country", out string value) ? value : null;
            }
            set
            {
                properties["country"] = value;
            }
        }
        [JsonProperty("locale")]
        public string Locale
        {
            get
            {
                return properties.TryGetValue("locale", out string value) ? value : null;
            }
            set
            {
                properties["locale"] = value;
            }
        }
        [JsonProperty("clientVersion")]
        public string ClientVersion
        {
            get
            {
                return properties.TryGetValue("clientVersion", out string value) ? value : null;
            }
            set
            {
                properties["clientVersion"] = value;
            }
        }
        [JsonProperty("custom")]
        public IReadOnlyDictionary<string, object> CustomProperties => customProperties;

        public StatsigUser()
        {
            properties = new Dictionary<string, string>();
            customProperties = new Dictionary<string, object>();
        }

        public void AddCustomProperty(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be empty.", "key");
            }
            customProperties[key] = value;
        }
    }
}