using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Common.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void PropertyChanged<T>(this ObservableCollection<T> observableCollection, Action<T, PropertyChangedEventArgs> callBackAction)
            where T : INotifyPropertyChanged
        {
            observableCollection.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems == null) return;
                foreach (T item in args.NewItems)
                {
                    item.PropertyChanged += (obj, eventArgs) =>
                    {
                        callBackAction((T)obj, eventArgs);
                    };
                }
            };
        }
    }
}
