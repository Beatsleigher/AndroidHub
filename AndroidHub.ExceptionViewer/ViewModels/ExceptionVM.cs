using System;

namespace AndroidHub.ExceptionViewer.ViewModels {

    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Controls;

    public class ExceptionVM: INotifyPropertyChanged {

        #region Private members
        private readonly Exception m_exception;
        #endregion

        #region Public members      
        #region Properties
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception {
            get {
                return m_exception;
            }
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName {
            get {
                return string.Format("{0}{1}", "Class name: ", m_exception.GetType().FullName);
            }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message {
            get {
                return string.Format("{0}{1}", "Message: ", Exception.Message);
            }
        }
        
        /// <summary>
        /// Gets the help URL.
        /// </summary>
        /// <value>
        /// The help URL.
        /// </value>
        public string HelpUrl {
            get {
                return string.Format("{0}{1}", "Help Link: ", m_exception.HelpLink);
            }
        }
        
        /// <summary>
        /// Gets the exception method.
        /// </summary>
        /// <value>
        /// The exception method.
        /// </value>
        public string ExceptionMethod {
            get {
                return string.Format("{0}{1}", "Exception Method: ", m_exception.TargetSite.Name);
            }
        }
        
        /// <summary>
        /// Gets the h result.
        /// </summary>
        /// <value>
        /// The h result.
        /// </value>
        public string HResult {
            get {
                return string.Format("{0}{1}", "HResult: ", Exception.HResult);
            }
        }
        
        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source {
            get {
                return string.Format("{0}{1}", "Source: ", Exception.Source);
            }
        }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string[] StackTrace {
            get {
                var m_itemCollection = new List<string>();

                foreach (string m_trace in Regex.Split(m_exception.StackTrace, "\n"))
                    m_itemCollection.Add(m_trace);

                return m_itemCollection.ToArray();
            }
        }
        #endregion

        #region Events        
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionVM"/> class.
        /// </summary>
        /// <param name="m_json">The Json.</param>
        public ExceptionVM(string m_json) {
            this.m_exception = JsonConvert.DeserializeObject<Exception>(m_json);
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionVM"/> class.
        /// </summary>
        /// <param name="m_exception">The exception.</param>
        public ExceptionVM(Exception m_exception) { this.m_exception = m_exception; Init(); }
        #endregion

        #region Private members        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init() {
            PropertyInfo[] m_properties = GetType().GetProperties();
            foreach (PropertyInfo m_prop in m_properties)
                if (!m_prop.Name.Equals("Exception"))
                    OnPropertyChanged(m_prop.Name);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="m_propertyName">Name of the m_property.</param>
        private void OnPropertyChanged(string m_propertyName) {
            foreach (PropertyChangedEventHandler m_handler in PropertyChanged.GetInvocationList())
                m_handler(this, new PropertyChangedEventArgs(m_propertyName));
        }
        #endregion
    }
}
