using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
         // ObservableCollection notifies databinding when collection changes
        // because it implements INotifyPropertyChanged

        public ObservableCollection<Friend> Friends { get; set; }

        public IFriendDataService _friendDataService { get; set; }

        public MainWindowViewModel(IFriendDataService friendDataService)
        {
            _friendDataService = friendDataService;

            Friends = new ObservableCollection<Friend>();
        }       

        #region This goes away with the new NavigationViewModel


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

                // Latest is to rely on compiler to pass in name of caller to invocation
                OnPropertyChanged();
            }
        }

        #endregion

        #region Move to Navigation/Detail Views 

        //public MainWindowViewModel(INavigationViewModel navigationViewModel, 
        //                            IFriendDetailViewModel friendDetailViewModel)
        //{
        //    NavigationViewModel = navigationViewModel;
        //    FriendDetailViewModel = friendDetailViewModel;
        //}

        //public INavigationViewModel NavigationViewModel { get; }
        //public IFriendDetailViewModel FriendDetailViewModel { get; }

        #endregion

        #region Move to Async loading

        public void Load()
        {
            var friends = _friendDataService.GetAll();
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        public async Task LoadAsync()
        {
            var friends = await _friendDataService.GetAllAsync();
            Friends.Clear();

            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        #endregion

        //public async Task LoadAsync()
        //{
        //    await NavigationViewModel.LoadAsync();
        //}
    }
}
