﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Events;
using dotnet_notepad_api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_notepad_api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        public async Task<ActionResult<Note>> Post([FromBody] CreateNote command)
        {
            var note = await _mediator.Send(command);

            await _mediator.Publish(new NoteCreated(
                note.Id,
                command.Title,
                command.Description
            ));

            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        // Put api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNote command)
        {
            var result = await _mediator.Send(command);

            if (result == false) {
                return NotFound();
            }

            await _mediator.Publish(new NoteUpdated(
                id,
                command.Title,
                command.Description
            ));

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteNote(id);

            var succeeded = await _mediator.Send(command);

            if (!succeeded) {
                return NotFound();
            }

            await _mediator.Publish(new NoteDeleted(
                id
            ));

            return NoContent();
        }
    }
}
