﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupService;

        public IEventAggregator _eventAggregator { get; set; }

        public NavigationViewModel(IFriendLookupDataService friendLookupService,
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
        }

        // Switch from LookupItem to NavigationItemViewModel
        //public ObservableCollection<LookupItem> Friends { get; }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        //public NavigationViewModel(IFriendLookupDataService friendLookupService,
        //    IEventAggregator eventAggregator)
        //{
        //    _friendLookupService = friendLookupService;
        //    _eventAggregator = eventAggregator;
        //    Friends = new ObservableCollection<LookupItem>();
        //    _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
        //}


        //LookupItem _selectedFriend;

        //public LookupItem SelectedFriend
        //{
        //    get { return _selectedFriend; }
        //    set
        //    {
        //        _selectedFriend = value;
        //        OnPropertyChanged();

        //        if (_selectedFriend != null)
        //        {
        //            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
        //                .Publish(_selectedFriend.Id);
        //        }
        //    }
        //}   

        NavigationItemViewModel _selectedFriend;

        public NavigationItemViewModel SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();

                if (_selectedFriend != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                        .Publish(_selectedFriend.Id);
                }
            }
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();

            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
                //Friends.Add(item);
            }
        }

        void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            var  lookupItem = Friends.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }
    }
}
