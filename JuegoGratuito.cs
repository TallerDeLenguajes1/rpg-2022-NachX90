using System.Text.Json.Serialization;

public class JuegoGratuito
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; }

    [JsonPropertyName("game_url")]
    public string GameUrl { get; set; }

    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [JsonPropertyName("platform")]
    public string Platform { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("developer")]
    public string Developer { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("freetogame_profile_url")]
    public string FreetogameProfileUrl { get; set; }

    public void MostrarJuego()
    {
        Console.WriteLine($"\t\t\t\tNombre:\t\t{Title}");
        Console.WriteLine($"\t\t\t\tGénero:\t\t{Genre}");
        Console.WriteLine($"\t\t\t\tPlataforma:\t{Platform}");
        Console.WriteLine($"\t\t\t\tDesarrollador:\t{Developer}");
    }
}
