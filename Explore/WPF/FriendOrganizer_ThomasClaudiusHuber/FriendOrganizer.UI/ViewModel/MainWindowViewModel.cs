using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IFriendDataService _friendDataService { get; set; }

        Friend _selectedFriend;

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                // Notify databindings of change

                // Traditional approach is to pass string name of field - error prone!
                //OnPropertyChanged("SelectedFriend");

                // C#6 added nameof keyword
                //OnPropertyChanged(nameof(SelectedFriend));

                // Latest is to relay on compiler to pass in name of caller to invocation
                OnPropertyChanged();
            }
        }
        
        // ObservableCollection notifies databinding when collection changes
        // because it implements INotifyPropertyChanged

        public ObservableCollection<Friend> Friends { get; set; }


        public MainWindowViewModel(IFriendDataService friendDataService)
        {
            _friendDataService = friendDataService;

            Friends = new ObservableCollection<Friend>();
        }


        public void Load()
        {
            var friends = _friendDataService.GetAll();
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
