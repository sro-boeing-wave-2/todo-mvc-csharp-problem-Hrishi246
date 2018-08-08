using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeepNotes.Models;
using System.Data;
using System.Configuration;

namespace KeepNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly KeepNotesContext _context;

        public NotesController(KeepNotesContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public IEnumerable<Notes> GetNotes()
        {
            return _context.Notes.Include(x => x.label).Include(x => x.checklist);
            //return Ok();
        }

        // GET: api/Notes/5 
        [HttpGet("ID/{id}")]
        public async Task<IActionResult> GetNotesById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.Include(y => y.label).Include(y => y.checklist).FirstOrDefaultAsync(x => x.ID == id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }


        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetNoteByTitle(string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var note = await _context.Note.FindAsync(id);
            var note = await _context.Notes.Include(y => y.label).Include(z => z.checklist).SingleOrDefaultAsync(x => x.Title == title);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }



        [HttpGet("Pinnedstatus/{Pinnedstatus}")]
        public List<Notes> Getpinned(bool Pinnedstatus)
        {
            var PinnedNotes = _context.Notes.Include(y => y.label).Include(z => z.checklist).Where(_notes => _notes.PinStat== Pinnedstatus).ToList();
            //if (PinnedNotes == null)
            //{
            //    return NotFound();
            //}

            return PinnedNotes;
        }

        [HttpGet("label/{label}")]
        public IActionResult GetNotesLabel([FromRoute] string label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var notes = _context.Notes.Include(x => x.checklist).Include(x=>x.label).Where(x => x.label.Exists(z => z.label == label)).ToList();
     
            if (notes == null) return NotFound();
            return Ok(notes);
        }

        // PUT: api/Notes/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] Notes note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.ID)
            {
                return BadRequest();
            }
            //await _context.Note.Include(x => x.ListofLabels).Include(x => x.ListofChecks).SingleOrDefaultAsync(n => n.NoteID == id);
            _context.Notes.Update(note);
            //_context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!NotesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(note);
        }



        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] Notes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotes", new { id = notes.ID }, notes);
        }

 

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.Include(y => y.label).Include(y => y.checklist).FirstOrDefaultAsync(x => x.ID == id);

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return Ok(notes);
        }

        [HttpDelete("delete/{title}")]
        public async Task<IActionResult> DeleteByTitle([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = _context.Notes.Include(y => y.label).Include(z => z.checklist).Where(x => x.Title == title);
            if (note == null)
            {
                return NotFound();
            }

            foreach (var item in note)
            {
                _context.Notes.Remove(item);

            }

            await _context.SaveChangesAsync();

            return Ok(note);
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.ID == id);
        }
    }
}