using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using System.Net.Http;
using System.Net.Http.Headers;

using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;

namespace App.Pages.Profile
{
    public class CreateProfileModel : PageModel
    {

        public async Task<IActionResult> OnGetAsync()
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
                    HttpResponseMessage response = await client.PostAsJsonAsync("/profile/create/{email}", job);

                    if (response.IsSuccessStatusCode)
                    {
                        // Get the URI of the created resource.
                        Uri? returnUrl = response.Headers.Location;
                        Console.WriteLine($"{await response.Content.ReadAsStringAsync()}");
                        Console.WriteLine($@"{job}");
                    }
                }
            }
            //return Redirect("/");
            return Page();
        }
    }
}
