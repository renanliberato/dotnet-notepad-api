using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_notepad_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_notepad_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private NotepadContext context;

        public NotesController(NotepadContext mContext)
        {
            context = mContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll()
        {
            return context.Notes.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var item = context.Notes.Find(id);

            if (item == null) {
                return NotFound();
            }

            return item;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note note)
        {
            context.Notes.Add(note);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Note note)
        {
            if (id != note.Id) {
                return BadRequest();
            }

            context.Entry(note).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            context.Notes.Remove(note);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
