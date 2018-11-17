#if UNITY_IPHONE
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Diagnostics;

//this script gets called after the build process.
public class MopubPostprocessScript : MonoBehaviour {
	
	//you may need to adjust this number "100" to adjust the priority of script 
	//if there other post process scripts are also included in proejct
	[PostProcessBuild(100)]
	
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
	{
		UnityEngine.Debug.Log("----Custome Script---Executing post process build phase."); 		
		Process myCustomProcess = new Process();		
		myCustomProcess.StartInfo.FileName = "python";
        myCustomProcess.StartInfo.Arguments = 
			string.Format("Assets/Editor/post_process_mopub.py \"{0}\"", pathToBuildProject);
        myCustomProcess.StartInfo.UseShellExecute = false;
        myCustomProcess.StartInfo.RedirectStandardOutput = false;
		myCustomProcess.Start(); 
		myCustomProcess.WaitForExit();
		UnityEngine.Debug.Log("----Custome Script--- Finished executing post process build phase.");  
	}	
}
#endif