using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appTechV1.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace appTechV1.Controllers
{
    [Route("api/Job")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobContext _context;

        public JobController(JobContext context)
        {
            _context = context;

            if (_context.JobItems.Count() == 0)
            {

                string apiUrl = "https://jobsearch.api.jobtechdev.se/search?q=hudiksvall&offset=0&limit=100";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET"; 
                request.Headers.Add("api-key", "YidceDkxXHg5Ylx4MTFgLlx4ODUjXHhlN1x4MDZceDk3XHhkY1x4MDJceGZjXHhmMVx4ODBceGNmNlx4MDdceDA1NSc"); //Use your own key.

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //om statusen är okej och inge fel uppstod kommer den fortsätta lägga in information i databasen   
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    }

                    var data = readStream.ReadToEnd();

                    var jObj = JObject.Parse(data);
                                 

                    for (int x = 0; x < 100; x++)
                    {

                        var headline = jObj["hits"][x]["headline"].ToString();
                        var city = jObj["hits"][x]["workplace_address"]["municipality"].ToString();
                        var description = jObj["hits"][x]["description"]["text"].ToString();
                        var applicationDeadline = DateTime.Parse(jObj["hits"][x]["application_deadline"].ToString());
                        applicationDeadline.ToString("yyyy-MM-dd");
                        var companyName = jObj["hits"][x]["employer"]["workplace"].ToString();
                        var workTitle = jObj["hits"][x]["occupation"]["label"].ToString();
                        var workingHours = jObj["hits"][x]["working_hours_type"]["label"].ToString();
                        var workingDuration = jObj["hits"][x]["duration"]["label"].ToString();
                        var publicationDate = DateTime.Parse(jObj["hits"][x]["publication_date"].ToString());
                        publicationDate.ToString("yyyy-MM-dd");



                        _context.JobItems.Add(new JobItem { Headline = headline, City = city, Description = description, ApplicationDeadline = applicationDeadline, CompanyName = companyName, WorkTitle = workTitle, WorkingHours = workingHours, WorkingDuration = workingDuration, PublicationDate = publicationDate });
                    }

                    _context.SaveChanges();

                    //stänger anslutning till api
                    response.Close();
                    readStream.Close();
                }
            }
        }

        // GET: api/Job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobItem>>> GetTodoItems()
        {
            return await _context.JobItems.ToListAsync();
        }
    }
}

