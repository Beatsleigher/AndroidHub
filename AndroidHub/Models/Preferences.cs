using System;

namespace AndroidHub.Models {

    using MahApps.Metro;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// AndroidHub's preference class.
    /// This class manages all IO and containing of all the preferences used in AndroidHub.
    /// This class contains a singleton property, as to make sure that only one instance of this class is created.
    /// All properties/preferences/objects are serialized and saved to/in the JSON format.
    /// If backups are created, they are saved in an encrypted .zip file.
    /// </summary>
    internal class Preferences: IPreferences {

        #region Static members
        /// <summary>
        /// Contains the instance of this class.
        /// </summary>
        private static Preferences m_instance;

        /// <summary>
        /// Gets the instance of this class.
        /// </summary>
        /// <value>
        /// The instanceof this class.
        /// </value>
        public static Preferences Instance {
            get {
                return (m_instance == null) ? (m_instance = new Preferences()) : m_instance;
            }
        }
        #endregion
         
        #region Instance members

        #region Variables
        #region Non-Preference Variables
        private string m_exceptionLogLocation; // Hidden
        private string m_preferenceBackupSaveLocation; // Hidden
        private string m_preferenceFile; // Hidden
        private string m_preferenceSaveLocation; // Hidden
        private string m_themeBackupLocation;
        private string m_themeStoreLocation;
        #endregion
        #region Preference Variables
        private Tuple<AppTheme, Accent> m_appTheme = ThemeManager.DetectAppStyle();
        private bool m_backupPreferences = true;
        #endregion
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the exception log location.
        /// </summary>
        /// <value>
        /// The exception log location.
        /// </value>
        public string ExceptionLogLocation {
            get {
                return m_exceptionLogLocation;
            }
        }

        /// <summary>
        /// Gets or sets the preference backup save location.
        /// </summary>
        /// <value>
        /// The preference backup save location.
        /// </value>
        public string PreferenceBackupSaveLocation {
            get {
                return m_preferenceBackupSaveLocation;
            }
            set {
                m_preferenceBackupSaveLocation = value;
                Serialize();
                ApplyPreferences();
            }
        }

        /// <summary>
        /// Gets the preference file.
        /// </summary>
        /// <value>
        /// The preference file.
        /// </value>
        public string PreferenceFile {
            get {
                return m_preferenceFile;
            }
            private set {
                m_preferenceFile = value;
                Serialize();
                ApplyPreferences();
            }
        }

        /// <summary>
        /// Gets or sets the preference save location.
        /// </summary>
        /// <value>
        /// The preference save location.
        /// </value>
        public string PreferenceSaveLocation {
            get {
                return m_preferenceSaveLocation;
            }
            private set {
                m_preferenceSaveLocation = value;
                Serialize();
                ApplyPreferences();
            }
        }

        /// <summary>
        /// Gets or sets the theme backup location.
        /// </summary>
        /// <value>
        /// The theme backup location.
        /// </value>
        public string ThemeBackupLocation {
            get {
                return m_themeBackupLocation;
            }
            set {
                m_themeBackupLocation = value;
                Serialize();
                ApplyPreferences();
            }
        }

        /// <summary>
        /// Gets or sets the theme store location.
        /// </summary>
        /// <value>
        /// The theme store location.
        /// </value>
        public string ThemeStoreLocation {
            get {
                return m_themeStoreLocation;
            }
            private set {
                m_themeStoreLocation = value;
                Serialize();
                ApplyPreferences();
            }
        }

        #region Actual Preferences        
        /// <summary>
        /// Gets or sets the application theme.
        /// </summary>
        /// <value>
        /// The application theme.
        /// </value>
        public Tuple<AppTheme, Accent> ApplicationTheme {
            get {
                return m_appTheme;
            }
            set {
                m_appTheme = value;
                Serialize();
                ApplyPreferences();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [backup preferences].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [backup preferences]; otherwise, <c>false</c>.
        /// </value>
        public bool BackupPreferences {
            get {
                return m_backupPreferences;
            }
            set {
                m_backupPreferences = value;
                Serialize();
                ApplyPreferences();
            }
        }


        #endregion
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets the serialized json.
        /// </summary>
        /// <value>
        /// The serialized json.
        /// </value>
        internal string SerializedJSON {
            get;
            private set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="Preferences"/> class from being created.
        /// </summary>
        private Preferences() {
            #region Variable setup
            m_preferenceBackupSaveLocation = string.Format(
                                                            @"{0}\.beatsleigher\AndroidHub\backup\hidden\preferences",
                                                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                          );
            m_preferenceSaveLocation = string.Format(
                                                      @"{0}\.beatsleigher\AndroidHub\hidden\preferences",
                                                      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                    );
            m_preferenceFile = string.Format(@"{0}\{1}.json", PreferenceSaveLocation, "this_is_not_the_file_youre_looking_for");
            m_themeBackupLocation = string.Format(
                                                    @"{0}\.beatsleigher\AndroidHub\backup\themes",
                                                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                );
            m_themeStoreLocation = string.Format(
                                                    @"{0}\.beatsleigher\AndroidHub\user_accessible\themes",
                                                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                );
            m_exceptionLogLocation = string.Format(
                @"{0}\.beatsleigher\AndroidHub\user_accessible\exception\logs", 
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            #endregion
        }
        #endregion

        #region Internal members
        /// <summary>
        /// Loads the preferences.
        /// </summary>
        internal void LoadPreferences() {
            if (!Directory.Exists(string.Format(@"{0}\.beatsleigher\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))) {
                Serialize();
                SavePreferences();
            }
            try {
                m_instance = JsonConvert.DeserializeObject<Preferences>(
                    m_preferenceFile,
                    new JsonSerializerSettings() {
                        CheckAdditionalContent = true,
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateParseHandling = DateParseHandling.DateTime,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local,
                        DefaultValueHandling = DefaultValueHandling.Populate,
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
                    }
                );
            } catch (JsonException ex) {
                var m_dateTime = DateTime.Now;
                var m_logFilename = string.Format(
                    @"{0}\exception-{1}-{2}-{3}-{4}.log",
                    m_exceptionLogLocation,
                    m_dateTime.Year, m_dateTime.Date.Month, m_dateTime.Date.Day, m_dateTime.ToFileTime());
                using (StreamWriter m_writer = new StreamWriter(m_logFilename)) {
                    if (!File.Exists(m_logFilename))
                        File.Create(m_logFilename);
                    m_writer.Write(JsonConvert.SerializeObject(ex, Formatting.Indented));
                }
                MessageBox.Show(
                     "An unexpected error occurred!\nFor more details, please view: " + m_logFilename, // Yes, I know I shouldn't do this! 
                    "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }

        /// <summary>
        /// Saves the preferences.
        /// </summary>
        internal void SavePreferences() {
            #region IO checks. Pretty substandard shizwiz
            if (!Directory.Exists(string.Format(@"{0}\.beatsleigher\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))) {
                Directory.CreateDirectory(PreferenceBackupSaveLocation);
                Directory.CreateDirectory(PreferenceSaveLocation);
                Directory.CreateDirectory(ThemeBackupLocation);
                Directory.CreateDirectory(ThemeStoreLocation);
                new DirectoryInfo(
                    string.Format(@"{0}\.beatsleigher\AndroidHub\hidden", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                ).Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.NotContentIndexed;
                new DirectoryInfo(
                    string.Format(@"{0}\.beatsleigher\AndroidHub\backup\hidden", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                ).Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.NotContentIndexed;
                
                if (!File.Exists(m_preferenceFile))
                    File.Create(m_preferenceFile);
                new FileInfo(m_preferenceFile).Attributes = FileAttributes.NotContentIndexed | FileAttributes.Hidden;
            }
            #endregion

            using (StreamWriter m_fileStream = new StreamWriter(m_preferenceFile, false)) {
                m_fileStream.Write(SerializedJSON);
                m_fileStream.Close();
            }
            
        }
        #endregion

        #region Public Members        
        /// <summary>
        /// Serializes all the properties and preferences in this class and stores them in an internal string property.
        /// </summary>
        public void Serialize() {
            try {
                SerializedJSON = JsonConvert.SerializeObject(
                    Instance,
                    Formatting.Indented,
                    new JsonSerializerSettings() {
                        CheckAdditionalContent = true,
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateParseHandling = DateParseHandling.DateTime,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local,
                        DefaultValueHandling = DefaultValueHandling.Populate,
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
                    }
                );
            } catch (Exception ex) {
                try {
                    var m_dateTime = DateTime.Now;
                    var m_logFilename = string.Format(
                        @"{0}\exception-{1}-{2}-{3}-{4}.log",
                        m_exceptionLogLocation,
                        m_dateTime.Year, m_dateTime.Date.Month, m_dateTime.Date.Day, m_dateTime.ToFileTime());
                    using (StreamWriter m_writer = new StreamWriter(m_logFilename)) {
                        if (!File.Exists(m_logFilename))
                            File.Create(m_logFilename);
                        m_writer.Write(JsonConvert.SerializeObject(ex, Formatting.Indented));
                    }
                    MessageBox.Show(
                         "An unexpected error occurred!\nFor more details, please view: " + m_logFilename, // Yes, I know I shouldn't do this! 
                        "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                } finally {  }
            }
        }

        /// <summary>
        /// Applies the preferences.
        /// </summary>
        public void ApplyPreferences() {
            ThemeManager.ChangeAppStyle(App.Current, ApplicationTheme.Item2, ApplicationTheme.Item1);
        }
        #endregion

        #endregion



        string IPreferences.SerializedJSON {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        void IPreferences.SavePreferences() {
            throw new NotImplementedException();
        }

        void IPreferences.LoadPreferences() {
            throw new NotImplementedException();
        }
    }
}
