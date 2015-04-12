/*
 * Alles, was hier verändert wurde, ist die Imports (bis auf System) ins Namespace, die Vererbung und den Ctor der Basisklasse aufrufen.
 */

using System;

namespace AndroidHub.Views {

    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for PreferenceView.xaml
    /// </summary>
    public partial class PreferenceView : BaseMetroDialog {
        public PreferenceView(MetroWindow m_parent, MetroDialogSettings m_settings): base(m_parent, m_settings) {
            InitializeComponent();
        }
    }
}
