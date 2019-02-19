using System.Threading;
using System.Threading.Tasks;
using WebAPI.Commands;
using WebAPI.Models;
using MediatR;

namespace WebAPI.CommandHandlers
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