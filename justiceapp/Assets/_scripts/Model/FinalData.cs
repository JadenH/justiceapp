using System.Collections.Generic;
using Newtonsoft.Json;

namespace _scripts.Model
{
    public class FinalData
    {
        [JsonProperty("finalText")]
        public string FinalText { get; set; }

        [JsonProperty("properties")]
        public List<string> Properties { get; set; }
    }
}