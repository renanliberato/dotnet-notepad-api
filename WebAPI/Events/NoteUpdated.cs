using MediatR;

namespace dotnet_notepad_api.Events
{
    public class NoteUpdated : INotification
    {
        public int Id { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public NoteUpdated(
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