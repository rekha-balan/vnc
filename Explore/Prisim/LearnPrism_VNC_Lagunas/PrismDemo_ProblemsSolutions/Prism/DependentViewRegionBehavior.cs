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
using System.Windows;

namespace PrismDemo.Prism
{
    // RegionBehavior is a Singleton. -> A

    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";

        // A. So no need to put static before the Dictionary.  Only will ever be one.

        Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;           
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    var dependentViewList = new List<DependentViewInfo>();

                    if (_dependentViewCache.ContainsKey(newView))
                    {
                        dependentViewList = _dependentViewCache[newView];
                    }
                    else
                    {
                        foreach (var attr in GetCustomAttributes<DependentViewAttribute>(newView.GetType()))
                        {
                            var info = CreateDependentView(attr);

                            if (info.View is ISupportDataContext && newView is ISupportDataContext)
                            {
                                ((ISupportDataContext)info.View).DataContext = ((ISupportDataContext)newView).DataContext;
                            }

                            dependentViewList.Add(info);
                        }

                        //// Save list of DependentViews so don't have to recreate each time.

                        //if (! _dependentViewCache.ContainsKey(newView))
                        //{
                            _dependentViewCache.Add(newView, dependentViewList);
                        //}

                        // Inject the RibbonPage into the region
                    }

                    dependentViewList.ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Add(x.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        // This removes the view from the region.  This does not remove the Views from Memory, however.
                        _dependentViewCache[oldView].ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Remove(x.View));

                        // Use the lifetime attributes to do the right thing.   If don't keep alive, remove them.

                        if (! ShouldKeepAlive(oldView))
                        {
                            _dependentViewCache.Remove(oldView);
                        }
                    }
                }
            }
        }

        bool ShouldKeepAlive(object oldView)
        {
            IRegionMemberLifetime lifetime = GetItemOrContextLifetime(oldView);

            if (lifetime != null)
            {
                return lifetime.KeepAlive;
            }

            RegionMemberLifetimeAttribute lifetimeAttribute = GetItemOrContentLifetimeAttribute(oldView);

            if (lifetimeAttribute != null)
            {
                return lifetimeAttribute.KeepAlive;   
            }

            // Always keep alive by default.  This would be the case if no support for lifetime management.

            return true;
        }

        RegionMemberLifetimeAttribute GetItemOrContentLifetimeAttribute(object oldView)
        {
            var lifeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(oldView.GetType()).FirstOrDefault();

            if (lifeAttribute != null) return lifeAttribute;

            var frameworkElement = oldView as FrameworkElement;

            if (frameworkElement != null && frameworkElement.DataContext != null)
            {
                var dataContext = frameworkElement.DataContext;

                var contextLifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(dataContext.GetType()).FirstOrDefault();

                return contextLifetimeAttribute;
            }

            return null;
        }

        IRegionMemberLifetime GetItemOrContextLifetime(object oldView)
        {
            // This interface can exist on View or ViewModel so check both.

            var regionLifetime = oldView as IRegionMemberLifetime;

            if (regionLifetime != null) return regionLifetime;

            var frameworkElement = oldView as FrameworkElement;

            if (frameworkElement != null) return frameworkElement.DataContext as IRegionMemberLifetime;

            return null;
        }

        DependentViewInfo CreateDependentView(DependentViewAttribute attr)
        {
            var info = new DependentViewInfo();

            info.TargetRegionName = attr.TargetRegionName;

            if (attr.Type != null)
            {
                info.View = Activator.CreateInstance(attr.Type);
            }

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    internal class DependentViewInfo
    {
        public object View { get; set; }

        public string TargetRegionName { get; set; }
    }
}
