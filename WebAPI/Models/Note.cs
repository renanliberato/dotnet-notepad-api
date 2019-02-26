using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class Note
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public User User { get; private set; }

        public static Note createNote(
            string Title,
            string Description,
            User User
        )
        {
            var note = new Note();
            note.Title = Title;
            note.Description = Description;
            note.User = User;

            return note;
        }

        public virtual bool IsOwnedBy(string UserId)
        {
            return User.Id == UserId;
        }

        public virtual void changeTitle(string newTitle)
        {
            Title = newTitle;
        }

        public virtual void changeDescription(string newDescription)
        {
            Description = newDescription;
        }
    }
}