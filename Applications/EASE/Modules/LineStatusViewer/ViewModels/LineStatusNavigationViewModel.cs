using LineStatusViewer.Data;
using LineStatusViewer.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LineStatusViewer.ViewModels
{
    public class LineStatusNavigationViewModel : BindableBase, ILineStatusNavigationViewModel
    {
        private ILookupBuildsService _lookupBuildsService;

        public ObservableCollection<BuildItem> Builds { get; }

        public LineStatusNavigationViewModel(ILookupBuildsService lookupBuildsService)
        {
            try
            {
                Message = "LineStatusNavigationViewModel from your Prism Module";

                _lookupBuildsService = lookupBuildsService;
                Builds = new ObservableCollection<BuildItem>();
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

        public async Task LoadAsync()
        {
            var lookup = await _lookupBuildsService.GetBuildsAsync();
            Builds.Clear();

            foreach (var item in lookup)
            {
                Builds.Add(item);
            }
        }
    }
}
