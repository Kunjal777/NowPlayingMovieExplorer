using NowPlaying.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace NowPlaying.Services
{
    public class TMDBService
    {
        // 2. Private readonly field for the injected HttpClient
        private readonly HttpClient _http;

        // 3. Private readonly JsonSerializerOptions for snake case
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        // 4. Constructor injecting both HttpClient and IConfiguration
        public TMDBService(HttpClient http, IConfiguration config)
        {
            _http = http; // Assign the injected HttpClient to our private field

            // 5. Retrieve TMDB access key from configuration
            string? tmdbKey = config["TmdbAccessKey"];

            // 6. Check if key is present; if so, set BaseAddress and Authorization headers
            if (!string.IsNullOrWhiteSpace(tmdbKey))
            {
                _http.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                _http.DefaultRequestHeaders.Authorization = new("Bearer", tmdbKey);
            }
            else
            {
                // For example, if you host on Netlify with an edge function or proxy
                _http.BaseAddress = new Uri(_http.BaseAddress + "tmdb/");
            }
        }

        // 7. Public method to fetch "now playing" movies from TMDB
        public async Task<MovieListResponse> GetNowPlayingMovies()
        {
            // 7a. The relative URL for the now playing endpoint
            string url = "movie/now_playing?region=US&language=en-US";

            // 7b. Make the HTTP GET call, deserializing into a MovieListResponse
            MovieListResponse response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions)
                ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Now Playing Movies could not be loaded");

            // 7c. Loop through each movie and fix up PosterPath
            foreach (Movie movie in response.Results)
            {
                if (string.IsNullOrWhiteSpace(movie.PosterPath))
                {
                    // Fallback image
                    movie.PosterPath = "/images/poster.png";
                }
                else
                {
                    // Construct a valid URL for the poster
                    movie.PosterPath = $"http://image.tmdb.org/t/p/w500{movie.PosterPath}";
                }
            }

            // 7d. Return the fully populated list
            return response;
        }


        public async Task<MovieListResponse> GetPopularMovies()
        {
            // 7a. The relative URL for the now playing endpoint
            string url = "https://api.themoviedb.org/3/movie/popular?region=US&language=en-US";

            // 7b. Make the HTTP GET call, deserializing into a MovieListResponse
            MovieListResponse response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions)
                ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Popular Movie could not be loaded");

            // 7c. Loop through each movie and fix up PosterPath
            foreach (Movie movie in response.Results)
            {
                if (string.IsNullOrWhiteSpace(movie.PosterPath))
                {
                    // Fallback image
                    movie.PosterPath = "/images/poster.png";
                }
                else
                {
                    // Construct a valid URL for the poster
                    movie.PosterPath = $"http://image.tmdb.org/t/p/w500{movie.PosterPath}";
                }
            }

            // 7d. Return the fully populated list
            return response;
        }

        public async Task<MovieListResponse> SearchMovies(string query)
        {
            var url = $"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US";

            MovieListResponse response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions)
                ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Search results could not be loaded");

            foreach (Movie movie in response.Results)
            {
                if (string.IsNullOrWhiteSpace(movie.PosterPath))
                {
                    movie.PosterPath = "/images/poster.png";
                }
                else
                {
                    movie.PosterPath = $"http://image.tmdb.org/t/p/w500{movie.PosterPath}";
                }
            }

            return response;
        }

        public async Task<MovieDetails> GetMovieById(int movieId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}";

            MovieDetails movie = await _http.GetFromJsonAsync<MovieDetails>(url, _jsonOptions)
                                    ?? throw new HttpIOException(HttpRequestError.InvalidResponse,
                                    "Could not retrieve movie details");

            movie.PosterPath = string.IsNullOrEmpty(movie.PosterPath)
                                ? "/images/poster.png"
                                : $"http://image.tmdb.org/t/p/w500{movie.PosterPath}";

            movie.BackdropPath = string.IsNullOrEmpty(movie.BackdropPath)
                               ? "/images/backdrop.jpg"
                               : $"http://image.tmdb.org/t/p/w500{movie.BackdropPath}";

            return movie;
        }

        public async Task<Video?> GetMovieTrailer(int movieId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}/videos?language=en-US";

            var videos = await _http.GetFromJsonAsync<MovieVideosResponse>(url, _jsonOptions)
                ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Could not retreive movie videos");

            return videos.Results.FirstOrDefault(v =>
                v.Site!.Contains("YouTube", StringComparison.OrdinalIgnoreCase)
                && v.Type!.Contains("Trailer", StringComparison.OrdinalIgnoreCase));

         
        }

        public async Task<CreditsResponse> GetMovieCredits(int movieId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}/credits?language=en-US";

            var credits = await _http.GetFromJsonAsync<CreditsResponse>(url, _jsonOptions)
                          ?? throw new HttpIOException(HttpRequestError.InvalidResponse,
                              $"Could not retrieve movie credits");

            foreach (var castMember in credits.Cast)
            {
                castMember.ProfilePath = string.IsNullOrEmpty(castMember.ProfilePath)
                    ? "/images/profile.jpg"
                    : $"https://image.tmdb.org/t/p/w500{castMember.ProfilePath}";
            }

            foreach (var crewMember in credits.Crew)
            {
                crewMember.ProfilePath = string.IsNullOrEmpty(crewMember.ProfilePath)
                    ? "/images/profile.jpg"
                    : $"https://image.tmdb.org/t/p/w500{crewMember.ProfilePath}";
            }

            return credits;
        }
    }
}
