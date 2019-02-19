using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class NotepadContext : DbContext
    {
        public NotepadContext(DbContextOptions<NotepadContext> options)
            : base(options)
        { }

        public DbSet<Note> Notes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}