using MediatR;

namespace dotnet_notepad_api.Commands 
{
    public class DeleteNote : IRequest<bool>
    {
        public int Id { get; private set; }

        public DeleteNote(
            int Id
        )
        {
            this.Id = Id;
        }
    }
}