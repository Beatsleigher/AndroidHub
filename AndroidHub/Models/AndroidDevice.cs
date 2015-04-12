using System;

namespace AndroidHub.Models {

    using RegawMOD.Android;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public class AndroidDevice: Device, INotifyPropertyChanged {

        public AndroidDevice(string m_serial): base(m_serial) {}

        public override DeviceState State { get { return base.State; } protected set { base.State = value; OnPropertyChanged("State"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string m_propertyName) {
            PropertyChangedEventHandler m_handler = PropertyChanged;

            if (m_handler != null)
                m_handler(this, new PropertyChangedEventArgs(m_propertyName));
        }
    }
}
