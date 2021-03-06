﻿using System;
using System.Collections.Generic;

namespace PortableRest
{

    /// <summary>
    /// Specifies the parameters for the HTTP request that will be executed against a given resource.
    /// </summary>
    public class RestRequest
    {

        #region Private Members

        /// <summary>
        /// 
        /// </summary>
        private List<KeyValuePair<string, string>> UrlSegments { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// A string representation of the specific resource to access, using ASP.NET MVC-like replaceable tokens.
        /// </summary>
        public string Resource { private get; set; }

        /// <summary>
        /// The HTTP method to use for the request.
        /// </summary>
        public string Method { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new RestRequest instance, specifying the request will be an HTTP GET.
        /// </summary>
        public RestRequest()
        {
            UrlSegments = new List<KeyValuePair<string, string>>();
            Method = "GET";
        }

        /// <summary>
        /// Creates a new RestRequest instance for a given Resource and Method.
        /// </summary>
        /// <param name="resource">The specific resource to access.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        public RestRequest(string resource, string method)
        {
            UrlSegments = new List<KeyValuePair<string, string>>();
            Method = method;
            Resource = resource;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddUrlSegment(string key, string value)
        {
            UrlSegments.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public string GetFormattedResource(string baseUrl)
        {
            foreach (var segment in UrlSegments)
            {
                Resource = Resource.Replace("{" + segment.Key + "}", Uri.EscapeUriString(segment.Value));
            }

            if (!string.IsNullOrEmpty(Resource) && Resource.StartsWith("/"))
            {
                Resource = Resource.Substring(1);
            }

            if (!string.IsNullOrEmpty(baseUrl))
            {
                Resource = string.IsNullOrEmpty(Resource) ? baseUrl : string.Format("{0}/{1}", baseUrl, Resource);
            }

            return Resource;
        }

        #endregion

    }
}
