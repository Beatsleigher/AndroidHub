using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidHub.Models {
    interface IPreferences {

        /// <summary>
        /// Gets the serialized json.
        /// </summary>
        /// <value>
        /// The serialized json.
        /// </value>
        string SerializedJSON {
            get;
            set;
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        void Serialize();

        /// <summary>
        /// Saves the preferences.
        /// </summary>
        void SavePreferences();

        /// <summary>
        /// Loads the preferences.
        /// </summary>
        void LoadPreferences();

    }
}
