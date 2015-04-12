using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AndroidHub.Resources {
    public class ResourceLib {

        private static ResourceLib m_instance = null;

        public Assembly LibAssembly { get { return GetType().Assembly; } }

        public static ResourceLib Instance {
            get { return (m_instance == null ? (m_instance = new ResourceLib()) : m_instance); }
        }

    }
}
