using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

namespace Tapdaq {

	[Serializable]
	public class TDMoreAppsConfig {

		public string placementTagPrefix;

		public int minAdsToDisplay;
		public int maxAdsToDisplay;

		public string headerText;
		public string installedAppButtonText;

		public string headerTextColor;
		public string headerColor;
		public string headerCloseButtonColor;
		public string backgroundColor;

		public string appNameColor;
		public string appButtonColor;
		public string appButtonTextColor;
		public string installedAppButtonColor;
		public string installedAppButtonTextColor;

		private static int ColorComponent(float val) {
			return (int)(Mathf.Clamp01 (val) * 255);
		}

		private static String HexConverter(Color c)
		{
			return "#" + ColorComponent(c.a).ToString("X2") 
				+ ColorComponent(c.r).ToString("X2") 
				+ ColorComponent(c.g).ToString("X2") 
				+ ColorComponent(c.b).ToString("X2");
		}

		public void SetHeaderTextColor(Color color) {
			headerTextColor = HexConverter (color);
		}

		public void SetHeaderColor(Color color) {
			headerColor = HexConverter (color);
		}

		public void SetHeaderCloseButtonColor(Color color) {
			headerCloseButtonColor = HexConverter (color);
		}

		public void SetBackgroundColor(Color color) {
			backgroundColor = HexConverter (color);
		}

		public void SetAppNameColor(Color color) {
			appNameColor = HexConverter (color);
		}

		public void SetAppButtonColor(Color color) {
			appButtonColor = HexConverter (color);
		}

		public void SetAppButtonTextColor(Color color) {
			appButtonTextColor = HexConverter (color);
		}

		public void SetInstalledAppButtonColor(Color color) {
			installedAppButtonColor = HexConverter (color);
		}

		public void SetInstalledAppButtonTextColor(Color color) {
			installedAppButtonTextColor = HexConverter (color);
		}

		public override string ToString ()
		{
			var settings = new JsonSerializerSettings ();
			settings.DefaultValueHandling = DefaultValueHandling.Ignore;
			return JsonConvert.SerializeObject(this, settings);
		}
	}
}
