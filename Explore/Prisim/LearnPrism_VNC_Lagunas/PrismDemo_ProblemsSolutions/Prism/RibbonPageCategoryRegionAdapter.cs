using DevExpress.Xpf.Ribbon;
using Prism.Regions;
using System;
using System.Collections.Specialized;

namespace PrismDemo.Prism
{
    public class RibbonPageCategoryRegionAdapter : RegionAdapterBase<RibbonPageCategory>
    {
        public RibbonPageCategoryRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, RibbonPageCategory regionTarget)
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
            // Ribbon can only have one active page.

            return new SingleActiveRegion();
        }

        static void AddViewToRegion(object view, RibbonPageCategory ribbonPageCategory)
        {
            var ribbonPage = view as RibbonPage;

            if (ribbonPage != null)
            {
                ribbonPageCategory.Pages.Add(ribbonPage);
            }
        }

        static void RemoveViewFromRegion(object view, RibbonPageCategory ribbonPageCategory)
        {
            var ribbonPage = view as RibbonPage;

            if (ribbonPage != null)
            {
                ribbonPageCategory.Pages.Remove(ribbonPage);
            }
        }
    }
}
