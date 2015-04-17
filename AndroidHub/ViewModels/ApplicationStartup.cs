using System;

namespace AndroidHub.ViewModels {

    using AndroidHub.Models;
    using MahApps.Metro;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    internal class ApplicationStartup: INotifyPropertyChanged {

#region Instance variables
        private int m_startupProgress;
        private int m_maximum;
        private int m_minimum;
        private string m_progress;
#endregion
#region Static variables
        public static readonly string m_themeDirectory;
        public static readonly string m_themeSerializationDirectory;
        public static readonly string m_exceptionLogLocation;
#endregion

        /// <summary>
        /// Initializes the <see cref="ApplicationStartup"/> class.
        /// </summary>
        static ApplicationStartup() {
            m_themeDirectory = 
                string.Format(@"{0}\.beatsleigher\AndroidHub\user_accessible\themes", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            m_themeSerializationDirectory = 
                string.Format(@"{0}\.beatsleigher\AndroidHub\hidden\theme_serialization", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            m_exceptionLogLocation = m_exceptionLogLocation = string.Format(
                @"{0}\.beatsleigher\AndroidHub\user_accessible\exception\logs",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            if (!Directory.Exists(m_exceptionLogLocation))
                Directory.CreateDirectory(m_exceptionLogLocation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStartup"/> class.
        /// </summary>
        public ApplicationStartup() {
            StartupProgress = 50;
            Maximum = 100;
            Minimum = 0;
            ReadableProgress = "Starting";
        }

        #region Public properties
        /// <summary>
        /// Gets or sets the startup progress.
        /// </summary>
        /// <value>
        /// The startup progress.
        /// </value>
        public int StartupProgress {
            get { return m_startupProgress; }
            internal set { m_startupProgress = value; OnPropertyChanged("StartupProgress"); }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Maximum {
            get { return m_maximum; }
            internal set { m_maximum = value; OnPropertyChanged("Maximum"); }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public int Minimum {
            get { return m_minimum; }
            set { m_minimum = value; OnPropertyChanged("Minimum"); }
        }

        /// <summary>
        /// Gets the hub icon.
        /// </summary>
        /// <value>
        /// The hub icon.
        /// </value>
        public BitmapImage HubIcon {
            get {
                BitmapImage img = new BitmapImage();
                using (MemoryStream memStream = new MemoryStream()) {
                    Properties.Resources.androidhub_icon.Save(memStream, ImageFormat.Png);

                    img.BeginInit();
                    img.StreamSource = new MemoryStream(memStream.ToArray());
                    img.EndInit();
                    memStream.Close();
                }
                return img;
            }
        }

        /// <summary>
        /// Gets or sets the readable progress.
        /// </summary>
        /// <value>
        /// The readable progress.
        /// </value>
        public string ReadableProgress {
            get { return m_progress; }
            set { m_progress = string.Format("{0}... [{1}%]", value, m_startupProgress); OnPropertyChanged("ReadableProgress"); }
        }
        #endregion

        // **************************************************************************
        // To-Do: Figure commands out and trigger boot when splash is shown.
        // With each step, increase value of StartupProgress and ReadableProgress.
        // After that, regret the fact you skipped the Model part and just 
        // went for the ViewModel and View, you stupid bastard.
        // Well, nighty night :*
        // **************************************************************************

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="m_propertyName">Name of the m_property.</param>
        public virtual void OnPropertyChanged(string m_propertyName = "") {
            if (PropertyChanged != null && PropertyChanged.GetInvocationList().Length > 0) {
                foreach (PropertyChangedEventHandler handler in PropertyChanged.GetInvocationList())
                    handler(this, new PropertyChangedEventArgs(m_propertyName));
            }
        }

        /// <summary>
        /// Initializes the boot sequence for AndroidHub.
        /// Loads the preferences, styles, frequently used devices, downloads the latest executables and more.
        /// </summary>
        internal async Task<bool> BootAndroidHubAsync() {
            return await Task<bool> Task.Run<bool>(new Action(() => {
				var properties = Preferences.Instance;
				var androidController = AndroidController.Instance;
				
				properties.ApplyPreferences();
				
				androidController.OnDeviceAdded += (s, evt) => {
					/* To-Do: 
						// Show notification window on bottom-right hand side of the monitor.
						// If user is using multi-monitor setup, show notification on main monitor.
						// Use await Task.Run<int>
					*/
				};
				androidController.OnDeviceRemoved += (s, evt) => {
					// Do same as OnDeviceAdded
				};
				
				// To-Do: Create ResourceManager class, static, manage resources. Lol.
				// ResourceManager.LoadJson();
				// ResourceManager.LoadResources();
				
				mainWindow = new MainWindow();
				
				var updatesAvailable = UpdateManager.UpdatesAvailable();
				
				if (updatesAvailable) {
					// Notify user
				}
				
				mainWindow.Show();
				
            }));
        }

        public static bool ReadUserThemes() {
            List<string> m_fileList = Directory.EnumerateFiles(m_themeDirectory, "*.theme.xaml|*.accent.xaml", SearchOption.TopDirectoryOnly).ToList();
            if (m_fileList.Count != 0) {
                foreach (string m_file in m_fileList)
                    if (m_file.ToLower().EndsWith(".theme.xaml"))
                        ThemeManager.AddAppTheme(Regex.Split(m_file, ".theme", RegexOptions.IgnoreCase)[0], new Uri(m_file));
                    else if (m_file.ToLower().EndsWith(".accent.xaml"))
                        ThemeManager.AddAccent(Regex.Split(m_file, ".accent", RegexOptions.IgnoreCase)[0], new Uri(m_file));
                    else continue;
                return true;
            } else return false;
        }

        public static bool SerializeThemes(bool write = true) {
            Dictionary<string, string> m_serializations = new Dictionary<string, string>();
            try {
                #region Loop through Accents and Themes
                foreach (Accent m_accent in ThemeManager.Accents.ToList())
                    m_serializations.Add(
                                        string.Format("{0}.accent.json", m_accent.Name),
                                        JsonConvert.SerializeObject(m_accent, Formatting.Indented, new JsonSerializerSettings() {
                                            CheckAdditionalContent = true,
                                            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                                            DateParseHandling = DateParseHandling.DateTime,
                                            DateTimeZoneHandling = DateTimeZoneHandling.Local,
                                            DefaultValueHandling = DefaultValueHandling.Populate,
                                            FloatFormatHandling = FloatFormatHandling.String,
                                            FloatParseHandling = FloatParseHandling.Double,
                                            Formatting = Formatting.Indented,
                                            MetadataPropertyHandling = MetadataPropertyHandling.Default,
                                            MissingMemberHandling = MissingMemberHandling.Ignore,
                                            NullValueHandling = NullValueHandling.Include,
                                            ObjectCreationHandling = ObjectCreationHandling.Auto,
                                            PreserveReferencesHandling = PreserveReferencesHandling.All,
                                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                                            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full,
                                            TypeNameHandling = TypeNameHandling.All
                                        }));
                foreach (AppTheme m_appTheme in ThemeManager.AppThemes.ToList())
                    m_serializations.Add(
                                        string.Format("{0}.theme.json", m_appTheme.Name),
                                        JsonConvert.SerializeObject(m_appTheme, Formatting.Indented, new JsonSerializerSettings() {
                                            CheckAdditionalContent = true,
                                            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                                            DateParseHandling = DateParseHandling.DateTime,
                                            DateTimeZoneHandling = DateTimeZoneHandling.Local,
                                            DefaultValueHandling = DefaultValueHandling.Populate,
                                            FloatFormatHandling = FloatFormatHandling.String,
                                            FloatParseHandling = FloatParseHandling.Double,
                                            Formatting = Formatting.Indented,
                                            MetadataPropertyHandling = MetadataPropertyHandling.Default,
                                            MissingMemberHandling = MissingMemberHandling.Ignore,
                                            NullValueHandling = NullValueHandling.Include,
                                            ObjectCreationHandling = ObjectCreationHandling.Auto,
                                            PreserveReferencesHandling = PreserveReferencesHandling.All,
                                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                                            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full,
                                            TypeNameHandling = TypeNameHandling.All
                                        }));
                #endregion
            } catch (Newtonsoft.Json.JsonException ex) {
                return false;
            }

            if (write) {
                var m_value = "";
                foreach (string m_key in m_serializations.Keys) {
                    m_serializations.TryGetValue(m_key, out m_value);
                    File.WriteAllText(string.Format(@"{0}\{1}", m_themeSerializationDirectory, m_key), m_value);
                }
            }

            return true;
        }

    }
}
