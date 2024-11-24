using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;

namespace App.Pages.Profile
{
    public class CreateProfileModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // request
                var email = Request.Form["email"];

                Console.WriteLine($"{email}");


                //cast the int back to the enum
                var job = new GuestProfileJob()
                {
                    //timestamp now                                    
                    Date = DateTime.Now,
                    Email = email,
                };

                //make API call - https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:3000/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //POST Method
                    HttpResponseMessage response = await client.PostAsJsonAsync($"/profile/create/{job.Email}", job);

                    if (response.IsSuccessStatusCode)
                    {
                        // Get the URI of the created resource.
                        var profileJob = await response.Content.ReadFromJsonAsync<GuestProfileJob>();
                        Console.WriteLine("Profile Job: " + profileJob);
                    }
                }
            }
            //return Redirect("/");
            return Page();
        }
    }
}