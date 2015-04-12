using System;

namespace TestApplication {

    using Newtonsoft.Json;
    using RegawMOD.Android;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Serializing object...");
            Preferences.Instance.SavePreferences();
            Console.WriteLine("Serialization complete...\nPress Enter to Continue...");
            Console.ReadKey();
        }

        internal class Preferences {

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
            private string m_preferenceBackupSaveLocation; // Hidden
            private string m_preferenceFile; // Hidden
            private string m_preferenceSaveLocation; // Hidden
            private string m_themeBackupLocation;
            private string m_themeStoreLocation;
            #endregion

            #region Properties
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
                }
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
                #endregion
                LoadPreferences();
            }
            #endregion

            #region Internal members
            /// <summary>
            /// Loads the preferences.
            /// </summary>
            internal void LoadPreferences() {
                if (!Directory.Exists(string.Format(@"{0}\.beatsleigher\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))) {
                    SavePreferences();
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
                    Console.WriteLine(m_preferenceFile);
                    if (!File.Exists(m_preferenceFile))
                        File.Create(m_preferenceFile);
                    new FileInfo(m_preferenceFile).Attributes = FileAttributes.NotContentIndexed | FileAttributes.Hidden;
                }
                #endregion
                string serializedData = JsonConvert.SerializeObject(Instance, new JsonSerializerSettings() {
                    CheckAdditionalContent = true,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    Formatting = Formatting.Indented,
                    MetadataPropertyHandling = MetadataPropertyHandling.Default,
                    MissingMemberHandling = MissingMemberHandling.Error,
                    NullValueHandling = NullValueHandling.Include,
                    ObjectCreationHandling = ObjectCreationHandling.Auto,
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                });
                Console.WriteLine(serializedData); 
            }
            #endregion

            #endregion

        }
    }
}
