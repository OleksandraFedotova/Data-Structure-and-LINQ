using Newtonsoft.Json;
using System;

namespace DataStructuresAndLINQ
{
    public class TodoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComlete { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
