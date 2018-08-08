using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using KeepNotes;
using KeepNotes.Models;
using KeepNotes.Controllers;

namespace Keeptest
{
    public class UnitTest
    {
        public NotesController GetController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KeepNotesContext>();
            optionsBuilder.UseInMemoryDatabase<KeepNotesContext>(Guid.NewGuid().ToString());
            var todocontext = new KeepNotesContext(optionsBuilder.Options);
            CreateTestData(optionsBuilder.Options);
            return new NotesController(todocontext);
        }

        public void CreateTestData(DbContextOptions<KeepNotesContext> options)
        {
            using (var todocontext = new KeepNotesContext(options))
            {
                var NotesToAdd = new List<Notes>
                {
                    new Notes()
                    {
                       ID=1,
                        Text = "This is my plaintext",
                        PinStat = true,
                        Title = "My First Note",
                        label = new List<Label>
                        {
                            new Label {
                                label ="Label Data 1"
                            },
                            new Label {
                                label ="Label Data 2"
                            }
                        },
                        checklist =new List<CheckList>
                        {
                            new CheckList {
                                list ="CheckList Data 1",
                              
                            },
                            new CheckList {
                                list ="CheckList Data 2",
                                
                            }
                        }
                    },
                    new Notes()
                    {
                       ID=2,
                        Text = "PlainText 2",
                        PinStat = false,
                        Title = "Title 2",
                        label = new List<Label>
                        {
                            new Label {
                                label ="Label Data 3"
                            },
                            new Label {
                                label ="Label Data 4"
                            }
                        },
                        checklist =new List<CheckList>
                        {
                            new CheckList {
                                list ="CheckList Data 3",
                             
                            },
                            new CheckList {
                                list ="CheckList Data 4",
                                
                            }
                        }
                    },
                };
                todocontext.Notes.AddRange(NotesToAdd);
                todocontext.SaveChanges();
            }
        }
        [Fact]
        public void TestGet()
        {
            var _controller = GetController();
            var result = _controller.GetNotes().ToList();
           
            //var objectresult = result as OkObjectResult;
            //var notes = objectresult.Value as List<Notes>;
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void TestGetId()
        {
            var _controller = GetController();
            var tofindID = _controller.GetNotes().ToList();
            var result = await _controller.GetNotesById(tofindID[0].ID);
            //Assert.True(condition: result is OkObjectResult);
            var OkObjectResult = result as OkObjectResult;
            //Assert.True(condition: result, OkObjectResult);
            var notes = OkObjectResult.Value as Notes;
            Assert.Equal(notes.ID, tofindID[0].ID);
        }

        [Fact]
        public async void GetNoteTestByTitle()
        {
            var _controller = GetController();
            // var result1 = _controller.GetNotes().ToList();
            var result = await _controller.GetNoteByTitle("My First Note");
            ////Assert.True(condition: result is OkObjectResult);
            var OkObjectResult = result as OkObjectResult;
            ////Assert.True(condition: result, OkObjectResult);
            var notes = OkObjectResult.Value as Notes;
            Assert.Equal("My First Note", notes.Title);

        }


        [Fact]
        public void TestIsPinned()
        {
            var _controller = GetController();
            List<Notes> trueNotes = new List<Notes>();
            var result1 = _controller.Getpinned(true);
            var result2 = _controller.GetNotes().ToList();
            foreach (var x in result2)
            {
                if (x.PinStat == true)
                {
                    trueNotes.Add(x);
                }
            }


            //var okResult = result1.Should().BeOfType<CreatedAtActionResult>().Subject;
            ///var notes = okResult.Value as List<Note>;
            Assert.Equal(trueNotes, result1);


        }



        [Fact]
        public  void TestGetLabel()
        {
            var _controller = GetController();
            var result =  _controller.GetNotesLabel("Label Data 1");
            var resultAsOkObjectResult = result as OkObjectResult;
            //Assert.True(condition: result, OkObjectResult);
            var notes = resultAsOkObjectResult.Value as List<Notes>;
            Assert.Single(notes);
        }

        [Fact]
        public async void TestPost()
        {
            var _controller = GetController();
            Notes note = new Notes
            {
                ID = 3,
                Title = "Post",
                Text = "Post sentence",
                PinStat = true,
                checklist = new List<CheckList>() { new CheckList { list = "hello2" }, new CheckList { list = "brother2" } },
                label = new List<Label>() { new Label { label = "number3" }, new Label { label = "number4" } }
            };
            var result = await _controller.PostNotes(note);
            var resultAsOkObjectResult = result as CreatedAtActionResult;
            //Assert.True(condition: result, OkObjectResult);
            var notes = resultAsOkObjectResult.Value as Notes;
            Assert.Equal(notes.Title, note.Title);
        }

        [Fact]
        public async void TestPutById()
        {
            var note = new Notes()
            {
                                    
                        ID = 2,
                        Text = "This is my plaintext",
                        PinStat = true,
                        Title = "Updated Note",
                        label = new List<Label>
                        {
                            new Label {
                                label ="Label Data 1"
                            },
                            new Label {
                                label ="Label Data 2"
                            }
                        },
                        checklist =new List<CheckList>
                        {
                            new CheckList {
                                list ="CheckList Data 1",

                            },
                            new CheckList {
                                list ="CheckList Data 2",

                            }
                        }

            };
            var _controller = GetController();
            var result = await _controller.PutToDo(2, note);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as Notes;
            Assert.Equal(2, notes.ID);
        }



        [Fact]
        public async void TestDel()
        {
            var _controller = GetController();
            var tofindID = _controller.GetNotes().ToList();
            var result = await _controller.DeleteNotes(tofindID[0].ID);
            //Console.WriteLine(note.Id);
            var resultAsOkObjectResult = result as OkObjectResult;
            //Assert.True(condition: result, OkObjectResult);
            var notes = resultAsOkObjectResult.Value as Notes;
            Assert.Equal(notes.ID, tofindID[0].ID);

        }

    }


}
