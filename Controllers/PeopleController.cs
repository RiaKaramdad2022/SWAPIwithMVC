using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SwapiMVC.Models;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SwapiMVC.Controllers
{
    // The class controller brings in MVC functionality like view() which allows us to connect our controller code to our .cshtml files.
    public class PeopleController: Controller
    {
        private readonly HttpClient _httpClient;
        public PeopleController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("swapi");
        }
        // page is an optional route parameter that will help us load the correct page based on a given value.
        public async Task<IActionResult> Index(string page)
        {
            //Over here, we're declaring a new string called route that creates the endpoint we'd like to hit. 
            // It's also adding our page query parameter to the route so as to request the appropriate page's data.
            // The code inside curly braces page ?? "1" let's us pass in the value of page, our parameter, unless page is null in which case it passes in the value "1".
            string route = $"people?page={page ?? "1"}";
            // here we are then pass this route into our _httpClient's GetAsync method and capture the response
            HttpResponseMessage response = await _httpClient.GetAsync(route);
            var responseString = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<ResultViewModel<PeopleViewModel>>(responseString);

            return View(people);
        }

        [Route("person/{id}")]
        public async Task<IActionResult> Person(string id)
        {
            var response = await _httpClient.GetAsync($"people/{id}");
            if(id is null || response.IsSuccessStatusCode == false)
                return RedirectToAction(nameof(Index));

            var responseString = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<PeopleViewModel>(responseString);
            return View(person);
    }
}
}