namespace dotnet_notepad_api.Commands 
{
    public class DeleteNoteCommand
    {
        public int Id { get; private set; }

        public DeleteNoteCommand(
            int Id
        )
        {
            this.Id = Id;
        }
    }
}