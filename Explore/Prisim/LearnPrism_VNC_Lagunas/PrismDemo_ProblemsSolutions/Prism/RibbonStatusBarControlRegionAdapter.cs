using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using Prism.Regions;
using System;
using System.Collections.Specialized;

namespace PrismDemo.Prism
{
    public class RibbonStatusBarControlRegionAdapter : RegionAdapterBase<RibbonStatusBarControl>
    {
        public RibbonStatusBarControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RibbonStatusBarControl regionTarget)
        {
            if (region == null) throw new ArgumentNullException("region");
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddViewToRegion(view, regionTarget);
                    }

                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveViewFromRegion(view, regionTarget);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        static void AddViewToRegion(object view, RibbonStatusBarControl statusBarControl)
        {
            var barItem = view as BarStaticItem;
            if (barItem != null)
            {
                statusBarControl.LeftItems.Add(barItem);
            }

            //var ribbonTabItem = view as RibbonTabItem;
            //if (ribbonTabItem != null)
            //    xamRibbon.Tabs.Add(ribbonTabItem);
        }

        static void RemoveViewFromRegion(object view, RibbonStatusBarControl statusBarControl)
        {
            var barItem = view as BarStaticItem;
            if (barItem != null)
            {
                statusBarControl.LeftItems.Remove(barItem);
            }
            //    var ribbonTabItem = view as RibbonTabItem;
            //    if (ribbonTabItem != null)
            //        xamRibbon.Tabs.Remove(ribbonTabItem);
        }
    }
}
