using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace Tapdaq {
	public class TapdaqUninstallScript {

		[MenuItem ("Tapdaq/Uninstall", false, 11111)]
		public static void CreateAsset () {
			if (EditorUtility.DisplayDialog ("Uninstall Tapdaq Plugin", 
				"Are you sure you want to remove all Tapdaq files from your project?", 
				"Uninstall", "Cancel")) {

				Uninstall ();
			}
		}

		private static void Uninstall() {
			
			Delete ("Editor/iOS/Xcode");
			DeleteIfEmpty ("Editor/iOS");

			Delete ("Editor/Vungle/VungleResources");
			Delete ("Editor/Vungle/mod_pbxproj.pyc");
			Delete ("Editor/Vungle/mod_pbxproj.py");
			Delete ("Editor/Vungle/VunglePostBuilder.cs");
			Delete ("Editor/Vungle/VunglePostProcessor.py");

			DeleteIfEmpty ("Editor/Vungle");
			DeleteIfEmpty ("Editor");

			Delete ("iOS/Tapdaq");
			Delete ("iOS/Tapdaq.framework");

			DeleteIfEmpty ("iOS");

			Delete ("Android/Tapdaq");
			foreach (var adapter in Enum.GetNames(typeof(TapdaqAdapter))) {
				var name = TDEnumHelper.FixAndroidAdapterName (adapter).Replace ("Adapter", "");
				var path = "Android/Tapdaq" + name;
				Delete (path);
			}

			Delete ("Android/libs/Tapdaq");

			DeleteIfEmpty ("Android/libs");

			DeleteIfEmpty ("Android", "AndroidManifest.xml");

			Delete ("Tapdaq");

			AssetDatabase.Refresh ();
		}

		private static void Delete(string path) {
			path = "Assets/Plugins/" + path;
			if (File.Exists (path) || Directory.Exists (path))
				FileUtil.DeleteFileOrDirectory (path);
		}

		private static void DeleteIfEmpty(string path, params string[] ignoreFiles) {

			var fullPath = "Assets/Plugins/" + path;

			if (!Directory.Exists (fullPath))
				return;

			var allFiles = Directory.GetFiles(fullPath);
			var allDirectories = Directory.GetDirectories (fullPath);

			var fileNames = new HashSet<string> ();

			foreach (var file in allFiles) {
				if (!file.EndsWith (".meta")) {
					var name = Path.GetFileName (file);
					fileNames.Add (name);
				}
			}

			foreach (var directory in allDirectories) {
				var name = Path.GetDirectoryName (directory);
				fileNames.Add (name);
			}

			foreach (var fileName in ignoreFiles) {
				fileNames.Remove (fileName);
			}

			if (fileNames.Count < 1)
				Delete (path);
		}
	}
}
