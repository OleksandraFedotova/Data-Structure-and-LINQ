using Newtonsoft.Json;
using System;

namespace DataStructuresAndLINQ
{
    public class PostModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("craetedAt")]
        public DateTime createdAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("likes")]
        public int Likes { get; set; }
    }
}
