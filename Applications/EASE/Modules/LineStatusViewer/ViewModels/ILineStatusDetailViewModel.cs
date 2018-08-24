using System.Threading.Tasks;

namespace LineStatusViewer.ViewModels
{
    public interface ILineStatusDetailViewModel
    {
        Task LoadAsync(string buildNo);
    }
}