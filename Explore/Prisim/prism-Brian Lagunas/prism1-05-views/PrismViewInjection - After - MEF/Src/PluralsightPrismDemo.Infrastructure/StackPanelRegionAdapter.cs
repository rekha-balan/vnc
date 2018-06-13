using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel.Composition;

namespace PluralsightPrismDemo.Infrastructure
{
    [Export]
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        [ImportingConstructor]
        public StackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
            
        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    {
                        foreach (FrameworkElement element in e.NewItems)
                        {
                            regionTarget.Children.Add(element);
                        }
                    }

                    //handle remove
                };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
