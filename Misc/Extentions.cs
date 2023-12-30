
using System.Collections.ObjectModel;

namespace TheJobOrganizationApp
{
    public static class Extensions
    {
        public static IEnumerable<T> ForEach<T>(this ICollection<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action.Invoke(item);
            }
            return collection;
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection) 
        {
            var newObs = new ObservableCollection<T>();
            if (collection != null & collection.Count() > 0) { 
            foreach (var item in collection)
            {
                newObs.Add(item);
            }
            }
            return newObs;
        }
    }
}
