using dotnet_notepad_api.Models;
using MediatR;

namespace dotnet_notepad_api.Commands 
{
    public class CreateNote : IRequest<Note>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public User User { get; set; }

        public CreateNote(
            string Title,
            string Description
        )
        {
            this.Title = Title;
            this.Description = Description;
        }
    }
}