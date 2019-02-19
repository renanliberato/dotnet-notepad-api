using System.Threading;
using System.Threading.Tasks;
using WebAPI.Commands;
using WebAPI.Models;
using MediatR;

namespace WebAPI.CommandHandlers
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

            if (!note.IsOwnedBy(request.UserId)) {
                return false;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}