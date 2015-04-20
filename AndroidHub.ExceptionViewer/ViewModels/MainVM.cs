using System;

namespace AndroidHub.ExceptionViewer.ViewModels {

    using AndroidHub.ExceptionViewer.Views;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Controls;

    internal class MainVM: INotifyPropertyChanged {

        private List<ListViewItem> m_listViewItems;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<ListViewItem> Items {
            get {
                return m_listViewItems;
            }
            private set {
                this.m_listViewItems = value;
                OnPropertyChanged("Items");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        public MainVM() {
            Refresh();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Refresh() {
            var m_items = new List<ListViewItem>();
            foreach (string m_json in Directory.GetFiles(
                string.Format(@"{0}\.beatsleigher\AndroidHub\user_accessible\exception\logs", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "*.json|*.log")) {
                    m_items.Add(new ExceptionListViewItem(new ExceptionVM(JsonConvert.DeserializeObject<Exception>(m_json))));
            }
            Items = m_items;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="m_propertyName">Name of the m_property.</param>
        private void OnPropertyChanged(string m_propertyName) {
            foreach (PropertyChangedEventHandler m_handler in PropertyChanged.GetInvocationList())
                m_handler(this, new PropertyChangedEventArgs(m_propertyName));
        }
    }
}
