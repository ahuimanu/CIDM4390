using Microsoft.EntityFrameworkCore;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;
using System.Net.Http.Json;

// this is a quick console app to test the job scheduler. This is useful to see
// how the job scheduler works in a simple console app where the steps are all in one place.
var random = new Random();

string RandomString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Make a new Guest Profile Job");


GuestProfileJob? job = GuestProfileJobFactory.CreateGuestProfileJob(
    $"{RandomString(10)}@example.com",      // email
    DateTime.Now                            // date
);

Console.WriteLine($"Submitting job for {job}");

//make API call - https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("https://localhost:3000/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //POST Method
    HttpResponseMessage response = await client.PostAsJsonAsync($"/profile/create/{job?.Email}", job);

    if (response.IsSuccessStatusCode)
    {
        // Get the URI of the created resource.
        var profileJob = await response.Content.ReadFromJsonAsync<GuestProfileJob>();
        Console.WriteLine("Profile Job: " + profileJob);
    }
}

//make API call - https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("https://localhost:3000/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //GET Method
    var profileRetrieved = await client.GetFromJsonAsync<GuestProfile>($"/profile/get/{job?.Email}");
    Console.WriteLine("Profile Job: " + profileRetrieved);    
}

// bang operator is the null-forgiving operator
// var profile = await CaptivePortalDbContext.GetGuestProfileAsync(job?.Email!);

// Console.WriteLine($"Profile found for {profile.Email}");

// GuestProfile? tempProfile = null;

// if (tempProfile != null)
// {
//     Console.WriteLine($"Profile found for {tempProfile.Email}");
// }
// else
// {
//     Console.WriteLine("Profile not found");
// }