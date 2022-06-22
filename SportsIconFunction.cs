using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AFApp
{
    public static class SportsIconFunctions
    {

        public static readonly List<SportsIcon> SportsIcons = new List<SportsIcon> {
            new SportsIcon { Id=1, Name = "Muhammad Ali", NickName = "The people's Champion", DateOfBirth = DateTime.ParseExact("01/07/1942", "MM/dd/yyyy", null), Sports="Boxing" },
            new SportsIcon { Id=2, Name = "Pele", DateOfBirth = DateTime.ParseExact("10/23/1940", "MM/dd/yyyy", null), Sports="Football" },
            new SportsIcon { Id=3, Name = "Mike Tyson", NickName = "Iron Mike", DateOfBirth = DateTime.ParseExact("06/30/1966", "MM/dd/yyyy", null), Sports="Boxing" },
        };

        [FunctionName("CreateSportsIcon")]
        public static async Task<IActionResult> CreateSportsIcon(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sportsicon")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating Sports icon.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<SportsIconCreateModel>(requestBody);

            //id -> just for demo
            var sIcon = new SportsIcon() { Id = new Random().Next(4, 1000), Name = data.Name, NickName = data.NickName, DateOfBirth = data.DateOfBirth, Sports = data.Sports };
            SportsIcons.Add(sIcon);

            string responseMessage = string.IsNullOrEmpty(requestBody)
                ? "This HTTP triggered function executed successfully. Pass a details in request body for a personalized response."
                : "This HTTP triggered function executed successfully. Details : " + sIcon;

            return new OkObjectResult(sIcon);
        }

        [FunctionName("GetAllSportsIcons")]
        public static IActionResult GetAllSportsIcons(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sportsicon")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all sports Icons");
            return new OkObjectResult(SportsIcons);
        }

        [FunctionName("GetSportsIconById")]
        public static IActionResult GetSportsIconById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sportsicon/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("Getting a single sports Icon");

            var result = SportsIcons.Find(s => s.Id == id);
            return new OkObjectResult(result);
        }

        [FunctionName("UpdateSportsIcon")]

        public static async Task<IActionResult> UpdateSportsIcon(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sportsicon/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            var sIcon = SportsIcons.Find(s => s.Id == id);
            if (sIcon == null)
            {
                return new NotFoundResult();
            }
            log.LogInformation("Updating Sports icon.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedSIcon = JsonConvert.DeserializeObject<SportsIconUpdateModel>(requestBody);

            sIcon.NickName = updatedSIcon.NickName;
            sIcon.DateOfBirth = updatedSIcon.DateOfBirth;
            sIcon.Sports = updatedSIcon.Sports;

            string responseMessage = string.IsNullOrEmpty(requestBody)
                ? "This HTTP triggered function executed successfully. Pass a details in request body for a personalized response."
                : "This HTTP triggered function executed successfully. Updated : " + sIcon;

            return new OkObjectResult(sIcon);
        }

        [FunctionName("DeleteSportsIcon")]
        public static IActionResult DeleteSportsIcon(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "sportsicon/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            var sIcon = SportsIcons.Find(s => s.Id == id);
            if (sIcon == null)
            {
                return new NotFoundResult();
            }
            log.LogInformation("Deleting Sports icon.");
            SportsIcons.Remove(sIcon);
            return new OkResult();
        }
    }
}
