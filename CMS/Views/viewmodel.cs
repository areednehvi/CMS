using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Views
{
    // ObservableObject is a custom base class that implements INotifyPropertyChanged
    internal class MainWindowVM : NotifyPropertyChanged
    {
        private ObservableCollection<Item> mItems;
        private HashSet<Item> mCheckedItems;

        public IEnumerable<Item> Items { get { return mItems; } }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _text;

        public MainWindowVM()
        {
            mItems = new ObservableCollection<Item>();
            mCheckedItems = new HashSet<Item>();
            mItems.CollectionChanged += Items_CollectionChanged;

            // Adding test data
            for (int i = 0; i < 10; ++i)
            {
                mItems.Add(new Item(string.Format("Item {0}", i.ToString("00"))));
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Item item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                    mCheckedItems.Remove(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Item item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                    if (item.IsChecked) mCheckedItems.Add(item);
                }
            }
            UpdateText();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                Item item = (Item)sender;
                if (item.IsChecked)
                {
                    mCheckedItems.Add(item);
                }
                else
                {
                    mCheckedItems.Remove(item);
                }
                UpdateText();
            }
        }

        private void UpdateText()
        {
            switch (mCheckedItems.Count)
            {
                case 0:
                    Text = "<none>";
                    break;
                case 1:
                    Text = mCheckedItems.First().Name;
                    break;
                default:
                    Text = "<multiple>";
                    break;
            }
        }
    }

    // Test item class
    // Test item class
    internal class Item : NotifyPropertyChanged
    {
        public string Name { get; private set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set {
                _isChecked =value;
                OnPropertyChanged("IsChecked");
          
            }
        }
        private bool _isChecked;

        public Item(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
