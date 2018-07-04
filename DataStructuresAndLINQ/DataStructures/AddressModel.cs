using Newtonsoft.Json;

namespace DataStructuresAndLINQ
{
    public class AddressModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
