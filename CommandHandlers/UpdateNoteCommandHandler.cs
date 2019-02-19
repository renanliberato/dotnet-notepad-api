using System.Threading;
using System.Threading.Tasks;
using dotnet_notepad_api.Commands;
using dotnet_notepad_api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace dotnet_notepad_api.CommandHandlers
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNote, bool>
    {
        NotepadContext _context;

        public UpdateNoteHandler(NotepadContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateNote request, CancellationToken cancellationToken)
        {
            var note = _context.Notes.Find(request.Id);

            if (note == null) {
                return false;
            }

            if (note.User.Id != request.UserId) {
                return false;
            }

            note.changeTitle(request.Title);
            note.changeDescription(request.Description);

            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}