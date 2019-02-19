using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Models;
using MediatR;

namespace dotnet_notepad_api.CommandHandlers
{
    public class CreateNoteHandler : IRequestHandler<CreateNote, Note>
    {
        NotepadContext _context;

        public CreateNoteHandler(NotepadContext context)
        {
            _context = context;
        }

        public async Task<Note> Handle(CreateNote request, CancellationToken cancellationToken)
        {
            var note = Note.createNote(
                request.Title,
                request.Description,
                request.User
            );

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }
    }
}