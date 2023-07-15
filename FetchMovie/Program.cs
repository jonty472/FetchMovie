using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace FetchMovie
{
    internal class Program
    {

        /*
         * MVP -
         * GET request movie(s)
         * Deserialize movie GET request into a Movie object
         * Add Movie to a database
         */
        public class RootObject
        {
            public List<Movie> results { get; set; }
        }
        public class Movie
        {
            public int id { get; set; }
            public string release_date { get; set; }
            public string title { get; set; }

        }

        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            /*
             * create a Task<t> that awaits a GET request for a movie
             * Pass the result of Task<t> into a method that deseralizes into an instances of an object
             * pass that object into the database
             */

            string movieRequestTask = await GetMovieAysnc(client);
            List<Movie> movie = DeserializingMovieAsync(movieRequestTask);
            
            foreach (var property in movie)
            {
                Console.WriteLine(property.title);
            }
        }
        
        public static async Task<string> GetMovieAysnc(HttpClient client)
        {
            string movieTitle = "Gladiator";
            string releaseYear = "2000";
            using HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={movieTitle}&year={releaseYear}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);
            return jsonResponse;
        }

        public static List<Movie> DeserializingMovieAsync(string jsonResponse)
        {
            RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(jsonResponse);

            List<Movie> movie = new List<Movie>();

            foreach (var property in movies.results)
            {
                movie.Add(new Movie() { id = property.id, title = property.title, release_date = property.release_date });
            }

            return movie;
        }




























/*
        static async Task<string> GetMovieAsync(HttpClient httpClient, string movieTitle, string releaseYear)
        {
            using HttpResponseMessage response = await httpClient.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=4cc1b68a07fe5ba265950e85ac96cb2c&query={movieTitle}&year={releaseYear}");

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            return jsonResponse;
        }

        // maybe this should be in the movie class, but have the Movie instance created outside of the class
        public async Task<Movie> AddMovie(Movie movie)
        {
            Console.WriteLine("Movie Title: ");
            string movieTitle = Console.ReadLine();

            Console.WriteLine("Year of release: ");
            string releaseYear = Console.ReadLine();

            var responseBody = await GetMovieAsync(client, movieTitle, releaseYear);

            //remove instance creation within method and pass it in.
            RootObject? movies = JsonConvert.DeserializeObject<RootObject?>(responseBody);
            Console.WriteLine(movies?.results);
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
*/
    }

}


