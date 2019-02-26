using MediatR;

namespace WebAPI.Events
{
    public class NoteCreated : INotification
    {
        public int Id { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public NoteCreated(
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