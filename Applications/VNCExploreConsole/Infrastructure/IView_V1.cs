namespace Infrastructure
{
    public interface IView_V1
    {
        // This is View 1st approach. 
        // View knows about ViewModel
        IViewModel_V1 ViewModel { get; set; }
    }
}
