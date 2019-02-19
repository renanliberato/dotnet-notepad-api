using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Models;
using MediatR;

namespace dotnet_notepad_api.CommandHandlers
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNote, bool>
    {
        NotepadContext _context;

        public DeleteNoteHandler(NotepadContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Handle(DeleteNote request, CancellationToken cancellationToken)
        {
            var note = _context.Notes.Find(request.Id);

            if (note == null) {
                return false;
            }

            if (note.User.Id != request.UserId) {
                return false;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}