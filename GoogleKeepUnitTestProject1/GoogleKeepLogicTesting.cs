using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Googlekeep.Controllers;
using Googlekeep.Model;
using Googlekeep.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GoogleKeepUnitTestProject1
{
    public class GoogleKeepLogicTesting
    {
        private readonly NotesController _controller;
        public GoogleKeepLogicTesting()
        {

            var optionsbackend = new DbContextOptionsBuilder<GooglekeepContext>();
            optionsbackend.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notecontext = new GooglekeepContext(optionsbackend.Options);
            _controller = new NotesController(notecontext);
            CreateTestData(notecontext);
        }

        private void CreateTestData(GooglekeepContext notecontext)
        {
            List<Note> note1 = new List<Note>()
            {
                new Note
                {
                    title = "First",
                    plain_text = "First sentence",
                    Ispinnned = true,
                    ListofChecks = new List<checklist>() { new checklist { chklist = "hello" }, new checklist { chklist = "brother" } },
                     ListofLabels = new List<label>() { new label { labelstring = "number1" }, new label { labelstring = "number2" } }
                },
                new Note  
                {
                    title = "Second",
                    plain_text = "First sentence",
                    Ispinnned = false,
                    ListofChecks = new List<checklist>() { new checklist { chklist = "hello2" }, new checklist { chklist = "brother2" } },
                    ListofLabels = new List<label>() { new label { labelstring = "number3" }, new label { labelstring = "number4" } }
                }
            };

            notecontext.AddRange(note1);
            notecontext.SaveChanges();

        }

        [Fact]
        public void GetNoteTest()
        {
            var result1 = _controller.GetNote().ToList();
            //var objectresult = result1 as ObjectResult;
            //var notes = objectresult.Value as List<Note>;
            Assert.Equal(2, result1.Count);
            //Console.WriteLine("-------------------------------");
            //Console.WriteLine(result1.Count);
            //Console.WriteLine("-------------------------------");
        }

        [Fact]
        public async void GetNoteTestByID()
        {
            var result1 = _controller.GetNote().ToList();
            var result = await _controller.GetNoteByID(result1[0].NoteID);
            var ObjectResult = result as OkObjectResult;
            var notes = ObjectResult.Value as Note;

            //Console.WriteLine(result1[0].NoteID);
            //Console.WriteLine("====");
            //Console.WriteLine(notes.NoteID);
            Assert.Equal(result1[0].NoteID, notes.NoteID);



        }

        [Fact]
        public async  void GetNoteTestByTitle()
        {
            var result1 = _controller.GetNote().ToList();
            var result = await  _controller.GetNoteByTitle(result1[1].title);
            ////Assert.True(condition: result is OkObjectResult);
            var OkObjectResult = result as OkObjectResult;
            ////Assert.True(condition: result, OkObjectResult);
            var notes = OkObjectResult.Value as Note;
            Assert.Equal(result1[1].title, notes.title);

        }



        [Fact]
        public void PutTest()
        {
            var updatedNote = new Note()
            {
                title = "First",
                plain_text = "changed first sentence",
            };
            var result1 = _controller.GetNote().ToList();

            var result2 = _controller.PutToDo(result1[1].NoteID, updatedNote);
            // Assert.True(result);
            Assert.NotEqual(updatedNote.plain_text, result1[1].plain_text);


        }

        [Fact]
        public void DeleteNoteByID()
        {
            var result = _controller.DeleteNote(2).IsCompletedSuccessfully;
            Assert.True(result);

        }

        [Fact]
        public void DeleteNoteByTitle()
        {
            var result = _controller.DeleteByTitle("second").IsCompletedSuccessfully;
            Assert.True(result);
        }

        [Fact]
        public async Task TestPost()
        {

            var note = new Note
            {
                title = "Hello",
                plain_text = "How are you?",
                Ispinnned = true,
                ListofLabels = new List<label>() { new label { labelstring = "Label_1" }, new label { labelstring = "Label_2" } },
                ListofChecks = new List<checklist>() { new checklist { chklist = "1" } }

            };
            var result = await _controller.PostNote(note);

            var okobjectresult = result as CreatedAtActionResult;
            var notes = okobjectresult.Value as Note;
            Assert.Equal(note, notes);



            //    //var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            //    //var notes = okResult.Value.Should().BeAssignableTo<Note>().Subject;
            //    //var notes = controller.GetNote().ToList();
            //    //   Assert.Equal(result1.Count + 1, result2.Count);

        }

        [Fact]
        public void TestIsPinned()
        {
            List<Note> trueNotes = new List<Note>() ;
            var result1 = _controller.Getpinned(true);
            var result2 = _controller.GetNote().ToList();
            foreach (var x in result2)
            {
                if(x.Ispinnned == true)
                {
                    trueNotes.Add(x);
                }
            }


            //var okResult = result1.Should().BeOfType<CreatedAtActionResult>().Subject;
            ///var notes = okResult.Value as List<Note>;
            Assert.Equal(trueNotes, result1);


        }

     





    }


}
