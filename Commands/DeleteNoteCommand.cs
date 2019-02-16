using MediatR;

namespace dotnet_notepad_api.Commands 
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeleteNoteCommand(
            int Id
        )
        {
            this.Id = Id;
        }
    }
}