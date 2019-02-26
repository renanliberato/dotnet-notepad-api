using MediatR;

namespace WebAPI.Commands 
{
    public class UpdateNote : IRequest<bool>
    {
        public int Id { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public string UserId { get; set; }

        public UpdateNote(
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