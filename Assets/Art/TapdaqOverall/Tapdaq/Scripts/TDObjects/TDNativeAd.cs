using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Tapdaq {
	[Serializable]
	public class TDNativeAd {

		public string applicationId; // ios
		public string targetingId; // ios
		public string subscriptionId; // ios 
		// (optional)

		//--native specific members
		public string appName; // android  ios
		public string description; // android  ios
		public string buttonText; // ios
		public string developerName; // android  ios
		public string ageRating; // android  ios
		public string appSize; // android  ios
		public string averageReview; // android  ios
		public string totalReviews; // android  ios
		public string category; // android  ios
		public string appVersion; // android  ios
		public string price; // android  ios
		public string currency; // android  ios
		public string imageUrl; // android  ios
		public string title; // android  ios
		public string iconUrl; // android  ios
		public string iconPath;
		public string uniqueId;

		public string creativeIdentifier;

		public Texture2D texture { get; private set; }

		public void LoadTexture(Action<TDNativeAd> onLoadCallback) {
			SpriteLoader.Instance.LoadTextureAsync (imageUrl, texture => {
				this.texture = texture;
				if(onLoadCallback != null)
					onLoadCallback(this);
			});
		}

		public override string ToString () {
			return JsonConvert.SerializeObject(this);
		}

		public static TDNativeAd CreateNativeAd (string jsonString) {
			return JsonConvert.DeserializeObject<TDNativeAd> (jsonString);
		}
	}
}