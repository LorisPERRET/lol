using DTO_API;
using Model;
using System.Net.Http;
using System.Net.Http.Json;
using DTO_API.Mapper;

Console.WriteLine("Test client API");

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7185");

Console.WriteLine("\n__________ Get Champions __________");
var champions = await httpClient.GetFromJsonAsync<IEnumerable<ChampionDto>>("/api/champions");
Console.WriteLine("Liste des champions :");
foreach (var c in champions)
{
    Console.WriteLine("\t - " + c.Name);
}

Console.WriteLine("\n__________ Get Champion __________");
var nom = "Ahri";
var champion = await httpClient.GetFromJsonAsync<ChampionDto>($"/api/Champions/{nom}");
Console.WriteLine("Champion lu : " + champion.Name);

Console.WriteLine("\n__________ Post __________");
var championPost = new Champion("hugo", ChampionClass.Assassin).ToDto();
var resultPost = await httpClient.PostAsJsonAsync("/api/Champions", championPost);
var newChampion = await resultPost.Content.ReadFromJsonAsync<ChampionDto>();
Console.WriteLine("Le champion " + newChampion.Name + " à été créé");

Console.WriteLine("\n__________ Put __________");
Console.WriteLine("Le champion " + champion.Name + " est un " + champion.Class);
var championPut = new Champion("Ahri", ChampionClass.Marksman).ToDto();
var resultPut = await httpClient.PutAsJsonAsync($"/api/Champions/Ahri", championPut);
var newChampionPut = await resultPut.Content.ReadFromJsonAsync<ChampionDto>();
Console.WriteLine("Le champion " + champion.Name + " est maintenant un " + newChampionPut.Class);

Console.WriteLine("\n__________ Delete __________");
var resultDelete = await httpClient.DeleteAsync($"/api/Champions/{nom}");
var championDelete = await resultDelete.Content.ReadAsStringAsync();
Console.WriteLine("Le champion " + championDelete + " a été supprimé");

Console.ReadLine();