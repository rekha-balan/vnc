using LineStatusViewer.Models;

namespace LineStatusViewer.Events
{
    public class AfterLineStatusSavedEventArgs
    {
        //public string BuildNo { get; set; }
        public BuildItem BuildItem { get; set; }
    }
}
