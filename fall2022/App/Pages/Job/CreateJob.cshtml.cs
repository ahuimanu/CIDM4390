using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using System.Net.Http;
using System.Net.Http.Headers;

using Services.WeatherDataService;
using Services.WeatherService;
using Services.WeatherReportJobService;

namespace web.Pages.Job
{
    public class CreateJobModel : PageModel
    {

        [BindProperty]
        public WeatherReportJob? Job { get; set; }
        public List<WeatherReportJob>? Jobs { get; set; }

        public string? JSONOutput { get; set; }

        public async Task OnGetAsync()
        {
            // https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                try
                {
                    Jobs = await client.GetFromJsonAsync<List<WeatherReportJob>>("job/all");
                }
                catch (Exception exp)
                {
                    Console.Error.WriteLine($"Problem: {exp.Message}");
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //do the db stuff
                //cast the int back to the enum
                Job!.JobActionType = (WeatherJobActionType)Job.JobActionType;
                //timestamp now                
                Job.JobScheduledAt = DateTime.Now;

                //make API call - https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:3000/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //POST Method
                    HttpResponseMessage response = await client.PostAsJsonAsync("job/create", Job);

                    if (response.IsSuccessStatusCode)
                    {
                        // Get the URI of the created resource.
                        Uri? returnUrl = response.Headers.Location;
                        JSONOutput += returnUrl;
                        Console.WriteLine(returnUrl);
                    }
                }

                //updating this variable for display in the rendered template
                JSONOutput += $": {JsonSerializer.Serialize(Job)}";

            }
            //return Redirect("/");
            return Page();
        }
    }
}
