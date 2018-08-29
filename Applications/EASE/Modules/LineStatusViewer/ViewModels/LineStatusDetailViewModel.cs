using AMLLinesEDMXCodeFirst;
using LineStatusViewer.Data;
using LineStatusViewer.Events;
using LineStatusViewer.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LineStatusViewer.ViewModels
{
    public class LineStatusDetailViewModel : BindableBase, ILineStatusDetailViewModel
    {
        public ILineStatusDataService _lineStatusDataService { get; set; }
        private IEventAggregator _eventAggregator;

        public ICommand SaveCommand { get; }

        public LineStatusDetailViewModel(ILineStatusDataService lineStatusDataService,
                                        IEventAggregator eventAggregator)
        {
            try
            {
                Message = "LineStatusDetailViewModel";
                
                _lineStatusDataService = lineStatusDataService;
                _eventAggregator = eventAggregator;
                _eventAggregator.GetEvent<OpenLineStatusDetailViewEvent>()
                    .Subscribe(OnOpenLineStatusDetailView);

                SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
        }

        private bool OnSaveCanExecute()
        {
            // Check if LineStatus is new
            return true;
        }

        private async void OnSaveExecute()
        {
            await _lineStatusDataService.SaveAsync(OldLineStatus, LineStatus);

            _eventAggregator.GetEvent<AfterLineStatusSavedEvent>().Publish(
                new AfterLineStatusSavedEventArgs
                {
                    //BuildNo = LineStatus.BuildNo,
                    BuildItem = new BuildItem
                    {
                        LineId = LineStatus.LineID,
                        StationNO = LineStatus.StationNO,
                        BuildNo = LineStatus.BuildNo
                    }
                });
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        async void OnOpenLineStatusDetailView(BuildItem buildItem)
        {
            await LoadAsync(buildItem);
        }

        //async void OnOpenLineStatusDetailView(string buildNo)
        //{
        //    await LoadAsync(buildNo);
        //}

        public async Task LoadAsync(BuildItem buildItem)
        {
            LineStatus = await _lineStatusDataService.GetByBuildItemAsync(buildItem);
            // Save the item in case we need to update it.
            //OldLineStatus = LineStatus;
            OldLineStatus = new BuildItem
            {
                LineId = LineStatus.LineID,
                StationNO = LineStatus.StationNO,
                BuildNo = LineStatus.BuildNo
            };
        }

        //public async Task LoadAsync(string buildNo)
        //{
        //    LineStatus = await _lineStatusDataService.GetByBuildNoAsync(buildNo);
        //    // Save the item in case we need to update it.
        //    //OldLineStatus = LineStatus;
        //    OldLineStatus = new BuildItem
        //    {
        //        LineId = LineStatus.LineID,
        //        StationNO = LineStatus.StationNO,
        //        BuildNo = LineStatus.BuildNo
        //    };
        //}

        private AML_LineStatus _lineStatus;

        public AML_LineStatus LineStatus
        {
            get { return _lineStatus; }
            private set
            {
                _lineStatus = value;
                OnPropertyChanged();
            }
        }

        BuildItem _oldLineStatus;
        public BuildItem OldLineStatus
        {
            get
            {
                return _oldLineStatus;
            }
            set
            {
                if (_oldLineStatus == value)
                    return;
                _oldLineStatus = value;
            }
        }

        //AML_LineStatus _oldLineStatus;
        //public AML_LineStatus OldLineStatus
        //{
        //    get
        //    {
        //        return _oldLineStatus;
        //    }
        //    set
        //    {
        //        if (_oldLineStatus == value)
        //            return;
        //        _oldLineStatus = value;
        //    }
        //}

    }
}
