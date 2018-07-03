using Newtonsoft.Json;
using System;

namespace DataStructuresAndLINQ
{
    public class CommentModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("postId")]
        public int PostId { get; set; }

        [JsonProperty("likes")]
        public int Likes { get; set; }
    }
}
