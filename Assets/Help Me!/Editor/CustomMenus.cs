using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;

#if UNITY_5_3
using UnityEditor.SceneManagement;
#endif

public class CustomMenus :MonoBehaviour{


	public static string Path1="";
	public static string Path2="";
	public static string Path3="";
	public static string PrefabSavePath="Assets/Prefabs/NPC/";
	public const string textFilePath = "Assets/Help Me!/Resources/CustomMenusData.txt";
	public const string CustomMenuDatabasePath = "Assets/Help Me!/Resources/CustomMenuDatabase.asset";

	void OnEnable()
	{
		
	}

	[MenuItem("HelpMe!/Plugins _F1")]
	private static void PluginScene()
	{		
		if(!EditorApplication.isPlaying)
		{
			#if UNITY_5_3
			bool value = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			if(value)
			{
				EditorSceneManager.OpenScene(CustomMenus.Path1);
			}
			#else
			bool value = EditorApplication.SaveCurrentSceneIfUserWantsTo();
			if(value)
			{
				//EditorApplication.OpenScene(CustomMenus.plugin_Scene_Path);
			}
			#endif
		}
	}


	[MenuItem("HelpMe!/Gameplay _F2")]
	private static void Gameplay()
	{		
		if(!EditorApplication.isPlaying)
		{
			#if UNITY_5_3
			bool value = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			if(value)
			{
				EditorSceneManager.OpenScene(CustomMenus.Path2);
			}
			#else

			bool value = EditorApplication.SaveCurrentSceneIfUserWantsTo();
			if(value)
			{
				//EditorApplication.OpenScene(CustomMenus.gameplay_Scene_Path);
			}	
			#endif
		}
	}


	[MenuItem("HelpMe!/MenuScene _F3")]
	private static void MenuScene()
	{
		if(!EditorApplication.isPlaying)
		{
			#if UNITY_5_3
			bool value=EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			if(value)
			{
				EditorSceneManager.OpenScene(CustomMenus.Path3);
			}
			#else
			bool value = EditorApplication.SaveCurrentSceneIfUserWantsTo();
			if(value)
			{
				//EditorApplication.OpenScene(CustomMenus.menuScene_Scene_Path);
			}
			#endif
		}
	}

	[MenuItem ("HelpMe!/Clear Console _F4")] 
	static void ClearConsole () 
	{
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null,null);
	}


	[MenuItem("HelpMe!/PlayStop _F5")]
	private static void PlayStopButton()
	{	
		if(!EditorApplication.isPlaying)
		{	
			#if UNITY_5_3
			bool value = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			if(value)
			{
				EditorSceneManager.OpenScene(CustomMenus.Path1/*"Assets/Scenes/IntroVideo.unity"*/);
				EditorApplication.ExecuteMenuItem("Edit/Play");
			}
			#else
			bool value=EditorApplication.SaveCurrentSceneIfUserWantsTo();
			if(value)
			{
				//EditorApplication.OpenScene(CustomMenus.plugin_Scene_Path);
				EditorApplication.ExecuteMenuItem("Edit/Play");
			}
			#endif
		}		

	}//End of PlayStopButton


	[MenuItem("HelpMe!/Pause _F6")]
	private static void PauseButton()
	{		
		if(EditorApplication.isPlaying)
		{
			EditorApplication.ExecuteMenuItem("Edit/Pause");
		}
	}//End of PauseButton


	[MenuItem("HelpMe!/Clear UserPrefs _F7")]
	private static void ClearUserPrefs()
	{

		if(!EditorApplication.isPlaying)
		{
			if(File.Exists(Application.persistentDataPath + "/" + "PlayerPrefs.txt"))
			{
				File.Delete(Application.persistentDataPath + "/" + "PlayerPrefs.txt");
			}

			if(File.Exists(Application.persistentDataPath + "/" + "RewardPrefs.txt"))
			{
				File.Delete(Application.persistentDataPath + "/" + "RewardPrefs.txt");
			}
		}
	}

	[MenuItem("Window/Behaviour Designer &1")]
	private static void BehaviourDesignerShorcut()
	{
		EditorApplication.ExecuteMenuItem("Tools/Behavior Designer/Editor");
	}

	[MenuItem("HelpMe!/Save Prefabs _F8")]

	private static void savePrfab()
	{
		EditorApplication.ExecuteMenuItem("GameObject/Apply Changes To Prefab");
		/*GameObject []obj =Selection.gameObjects;
		foreach (GameObject o in obj) {
			string name = o.name;

			Object prefab = EditorUtility.CreateEmptyPrefab (PrefabSavePath + name + ".prefab");
			EditorUtility.ReplacePrefab (o, prefab);
			AssetDatabase.Refresh ();*/
		//}
	}

	[MenuItem("HelpMe!/Reset Tranform _F9")]
	private static void Reset()
	{
		GameObject[] selectedObjects = Selection.gameObjects;

		foreach(GameObject obj in selectedObjects)
		{
			obj.transform.position = Vector3.zero;
			obj.transform.rotation = Quaternion.identity;
		}
	}


	#region DXTTextureFinder
	[MenuItem("HelpMe!/DXT Texture Finder _F10")]
	private static void TextureFinder()
	{
		List<Texture2D> DxtTextures= new List<Texture2D>();

		foreach (string s in AssetDatabase.GetAllAssetPaths().Where(s => s.EndsWith(
			".psd", System.StringComparison.OrdinalIgnoreCase)
			|| s.EndsWith(".tiff", System.StringComparison.OrdinalIgnoreCase) 
			|| s.EndsWith(".tif", System.StringComparison.OrdinalIgnoreCase) 
			|| s.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase)
			|| s.EndsWith(".tga", System.StringComparison.OrdinalIgnoreCase)
			|| s.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase))) {
			Object  txtures = AssetDatabase.LoadAssetAtPath (s,typeof(Texture2D));

			Texture2D dxtTexture = (Texture2D)txtures;

			if (dxtTexture.format.Equals (TextureFormat.DXT1) || dxtTexture.format.Equals (TextureFormat.DXT5) || dxtTexture.format.Equals (TextureFormat.DXT1Crunched) || dxtTexture.format.Equals (TextureFormat.DXT5Crunched)) {
				Debug.Log ("Format " + dxtTexture.format + ", Name "+ dxtTexture.name,txtures);


			}
		}
	}
	#endregion

	#region FindMissingScriptsRecursively
	[MenuItem("HelpMe!/FindMissingScriptsRecursively _F11")]
	static void FindMissingScriptsRecursivelyMenuItem()
	{
		FindMissingScriptsRecursively.ShowWindow();
	}
	#endregion

	#region CustomMenuInput
	[MenuItem("HelpMe!/Custom Menu Input _F12")]
	
	private static void CustomMenuInput()
	{
		Selection.activeObject = _inputEditor;
	}

	public static CustomMenuDatabase _inputEditor1;
	public static CustomMenuDatabase _inputEditor
	{
		get
		{
		if(_inputEditor1 == null)
			{
				showDataBase();
			}
		return _inputEditor1;
		}
		private set{ _inputEditor1 = value; }
	}


	static void showDataBase()
	{
		try{
			_inputEditor1 =(CustomMenuDatabase)Resources.Load(CustomMenuDatabasePath, typeof(CustomMenuDatabase));
			if(_inputEditor1==null)
			{
				if(!Directory.Exists(Application.dataPath + "/Help Me!/Resources"))
				{
					Directory.CreateDirectory(Application.dataPath + "/Help Me!/Resources");
				}
				if(!Directory.Exists(Application.dataPath + "/Help Me!/Resources/"))
				{
					Directory.CreateDirectory(Application.dataPath + "/Help Me!/Resources/");
					Debug.Log("HelpMe!: Help Me!/Resources folder is required to store settings. it was created ");
				}

				const string path = CustomMenuDatabasePath;
				if(File.Exists(path))
				{
					AssetDatabase.DeleteAsset(path);
					AssetDatabase.Refresh();
				}
				var asset = ScriptableObject.CreateInstance<CustomMenuDatabase>();
				//Assign Values Here

				if(File.Exists(textFilePath))
				{
					int i=0;
					foreach(string s in File.ReadAllLines(textFilePath))
					{
						if(i==0){asset._inputValues.ScenePath1 = s;}
						else if(i==1){asset._inputValues.ScenePath2 = s;}
						else if(i==2){asset._inputValues.ScenePath3 = s;}
						else if(i==3){asset._inputValues.PrefabSavePath = s;}
						else{Debug.Log("Assign This:"+s);}
						i++;
					}
				}


				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.Refresh();

				AssetDatabase.SaveAssets();
				Debug.LogWarning("HelpMe!: CustomMenuDatabase file didn't exist and was created");
				Selection.activeObject = asset;
					
					//save reference
					_inputEditor1 =	asset;
			}
		}
		catch(System.Exception e)
		{
			Debug.Log("Error getting Settings in InitAPI: " + e.Message);
		}


	}
	#endregion


	static void SelectObject(Object selectedObject,bool append)
	{
		if (append)
		{
			List<Object> currentSelection=new List<Object>(Selection.objects);
			// Allow toggle selection
			if (currentSelection.Contains(selectedObject)) currentSelection.Remove(selectedObject);
			else currentSelection.Add(selectedObject);

			Selection.objects=currentSelection.ToArray();
		}
		else 
			Selection.activeObject=selectedObject;
	}



	[MenuItem ("HelpMe!/Clean Materials(Development Phase)")]
	static void CleanUnusedMaterials()
	{
		CleanMaterials.CleaningProcess();
	}


//	[MenuItem("HelpMe!/AddBoxCollider _1")]
//	private static void AddBoxCollider()
//	{		
//		if(!EditorApplication.isPlaying)
//		{
//			EditorApplication.ExecuteMenuItem("Component/Physics/Box Collider");
//		}
//	}//End of PauseButton
//
//	[MenuItem("HelpMe!/Copy _3")]
//    private static void CopyComponents()
//    {
//        allComponents = null;
//        GameObject selectedObject = Selection.activeGameObject;
//        allComponents = selectedObject.GetComponents(typeof(Component));
//    }


	
	public static Component[] allComponents;

    System.Reflection.FieldInfo[] fields;
//	[MenuItem("HelpMe!/pasteValues _4")]
//    private static void PasteComponents()
//    {
//        GameObject[] selectedObjects = Selection.gameObjects;
//
//        foreach (GameObject obj in selectedObjects)
//        {
//            foreach (Component c in allComponents)
//            {
//
//                Component copy = obj.AddComponent(c.GetType());
//                System.Reflection.FieldInfo[] fields = c.GetType().GetFields();
//                ; 
//                foreach (System.Reflection.FieldInfo field in fields)
//                {
//                    field.SetValue(copy, field.GetValue(c));
//                }
//                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
//                PropertyInfo[] pinfos = c.GetType().GetProperties(flags);
//                foreach (var pinfo in pinfos)
//                {
//                    if (pinfo.CanWrite)
//                    {
//                        try
//                        {
//                            pinfo.SetValue(copy, pinfo.GetValue(c, null), null);
//                        }
//                        catch
//                        {
//							
//                        } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
//                    }
//                }
//
//
//
//
//            }
//        }
//    }
//


}

#region FindMissingScriptsRecursively

public class FindMissingScriptsRecursively : EditorWindow 
{
	static int go_count = 0, components_count = 0, missing_count = 0;

	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(FindMissingScriptsRecursively));
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
		{
			FindInSelected();
		}
	}
	private static void FindInSelected()
	{
		GameObject[] go = Selection.gameObjects;
		go_count = 0;
		components_count = 0;
		missing_count = 0;
		foreach (GameObject g in go)
		{
			FindInGO(g);
		}
		Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
	}

	private static void FindInGO(GameObject g)
	{
		go_count++;
		Component[] components = g.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			components_count++;
			if (components[i] == null)
			{
				missing_count++;
				string s = g.name;
				Transform t = g.transform;
				while (t.parent != null) 
				{
					s = t.parent.name +"/"+s;
					t = t.parent;
				}
				Debug.Log (s + " has an empty script attached in position: " + i, g);
			}
		}
		// Now recurse through each child GO (if there are any):
		foreach (Transform childT in g.transform)
		{
			//Debug.Log("Searching " + childT.name  + " " );
			FindInGO(childT.gameObject);
		}
	}
}

#endregion

#region CleanMaterial
public class CleanMaterials {

    private const string assetFolder = "Assets";

    /// This method selects and returns all selected scenes dependencies.
    public static Object[] SelectScenesDependencies() 
	{
        return EditorUtility.CollectDependencies(Selection.objects);
    }

    /// This method selects and returns all materials inside Assets directory and subdirectories.
	public static Object[] SelectAllMaterials() 
	{
        List<Object> allMaterials = new List<Object>();
        string searchPath = string.Empty;

        /// Gets all materials inside "Assets" directory.
        DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath);
        foreach (FileInfo fi in dirInfo.GetFiles("*.mat"))
        {
            searchPath = fi.DirectoryName.Substring(fi.DirectoryName.IndexOf(assetFolder)) + "\\" + fi.Name;
            Debug.Log(searchPath);
            Debug.Log(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));

            allMaterials.Add(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));
            Debug.Log(fi.Name + " has been added as Material...");
        }

        /// Gets all materials inside all "Assets" subdirectories.
        foreach (string directoryName in Directory.GetDirectories(Application.dataPath,"*", SearchOption.AllDirectories))
        {
            dirInfo = new DirectoryInfo(directoryName);

            switch(dirInfo.Name.ToLower())
            {
                case ".svn":
                case "prop-base":
                case "props":
                case "text-base":
                case "tmp":
                    break;

                default:
                    Debug.Log("We are inside " + dirInfo.Name + " directory...");
                     
                    foreach (FileInfo fi in dirInfo.GetFiles("*.mat"))
                    {
                        searchPath = fi.DirectoryName.Substring(fi.DirectoryName.IndexOf(assetFolder)) + "\\" + fi.Name;
                        allMaterials.Add(AssetDatabase.LoadAssetAtPath(searchPath, typeof(Object)));
                        Debug.Log(fi.Name + " has been added as Material...");
                    }

                    break;
            };
        }   
        return allMaterials.ToArray();
    }

    /// This method removes all unused materials.
	public static void RemoveUnusedMaterials(Object[] AllMaterials, Object[] UsedMaterials) 
	{
        Debug.Log("Deleting non-used Materials...");
        for(uint i = 0; i < AllMaterials.Length; i++)
        {
            Debug.Log("Checking " + AllMaterials[i].name);
            for (uint j = 0; j < UsedMaterials.Length; j++)  
            {
                if (UsedMaterials[j].name == AllMaterials[i].name)
                    break;
                else if ((j == UsedMaterials.Length - 1) && (UsedMaterials[j].name != AllMaterials[i].name))
                {
                    Debug.Log(AllMaterials[i].name + " has been deleted...");
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(AllMaterials[i]));
                }
            }
        }
    } 

    /// The Main method.
	
    public static void CleaningProcess()
    {
        RemoveUnusedMaterials(SelectAllMaterials(), SelectScenesDependencies()); 	
    }
}
#endregion


