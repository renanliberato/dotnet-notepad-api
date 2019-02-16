using MediatR;

namespace dotnet_notepad_api.Commands 
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public int Id { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public UpdateNoteCommand(
            int Id,
            string Title,
            string Description
        )
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
        }
    }
}