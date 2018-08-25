using AMLLinesEDMXCodeFirst;
using LineStatusViewer.Data;
using LineStatusViewer.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineStatusViewer.ViewModels
{
    public class LineStatusDetailViewModel : BindableBase, ILineStatusDetailViewModel
    {
        public ILineStatusDataService _lineStatusDataService { get; set; }
        private IEventAggregator _eventAggregator;

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
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        async void OnOpenLineStatusDetailView(string buildNo)
        {
            await LoadAsync(buildNo);
        }

        public async Task LoadAsync(string buildNo)
        {
            LineStatus = await _lineStatusDataService.GetByBuildNoAsync(buildNo);
        }

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
    }
}
