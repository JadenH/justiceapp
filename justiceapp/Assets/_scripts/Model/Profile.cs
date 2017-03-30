using Newtonsoft.Json;

namespace _scripts.Model
{
    public class Profile
    {
        [JsonProperty("Citizenship")]
        public string Citizenship { get; set; }

        [JsonProperty("Date of Birth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("Education")]
        public string Education { get; set; }

        [JsonProperty("Employment")]
        public string Employment { get; set; }

        [JsonProperty("File Name")]
        public string FileName { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Marital Status")]
        public string MaritalStatus { get; set; }

        [JsonProperty("Max")]
        public string Max { get; set; }

        [JsonProperty("Min")]
        public string Min { get; set; }

        [JsonProperty("Offense")]
        public string Offense { get; set; }

        [JsonProperty("Prior drug offense")]
        public string PriorDrugOffense { get; set; }

        [JsonProperty("Prior felonies")]
        public string PriorFelonies { get; set; }

        [JsonProperty("Prior sex offenses")]
        public string PriorSexOffenses { get; set; }

        [JsonProperty("Prior violent felonies")]
        public string PriorViolentFelonies { get; set; }

        [JsonProperty("Race")]
        public string Race { get; set; }

        [JsonProperty("Zipcode")]
        public string Zipcode { get; set; }
    }
}