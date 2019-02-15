using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_notepad_api.Models
{
    public class Note
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public static Note createNote(
            string Title,
            string Description
        )
        {
            var note = new Note();
            note.Title = Title;
            note.Description = Description;

            return note;
        }

        public void changeTitle(string newTitle)
        {
            Title = newTitle;
        }

        public void changeDescription(string newDescription)
        {
            Description = newDescription;
        }
    }
}