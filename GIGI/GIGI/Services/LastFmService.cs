using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using GIGI.Models;


public class LastFmService
{
    private readonly string _apiKey = "6719fa85837128788a644dc0301d520e";
    private readonly HttpClient _client;

    public LastFmService()
    {
        _client = new HttpClient();
    }
    public async Task<List<Artist>> SearchArtistsAsync(string query)
    {
        var url = $"http://ws.audioscrobbler.com/2.0/?method=artist.search&artist={query}&api_key={_apiKey}&format=json";
        var response = await _client.GetStringAsync(url);
        var json = JObject.Parse(response);
        var artistList = new List<Artist>();

        var results = json["results"]?["artistmatches"]?["artist"];
        if (results != null)
        {
            foreach (var item in results)
            {
                artistList.Add(new Artist
                {
                    Name = (string)item["name"],
                    Url = (string)item["url"],
                    ImageUrl = item["image"]?[2]?["#text"]?.ToString()
                });
            }

        }
        return artistList;
    }
}


