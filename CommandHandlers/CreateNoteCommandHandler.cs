using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Models;
using MediatR;

namespace dotnet_notepad_api.CommandHandlers
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Note>
    {
        NotepadContext _context;

        public CreateNoteCommandHandler(NotepadContext context)
        {
            _context = context;
        }

        public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = Note.createNote(
                request.Title,
                request.Description
            );

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }
    }
}