using System.Net.Mail;

namespace WebAPI.Mails
{
    public class NoteDeletedMail : MailMessage
    {
        public NoteDeletedMail(
            int noteId
        )
        {
            From = new MailAddress("whoever@me.com");
            To.Add("receiver@me.com");
            Body = $"A note was deleted:\n Id: {noteId}";
            Subject = $"Note deleted: {noteId}";
        }
    }
}