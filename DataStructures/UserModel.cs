using Newtonsoft.Json;
using System;

namespace DataStructuresAndLINQ
{
    public class UserModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("creaedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
