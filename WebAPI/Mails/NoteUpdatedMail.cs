using System.Net.Mail;

namespace WebAPI.Mails
{
    public class NoteUpdatedMail : MailMessage
    {
        public NoteUpdatedMail(
            string noteTitle,
            string noteDescription
        )
        {
            From = new MailAddress("whoever@me.com");
            To.Add("receiver@me.com");
            Body = $"A note was updated:\n Title: {noteTitle} \n Description: {noteDescription}";
            Subject = $"Note updated: {noteTitle}";
        }
    }
}