using FluentAssertions;
using Googlekeep;
using Googlekeep.Model;
using Googlekeep.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace keep.XunitTests
{
    public class NotesControllerIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public NotesControllerIntegrationTests()
        {
            
           var host = new TestServer(new WebHostBuilder().UseEnvironment("Testing")
               .UseStartup<Startup>());
           // var _context = host.Host.Services.GetService(typeof(GooglekeepContext)) as GooglekeepContext;
            _client = host.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task TestGetRequestAsync()
        {
            var Response = await _client.GetAsync("/api/Notes");
            var ResponseBody = await Response.Content.ReadAsStringAsync();
            //Console.WriteLine("Body"+ResponseBody.);
            Response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestPost()
        {
            var notes =
                new Note()
                {
                    NoteID = 1,
                    title = "My First Note",
                    plain_text = "This is my plaintext",
                    Ispinnned = true,
                    ListofChecks = new List<checklist>()
                    {
                        new checklist()
                        {
                            chklist="checklist data 1",
                           
                        }
                    },
                    ListofLabels = new List<label>()
                    {
                        new label()
                        {
                            labelstring ="labeldata 1"
                        }
                    }
                };
            var json = JsonConvert.SerializeObject(notes);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var Response = await _client.PostAsync("/api/Notes", stringContent);
            var ResponseGet = await _client.GetAsync("/api/Notes");
            //Console.WriteLine(await ResponseGet.Content.ReadAsStringAsync());
            ResponseGet.EnsureSuccessStatusCode();
        }
        

       

        //[Fact]
        //public async Task IntegrationTestPutNote()
        //{
        //    // Arrange

        //    var note = 


        //    var content = JsonConvert.SerializeObject(note);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PutAsync("/api/Notes", stringContent);

        //    // Assert
        //    // response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var notes = JsonConvert.DeserializeObject<Note>(responseString);
        //    Console.WriteLine("------------------------------------------");

        //    Assert.Equal("First", note[0].title);
        //}

    }

}