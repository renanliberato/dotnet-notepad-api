using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_notepad_api.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}