namespace dotnet_notepad_api.Commands 
{
    public class UpdateNoteCommand
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        public UpdateNoteCommand(
            string Title,
            string Description
        )
        {
            this.Title = Title;
            this.Description = Description;
        }
    }
}