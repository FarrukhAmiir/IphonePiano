using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace TDEditor {

	public class TDPathHelper {

		public static void CreatePathFolders(string folderPath) {
			var pathArray = folderPath.Split(new [] { "/" }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 2; i <= pathArray.Length; i++) {
				var folderName = string.Join ("/", pathArray, 0, i);
				if (!Directory.Exists (folderName)) {
					AssetDatabase.CreateFolder (string.Join ("/", pathArray, 0, i - 1), pathArray [i-1]);
				}
			}
		}
	}
}
