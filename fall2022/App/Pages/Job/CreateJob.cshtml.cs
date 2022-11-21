using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using Services.DataService;
using Services.WeatherService;
using Services.WeatherReportJobService;

namespace web.Pages.Job
{
    public class CreateJobModel : PageModel
    {

        [BindProperty]
        public WeatherReportJob? Job {get; set;}



        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid){
                //do the db stuff
                //System.Text.Json;
            }
            return Page();
        }
    }
}
