using System.Net.Mail;

namespace WebAPI.Mails
{
    public class NoteCreatedMail : MailMessage
    {
        public NoteCreatedMail(
            string noteTitle,
            string noteDescription
        )
        {
            From = new MailAddress("whoever@me.com");
            To.Add("receiver@me.com");
            Body = $"A new note was created:\n Title: {noteTitle} \n Description: {noteDescription}";
            Subject = $"New note created: {noteTitle}";
        }
    }
}