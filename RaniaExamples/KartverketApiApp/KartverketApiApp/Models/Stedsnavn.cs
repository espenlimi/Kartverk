using System.Text.Json.Serialization;
namespace KartverketApiApp.Models
{
    public class StedsnavnResponse
    {
        public Metadata Metadata { get; set; }
        public List<Navn> Navn { get; set; } = new List<Navn>();
    }

    public class Metadata
    {
        public int Side { get; set; }
        public string SokeStreng { get; set; }
        public int TotaltAntallTreff { get; set; }
        public int TreffPerSide { get; set; }
        public int Utkoordsys { get; set; }
        public int ViserFra { get; set; }
        public int ViserTil { get; set; }
    }

    public class Navn
    {
        [JsonPropertyName("fylker")]
        public List<Fylke> Fylker { get; set; } = new List<Fylke>();

        [JsonPropertyName("kommuner")]
        public List<Kommune> Kommuner { get; set; } = new List<Kommune>();

        [JsonPropertyName("navneobjekttype")]
        public string? Navneobjekttype { get; set; }

        [JsonPropertyName("navnestatus")]
        public string? Navnestatus { get; set; }

        [JsonPropertyName("representasjonspunkt")]
        public string? Representasjonspunkt { get; set; }

        [JsonPropertyName("skrivemåte")]
        public string? Skrivemåte { get; set; }

        [JsonPropertyName("skrivemåtestatus")]
        public string? Skrivemåtestatus { get; set; }

        [JsonPropertyName("språk")]
        public string? Språk { get; set; }

        [JsonPropertyName("stedsnummer")]
        public int? Stedsnummer { get; set; }

        [JsonPropertyName("stedstatus")]
        public string? Stedstatus { get; set; }
}

    public class Fylke
    {
        public string? Fylkesnavn { get; set; }
        public string? Fylkesnummer { get; set; }
    }

    public class Kommune
    {
        public string Kommunenavn { get; set; }
        public string Kommunenummer { get; set; }
    }


}
