using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
//using Infragistics.Windows.Ribbon;
using DevExpress.Xpf.Ribbon;
using PrismDemo.Core;

namespace PrismDemo.Prism
{
    public class RibbonPageCategoryRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DXRibbonPageCategoryRegionBehavior";

        public const string RibbonPageCategoryRegionName = "RibbonPageCategoryRegion";

        protected override void OnAttach()
        {
            // Only do this when injecting Views into ContentRegion

            if (Region.Name == "ContentRegion")
            {
                // Can work with Views or ActiveViews

                Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;

                //Region.Views.CollectionChanged += Views_CollectionChanged;
            }             
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var ribbonPageList = new List<RibbonPage>();

                foreach (var newView in e.NewItems)
                {
                    foreach (var attr in GetCustomAttributes<DependentViewAttribute>(newView.GetType()))
                    {
                        var ribbonPageItem = Activator.CreateInstance(attr.Type) as RibbonPage;

                        // Add in DataContext if both view and ribbonPage support it

                        if (ribbonPageItem is ISupportDataContext && newView is ISupportDataContext)
                        {
                            ((ISupportDataContext)ribbonPageItem).DataContext = ((ISupportDataContext)newView).DataContext;
                        }

                        ribbonPageList.Add(ribbonPageItem);
                    }

                    // Inject the RibbonPage into the region

                    ribbonPageList.ForEach(x => Region.RegionManager.Regions[RibbonPageCategoryRegionName].Add(x));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var views = Region.RegionManager.Regions[RibbonPageCategoryRegionName].Views.ToList();

                views.ForEach(x => Region.RegionManager.Regions[RibbonPageCategoryRegionName].Remove(x));
            }
        }

        void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            

        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
