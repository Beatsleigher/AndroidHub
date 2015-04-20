using System;

namespace AndroidHub.Views {

    using AndroidHub.ViewModels;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash {
        public Splash() {
            var context = new ApplicationStartup();
            InitializeComponent();
            //this.loadProgress.DataContext = context;
            //this.iconHolder.DataContext = context;
            //this.loadProgressTxt.DataContext = context;
            this.DataContext = context;
            context.StartupProgress = 0;

            this.Loaded += async (s, evt) => await context.BootAndroidHubAsync();
        }
    }
}
