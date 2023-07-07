using FetchMovie;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class Program
{
    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    static readonly HttpClient client = new HttpClient();



    static async Task Main()
    {

        using HttpResponseMessage response = await client.GetAsync("https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query=Oldboy&year=2003");

        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        

        // write this into a class called GetAysncMovie();
        Result movie = new Result();

        RootObject movies = JsonConvert.DeserializeObject<RootObject>(responseBody);
        Console.WriteLine(movies.results);
        foreach (var property in movies.results)
        {
            Console.WriteLine(property.title); 
            movie.title = property.title;
            movie.overview = property.overview;
            movie.id = property.id;
            movie.genre_ids = property.genre_ids;
            movie.release_date = property.release_date;
            movie.vote_average = property.vote_average;
        }
    }
}


