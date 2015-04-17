using System;

namespace AndroidHub.Net {
	
	using System.Net;
	
	
	public static class UpdateManager {
	
		#region Private members
		/// <summary>
		/// Private variable containing the Url pointing to the update server location
		/// </summary>
		private static readonly Url m_updateUrl;
		
		/// <summary>
		/// Private variable containing the location of the locale dir where updates are downloaded to.
		/// This location has the FileAttribute Temp, every time the system restarts, the directory 
		/// is cleared.
		/// This means less logic, therefore a slightly faster and smaller update process.
		/// </summary>
		private static readonly string m_tempUpdateDownloadDir;
		#endregion
		
		#region Public members
		#region Properties
		/// <summary>
		/// Gets the <see cref="System.Net.Url" /> pointing to the update server location.
		/// </summary>
		public static Url UpdateUrl {
			get {
				return m_updateUrl;
			}
		}
		
		/// <summary>
		/// Gets the location of the locale dir, where updates are downloaded to.
		/// This location has the FileAttribute Temp; every time the system restarts, the directory
		/// is cleared.
		/// </summary>
		public static string DownloadDir {
			get {
				return m_tempUpdateDownloadDir;
			}
		}
		#endregion
		
		public static bool UpdatesAvailable() {
			// To-Do: Add method logic.
			return true;
		}
		
		public async static Task<bool> UpdatesAvailableAsync() {
			return await Task.Run<bool>(new Action(() => {
				// To-Do: Add method logic.
			}));
		}
		
		public static List<Update> GetUpdates() {
			// To-Do: Add method logic.
			return null;
		}
		
		public async static Task<List<Update>> GetUpdatesAsync() {
			return await Task.Run<List<Update>>(new Action(() => {
				// To-Do: Add method logic.
				return null;
			}));
		}
		
		#endregion
		
	}
	
}
