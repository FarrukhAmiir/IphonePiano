using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using TDEditor;
using System.Linq;

namespace Tapdaq {
	public class TDMenuEditor
	{
		[MenuItem ("Tapdaq/Edit Settings", false, 101)]
		public static void CreateAsset () {
			var folderPath = TDPaths.TapdaqPath + "/Resources/Tapdaq";
			var fileName = "TapdaqSettings.asset";
			TDSettings asset;
			TDSettings[] assets = Resources.LoadAll<TDSettings>("Tapdaq");

			if (assets != null && assets.Length > 0) {
				asset = assets [0];
			}
			else {
				TDPathHelper.CreatePathFolders (folderPath);
				var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (folderPath + "/" + fileName);
				asset = ScriptableObject.CreateInstance<TDSettings> ();

				AssetDatabase.CreateAsset (asset, assetPathAndName);

				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();
			}
			EditorUtility.FocusProjectWindow ();
			Selection.activeObject = asset;
		}
	}
}