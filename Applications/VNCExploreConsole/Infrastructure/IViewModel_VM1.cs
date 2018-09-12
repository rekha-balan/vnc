namespace Infrastructure
{
    public interface IViewModel_VM1
    {
        // Normally in MVVM a ViewModel 
        // does not know about the view
        // Here we are using an Interface 
        // so there is still separation
        IView_VM1 View { get; set; }
    }
}
