using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Text.RegularExpressions;
using System;
using Tapdaq;

#if UNITY_IOS
using TDEditor.iOS.Xcode;
#endif

public class TapdaqBuildPostprocessor : MonoBehaviour
{
	private const string FrameworkPath = "Frameworks/Plugins/iOS/Tapdaq/";

	private const string BuildPathKey = "IOSBuildProjectPath";

	[MenuItem ("Tapdaq/Run iOS Build Postprocess", false, 2222)]
	private static void RunIOSPostProcessManually()
	{
		var path = EditorPrefs.GetString (BuildPathKey, null);
		OnPostprocessBuild (BuildTarget.iOS, path);
	}

	[MenuItem ("Tapdaq/Run iOS Build Postprocess", true)]
	static bool validateRunPostBuilder()
	{
		var path = EditorPrefs.GetString (BuildPathKey, null);
		if( path == null || !Directory.Exists( path ) )
			return false;

		var projectFile = Path.Combine( path, "Unity-iPhone.xcodeproj/project.pbxproj" );
		if( !File.Exists( projectFile ) )
			return false;

		return true;
	}

	[PostProcessBuild(101)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
		EditorPrefs.SetString (BuildPathKey, pathToBuiltProject);
		#if UNITY_IOS
        if (buildTarget == BuildTarget.iOS)
        {
            var path = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            if (!File.Exists(path))
            {
                Debug.LogError(string.Format("pbxproj '{0}' does not exists", path));
                return;
            }

            var proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(path));
            var target = proj.TargetGuidByName("Unity-iPhone");

			SetBuildProperties(proj, target);

			AddLibraries(proj, target);

			SetPListProperties(pathToBuiltProject);

            File.WriteAllText(path, proj.WriteToString());
        }
#endif
	}

	#if UNITY_IOS

	private static void SetBuildProperties(PBXProject proj, string target) {
		proj.SetBuildProperty(target, "ENABLE_BITCODE", "YES");
		proj.SetBuildProperty(target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
		proj.SetBuildProperty(target, "IPHONEOS_DEPLOYMENT_TARGET", GetIOSDeploymentTarget(proj));
	}

	private static void AddLibraries(PBXProject proj, string target) {
		foreach(var name in Enum.GetNames(typeof(TapdaqAdapter))) {
			proj.EmbedFramework(target, FrameworkPath + "Adapters/" + name +".framework");
		}

		if (AssetDatabase.FindAssets ("TapjoyAdapter.framework").Length > 0) {
			if (!proj.ContainsFileByProjectPath ("TapjoyResources.bundle")) {
				var fullPath = Application.dataPath
				              + "/Plugins/iOS/Tapdaq/Adapters/TapjoyAdapter.framework/TapjoyResources.bundle";
				proj.AddFileToBuild (target, proj.AddFile (fullPath, "TapjoyResources.bundle", PBXSourceTree.Source));
			}
		}

		if (!proj.ContainsFileByProjectPath ("Frameworks/libz.1.dylib")) {
			proj.AddFileToBuild (target, proj.AddFile ("usr/lib/libz.1.dylib", "Frameworks/libz.1.dylib", PBXSourceTree.Sdk));
		}
	}

	private static void SetPListProperties(string pathToBuiltProject) {
		
		var plistPath = pathToBuiltProject + "/Info.plist";
		var plist = new PlistDocument();

		plist.ReadFromString(File.ReadAllText(plistPath));
		var rootDict = plist.root;

		if(AssetDatabase.FindAssets("AdColonyAdapter.framework").Length > 0) {
			rootDict.SetString("NSMotionUsageDescription", "Interactive ad controls");
			rootDict.SetString("NSPhotoLibraryUsageDescription", "Taking selfies");
			rootDict.SetString("NSCalendarsUsageDescription", "Adding events");
		}

		var appTransportSecurity = rootDict["NSAppTransportSecurity"].AsDict();

		if(appTransportSecurity == null)
			appTransportSecurity = rootDict.CreateDict("NSAppTransportSecurity");
		
		appTransportSecurity.SetBoolean ("NSAllowsArbitraryLoads", true);

		// Write to file
		File.WriteAllText(plistPath, plist.WriteToString());
	}

	private static string GetIOSDeploymentTarget(PBXProject proj) {
		var target = proj.TargetGuidByName("Unity-iPhone");
		var deploymentTargets = proj.GetBuildProperties (target, "IPHONEOS_DEPLOYMENT_TARGET");

		var deploymentTarget = "0";
		if (deploymentTargets.Count > 0) {
			deploymentTarget = deploymentTargets [0];
		}

		if (string.IsNullOrEmpty (deploymentTarget))
			deploymentTarget = "0";

		Regex rgx = new Regex("[^0-9.]");
		var numberOnly = rgx.Replace(deploymentTarget, "");

		var version = Tapdaq.TDExtensionMethods.ParseFloat (numberOnly, 0);

		if (version >= 8.0f)
			return deploymentTarget;

		Debug.LogWarning ("TapdaqBuildPostprocessor changes iOS build target version from " + deploymentTarget + " to = 8.0");

		return "8.0";
	}
	#endif
}
