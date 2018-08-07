﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Googlekeep.Model;
using Googlekeep.Models;

namespace Googlekeep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly GooglekeepContext _context;
       

        public NotesController(GooglekeepContext context)
        {
            _context = context;
        }



        // GET: api/Notes
        [HttpGet]
        public  IEnumerable<Note> GetNote()
        {
            return  _context.Note.Include(x => x.ListofLabels).Include(x => x.ListofChecks);
            
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetNoteByID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var note = await _context.Note.FindAsync(id);
            var note = await _context.Note.Include(y => y.ListofLabels).Include(z => z.ListofChecks).SingleOrDefaultAsync(x => x.NoteID == id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }


        [HttpGet("{title}")]
        public async Task<IActionResult> GetNoteByTitle(string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var note = await _context.Note.FindAsync(id);
            var note = await _context.Note.Include(y => y.ListofLabels).Include(z => z.ListofChecks).SingleOrDefaultAsync(x => x.title == title);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/ToDoes/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteID)
            {
                return BadRequest();
            }
            //await _context.Note.Include(x => x.ListofLabels).Include(x => x.ListofChecks).SingleOrDefaultAsync(n => n.NoteID == id);
            _context.Note.Update(note);
            //_context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetNote", new { id = note.NoteID}, note);
            return Ok(note);
        }




        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.NoteID }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Note.Include(y => y.ListofLabels).Include(z => z.ListofChecks).SingleOrDefaultAsync(x => x.NoteID == id);
            if (note == null)
            {
                return NotFound();
            }

            
                _context.Note.Remove(note);
          
            
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("delete/{title}")]
        public async Task<IActionResult> DeleteByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var note =  _context.Note.Include(y => y.ListofLabels).Include(z => z.ListofChecks).Where( x=> x.title == title);
            if (note == null)
            {
                return NotFound();
            }

            foreach (var item in note)
            {
                _context.Note.Remove(item);

            }

            await _context.SaveChangesAsync();

            return Ok(note);
        }

        [HttpGet("Pinnedstatus/{Pinnedstatus}")]
        public List<Note> Getpinned(bool Pinnedstatus)
        {
            var PinnedNotes =  _context.Note.Include(y => y.ListofLabels).Include(z => z.ListofChecks).Where(_notes => _notes.Ispinnned == Pinnedstatus).ToList();
            //if (PinnedNotes == null)
            //{
            //    return NotFound();
            //}

            return PinnedNotes;
        }



        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.NoteID == id);
        }
    }
}