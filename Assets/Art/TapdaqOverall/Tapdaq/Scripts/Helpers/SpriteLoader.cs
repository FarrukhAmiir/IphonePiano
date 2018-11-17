using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tapdaq {
	public class SpriteLoader : MonoBehaviour {

		private static SpriteLoader instance;

		public static SpriteLoader Instance {
			get {
				if (instance == null) {
					SpriteLoader[] loaders = FindObjectsOfType<SpriteLoader> ();

					if (loaders.Length > 0) {
						instance = loaders [0]; 
					} else {
						instance = new GameObject ("SpriteLoader").AddComponent<SpriteLoader> ();
					};
				}
				return instance;
			}
		}

		public void LoadTextureAsync(string url, Action<Texture2D> action) {
			StartCoroutine (LoadTexture(url, action));
		}

		IEnumerator LoadTexture(string url, Action<Texture2D> action) {
			WWW www = new WWW(url);
			yield return www;
			if (action != null)
				action (www.texture);
		}
	}
}
