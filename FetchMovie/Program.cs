using FetchMovie;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Movie
{
    public string MovieTitle { get; set; }
    public int ReleaseDate { get; set; }
}
internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("What movie are you looking for: ");
        string MovieTitle = Console.ReadLine();
        Task<Movie> movieTask = RequestMovieAsync(MovieTitle);
        Movie movie = await movieTask;
        Console.WriteLine("Found movie");
    }



    private static async Task<Movie> RequestMovieAsync(string MovieTitle)
    {
        Console.WriteLine("Searching for movie");
        // use api to lookup movie
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query=Oldboy&year=2003");
        response.EnsureSuccessStatusCode();
        string requestBody = await response.Content.ReadAsStringAsync();
        var movietitle = JsonSerializer.Deserialize<Movie>(requestBody.ToString()).Results.title;
        // display movie details
        Console.WriteLine($"test{movietitle}");
        return new Movie();
    }
}


