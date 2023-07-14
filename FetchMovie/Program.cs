using FetchMovie;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class Program
{

    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    public static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        using HttpResponseMessage request = await client.GetAsync("https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query=Oldboy&year=2003");

        request.EnsureSuccessStatusCode();
        string responseBody = await request.Content.ReadAsStringAsync();


        // write this into a class called GetMovieAysnc();
        Movie movie = new Movie();

        RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(responseBody);
        Console.WriteLine(movies.results);
        foreach (var property in movies.results)
        {
            movie.title = property.title;
            movie.overview = property.overview;
            movie.id = property.id;
            movie.genre_ids = property.genre_ids;
            movie.release_date = property.release_date;
            movie.vote_average = property.vote_average;
        }

    }
    // use http method and return response string
    static async Task<String> GetAsync(HttpClient httpClient)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("todos/3");

        response.EnsureSuccessStatusCode();

        string jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        // Expected output:
        //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
        //   {
        //     "userId": 1,
        //     "id": 3,
        //     "title": "fugiat veniam minus",
        //     "completed": false
        //   }
        return jsonResponse;
    }

    // maybe this should be in the movie class, but have the Movie instance created outside of the class
    public async Task<Movie> AddMovie(string movieRequest, Movie movie)
    {
        var responseBody = await GetAsync(client);
        
        //remove instance creation within method and pass it in.
        Movie movie = new Movie();
        RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(responseBody);
        Console.WriteLine(movies.results);
        foreach (var property in movies.results)
        {
            movie.title = property.title;
            movie.overview = property.overview;
            movie.id = property.id;
            movie.genre_ids = property.genre_ids;
            movie.release_date = property.release_date;
            movie.vote_average = property.vote_average;
        }
        Console.WriteLine(movie.id);
        return movie;
    }

}


