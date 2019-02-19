using MediatR;

namespace dotnet_notepad_api.Events
{
    public class NoteDeleted : INotification
    {
        public int Id { get; private set; }

        public NoteDeleted(
            int Id
        )
        {
            this.Id = Id;
        }
    }
}