using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_notepad_api.Models
{
    public class NotepadContext : DbContext
    {
        public NotepadContext(DbContextOptions<NotepadContext> options)
            : base(options)
        { }

        public DbSet<Note> Notes { get; set; }
    }
}