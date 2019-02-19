using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class NotepadContext : DbContext
    {
        public NotepadContext(DbContextOptions<NotepadContext> options)
            : base(options)
        { }

        public virtual DbSet<Note> Notes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual void MarkAsModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}