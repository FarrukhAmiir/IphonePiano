using System;
using System.Threading;

namespace Tapdaq {
	public class TDCallbacks {

		private static TDCallbacks reference;

		public static TDCallbacks instance {
			get {
				if (TDCallbacks.reference == null) {
					TDCallbacks.reference = new TDCallbacks ();
				}
				return TDCallbacks.reference;
			}
		}

		//
		// Constructors
		//
		internal TDCallbacks () {}

		//
		// Static Events
		//
		public static event Action<TDAdEvent> AdAvailable;

		public static event Action<TDAdEvent> AdNotAvailable;

		public static event Action<TDAdEvent> AdWillDisplay;

		public static event Action<TDAdEvent> AdDidDisplay;

		public static event Action<TDAdEvent> AdClicked;

		public static event Action<TDAdEvent> AdClosed;

		public static event Action<TDAdEvent> AdError;

		public static event Action TapdaqConfigLoaded;

		public static event Action<TDVideoReward> RewardVideoValidated;

		// Obsolete events

		[Obsolete("Use events 'AdWillDisplay' and 'AdDidDisplay' instead.")]
		public static event Action<TDAdEvent> AdStarted;

		[Obsolete("Use event 'AdClosed' instead.")]
		public static event Action<TDAdEvent> AdFinished;

		//
		// Methods
		//

		private static void Invoke<T>(Action<T> action, T value) {
			if (action != null) {
				action (value);
			}
		}

		private static void Invoke(Action action) {
			if (action != null) {
				action ();
			}
		}


		public void OnAdAvailable (TDAdEvent adEvent) {
			Invoke (AdAvailable, adEvent);
		}

		public void OnAdClicked (TDAdEvent adEvent) {
			Invoke (AdClicked, adEvent);
		}

		public void OnAdError (TDAdEvent adEvent) {
			Invoke (AdError, adEvent);
		}

		public void OnAdClosed (TDAdEvent adEvent) {
			Invoke (AdClosed, adEvent);
		}

		public void OnAdNotAvailable (TDAdEvent adEvent) {
			Invoke (AdNotAvailable, adEvent);
		}

		public void OnAdDidDisplay (TDAdEvent adEvent) {
			Invoke (AdDidDisplay, adEvent);
		}

		public void OnAdWillDisplay (TDAdEvent adEvent) {
			Invoke (AdWillDisplay, adEvent);
		}

		public void OnTapdaqConfigLoaded() {
			Invoke (TapdaqConfigLoaded);
		}

		public void OnRewardedVideoValidated(TDVideoReward reward) {
			Invoke (RewardVideoValidated, reward);
		}
	}
}
