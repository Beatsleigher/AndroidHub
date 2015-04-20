using System;

namespace AndroidHub.ExceptionViewer {

    using AndroidHub.ExceptionViewer.ViewModels;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            this.DataContext = new MainVM();
            InitializeComponent();
        }
    }
}
