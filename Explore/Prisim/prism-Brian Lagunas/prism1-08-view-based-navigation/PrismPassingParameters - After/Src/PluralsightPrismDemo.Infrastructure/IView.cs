using System;

namespace PluralsightPrismDemo.Infrastructure
{
    public interface IView
    {
        IViewModel ViewModel { get; set; }
    }
}
