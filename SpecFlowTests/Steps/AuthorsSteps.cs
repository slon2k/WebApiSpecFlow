using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Steps
{
    [Binding]
    public class AuthorsSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly CustomWebApplicationFactory<Startup> factory;
        private HttpClient client { get; set; }
        private HttpResponseMessage response { get; set; }
        private HttpStatusCode statusCode;
        private object responseContent { get; set; }
        const string BaseURL = "https://localhost";
        public string requestdata { get; private set; }

        public AuthorsSteps(ScenarioContext scenarioContext, CustomWebApplicationFactory<Startup> factory)
        {
            this.scenarioContext = scenarioContext;
            this.factory = factory;
        }

        [Given(@"I am a client")]
        public void GivenIAmAClient()
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BaseURL)
            });
        }

        [When(@"I make request to ""(.*)""")]
        public async Task WhenIMakeRequestTo(string endpoint)
        {
            response = await client.GetAsync(endpoint);
            responseContent = await response.Content.ReadFromJsonAsync<object>();
            statusCode = response.StatusCode;
        }
        
        [When(@"I make create request to ""(.*)"" with data")]
        public async Task WhenIMakeCreateRequestWithData(string endpoint, Table table)
        {
            var author = table.CreateSet<AuthorCreateDto>().First();        
            requestdata = JsonSerializer.Serialize(author);
            HttpContent content = new StringContent(requestdata, Encoding.UTF8, "application/json");
            response = await client.PostAsync(endpoint, content);
            responseContent = await response.Content.ReadFromJsonAsync<object>();
            statusCode = response.StatusCode;
        }

        [When(@"I make change request to ""(.*)"" with data")]
        public async Task WhenIMakeChangeRequestToWithData(string endpoint, Table table)
        {
            var author = table.CreateSet<AuthorUpdateDto>().First();
            requestdata = JsonSerializer.Serialize(author);
            HttpContent content = new StringContent(requestdata, Encoding.UTF8, "application/json");
            response = await client.PutAsync(endpoint, content);
            statusCode = response.StatusCode;
        }


        [Then(@"the response status code should be ""(.*)""")]
        public void ThenTheResponseStatusCodeShouldBe(string code)
        {
            Assert.AreEqual(statusCodes[code], statusCode);
        }

        private Dictionary<string, HttpStatusCode> statusCodes = new Dictionary<string, HttpStatusCode>
        {
            { "Ok", HttpStatusCode.OK },
            { "Created", HttpStatusCode.Created },
            { "NotFound", HttpStatusCode.NotFound},
            { "NoContent", HttpStatusCode.NoContent},
        };

    }
}
