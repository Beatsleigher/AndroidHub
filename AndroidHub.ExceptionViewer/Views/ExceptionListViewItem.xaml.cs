using System;

namespace AndroidHub.ExceptionViewer.Views {

    using AndroidHub.ExceptionViewer.ViewModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for ExceptionListViewItem.xaml
    /// </summary>
    public partial class ExceptionListViewItem : ListViewItem {

        public ExceptionListViewItem(ExceptionVM m_exceptionViewModel): this() {
            this.DataContext = m_exceptionViewModel;
        }

        private ExceptionListViewItem() {
            InitializeComponent();
        }
    }
}
