using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_notepad_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private NotepadContext context;

        public NotesController(NotepadContext mContext, IMediator mediator)
        {
            context = mContext;
            _mediator = mediator;
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
        public async Task<ActionResult<Note>> Post([FromBody] CreateNoteCommand command)
        {
            var note = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        // Put api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNoteCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == false) {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteNoteCommand(id);

            var succeeded = await _mediator.Send(command);

            if (!succeeded) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
