

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.IO;



public class CustomizeInspector : Editor 
{

}

#region ItweenMagicInspector
[CustomEditor(typeof(ITweenMagic)), CanEditMultipleObjects]
public class ItweenMagicInspector : Editor 
{
	#if UNITY_EDITOR
	#region properties
	public SerializedProperty 
	MovementType,

	Movement,
	Rotation,
	Scale,

	TargetPosition3D,
	InitialPosition3D,


	TargetPosition2D,
	InitialPosition2D,

	InitialRotation,
	TargetRotation,

	TargetScale,
	InitialScale,

	EaseTypeMovement,
	EaseTypeRotation,
	EaseTypeScale,

	timeMovement,
	timeRotation,
	timeScale,

	delayMovement,
	delayRotation,
	delayScale,


	LoopTypeMovement,
	LoopTypeRotation,
	LoopTypeScale,

	MovementTweenCompletionEvent,
	RotationTweenCompletionEvent,
	ScaleTweenCompletionEvent
	;

#endregion

	void OnEnable () 
	{
		// Setup the SerializedProperties
		MovementType = serializedObject.FindProperty("ITweenSpace");

		Movement = serializedObject.FindProperty("Movement");
		Rotation = serializedObject.FindProperty("Rotation");
		Scale = serializedObject.FindProperty("Scale");

		InitialPosition3D = serializedObject.FindProperty("initialPosition3D");
		TargetPosition3D = serializedObject.FindProperty("targetPosition3D");

		InitialPosition2D = serializedObject.FindProperty("initialPosition2D");
		TargetPosition2D = serializedObject.FindProperty("targetPosition2D");

		InitialRotation = serializedObject.FindProperty("initialRotation");
		TargetRotation = serializedObject.FindProperty("targetRotation");

		InitialScale = serializedObject.FindProperty("initialScale");
		TargetScale = serializedObject.FindProperty("targetScale");


		EaseTypeMovement = serializedObject.FindProperty("EaseTypeMovement");
		EaseTypeRotation = serializedObject.FindProperty("EaseTypeRotation");
		EaseTypeScale = serializedObject.FindProperty("EaseTypeScale");

		timeMovement = serializedObject.FindProperty("timeMovement");
		timeRotation = serializedObject.FindProperty("timeRotation");
		timeScale = serializedObject.FindProperty("timeScale");


		delayMovement = serializedObject.FindProperty("delayMovement");
		delayRotation = serializedObject.FindProperty("delayRotation");
		delayScale = serializedObject.FindProperty("delayScale");

		LoopTypeMovement = serializedObject.FindProperty("LoopTypeMovement");
		LoopTypeRotation = serializedObject.FindProperty("LoopTypeRotation");
		LoopTypeScale = serializedObject.FindProperty("LoopTypeScale");

		MovementTweenCompletionEvent = serializedObject.FindProperty("movementTweenCompleteEvent");
		RotationTweenCompletionEvent = serializedObject.FindProperty("rotationTweenCompleteEvent");
		ScaleTweenCompletionEvent = serializedObject.FindProperty("scaleTweenCompleteEvent");

	}

	ITweenMagic _iTweenMagic;
	public override void OnInspectorGUI () 
	{
		serializedObject.Update ();
		EditorGUILayout.PropertyField(MovementType);


		GUILayout.Space(10);
		EditorGUILayout.PropertyField(Movement);

		_iTweenMagic = (ITweenMagic)target;
		if(Movement.boolValue && MovementType.enumValueIndex==0)
		{
			GUILayout.BeginVertical("box");	
			EditorGUILayout.PropertyField(LoopTypeMovement);
			EditorGUILayout.PropertyField(EaseTypeMovement);
			InitialPosition3DFunction();
			TargetPosition3DFunction();
			//EditorGUILayout.PropertyField(TargetPosition3D);
			EditorGUILayout.PropertyField(timeMovement);
			EditorGUILayout.PropertyField(delayMovement);
			EditorGUILayout.PropertyField(MovementTweenCompletionEvent);
			GUILayout.EndVertical();

		}
		else if(Movement.boolValue && MovementType.enumValueIndex==1)
		{
			GUILayout.BeginVertical("box");	
			EditorGUILayout.PropertyField(LoopTypeMovement);
			EditorGUILayout.PropertyField(EaseTypeMovement);
			InitialPosition2DFunction();
			TargetPosition2DFunction();
			EditorGUILayout.PropertyField(timeMovement);
			EditorGUILayout.PropertyField(delayMovement);
			EditorGUILayout.PropertyField(MovementTweenCompletionEvent);
//			GUILayout.Space(20);
			GUILayout.EndVertical();
		}

		EditorGUILayout.PropertyField(Rotation);
		if(Rotation.boolValue)
		{
			GUILayout.BeginVertical("box");	
			EditorGUILayout.PropertyField(LoopTypeRotation);
			EditorGUILayout.PropertyField(EaseTypeRotation);
			InitialRotationFunction ();
			TargetRotationFunction ();
			//EditorGUILayout.PropertyField(TargetRotation);
			EditorGUILayout.PropertyField(timeRotation);
			EditorGUILayout.PropertyField(delayRotation);
			EditorGUILayout.PropertyField(RotationTweenCompletionEvent);
			GUILayout.EndVertical();
		}
	


		EditorGUILayout.PropertyField(Scale);
	
		 if(Scale.boolValue)
		{
			GUILayout.BeginVertical("box");
			EditorGUILayout.PropertyField(LoopTypeScale);
			EditorGUILayout.PropertyField(EaseTypeScale);
			InitialScaleFunction();
			TargetScaleFunction();
			EditorGUILayout.PropertyField(timeScale);
			EditorGUILayout.PropertyField(delayScale);
			EditorGUILayout.PropertyField(ScaleTweenCompletionEvent);
			GUILayout.EndVertical();
		}





		serializedObject.ApplyModifiedProperties ();
	}

	#region MovementFunctions

	void TargetPosition2DFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(TargetPosition2D);
		if(GUILayout.Button("CurrentPosition"))
		{
			_iTweenMagic.targetPosition2D=_iTweenMagic.GetComponent<RectTransform>().anchoredPosition;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.targetPosition2D=Vector2.zero;
		}
		GUILayout.EndHorizontal();
	}
	void InitialPosition2DFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(InitialPosition2D);
		if(GUILayout.Button("CurrentPosition"))
		{
			_iTweenMagic.initialPosition2D=_iTweenMagic.GetComponent<RectTransform>().anchoredPosition;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.initialPosition2D=Vector2.zero;
		}
		GUILayout.EndHorizontal();
	}


	void TargetPosition3DFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(TargetPosition3D);
		if(GUILayout.Button("CurrentPosition"))
		{
			_iTweenMagic.targetPosition3D=_iTweenMagic.GetComponent<Transform>().localPosition;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.targetPosition3D=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	void InitialPosition3DFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(InitialPosition3D);
		if(GUILayout.Button("CurrentPosition"))
		{
			_iTweenMagic.initialPosition3D=_iTweenMagic.GetComponent<Transform>().localPosition;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.initialPosition3D=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	#endregion

	#region ScaleFunctions
	void InitialScaleFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(InitialScale);

		if(GUILayout.Button("CurrentScale"))
		{
			_iTweenMagic.initialScale=_iTweenMagic.GetComponent<Transform>().localScale;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.initialScale=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}

	void TargetScaleFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(TargetScale);

		if(GUILayout.Button("CurrentScale"))
		{
			_iTweenMagic.targetScale=_iTweenMagic.GetComponent<Transform>().localScale;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.targetScale=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	#endregion

	#region RotationFunctions

	void InitialRotationFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(InitialRotation);

		if(GUILayout.Button("CurrentRotation"))
		{
			_iTweenMagic.initialRotation=_iTweenMagic.GetComponent<Transform>().localEulerAngles;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.initialRotation=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	void TargetRotationFunction()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(TargetRotation);

		if(GUILayout.Button("CurrentRotation"))
		{
			_iTweenMagic.targetRotation=_iTweenMagic.GetComponent<Transform>().localEulerAngles;
		}
		else if(GUILayout.Button("Reset"))
		{
			_iTweenMagic.targetRotation=Vector3.zero;
		}
		GUILayout.EndHorizontal();
	}
	#endregion

	#endif
}
#endregion

#region GameGizmosInspector
[CustomEditor(typeof(GameGizmos)), CanEditMultipleObjects]
public class GameGizmosInspector:Editor
{
	#if UNITY_EDITOR
	GameGizmos _gameGizmos;
	public SerializedProperty 
		ChildName,
		StartingNumber,
		Radius,
		SizeForWiredBox
	;

	void OnEnable () 
	{
		// Setup the SerializedProperties
		ChildName = serializedObject.FindProperty("childName");
		StartingNumber = serializedObject.FindProperty("startingNumber");
		Radius = serializedObject.FindProperty("radius");
		SizeForWiredBox = serializedObject.FindProperty("size");
		_gameGizmos= (GameGizmos)target;
	}


	public override void OnInspectorGUI () 
	{
		serializedObject.Update ();


		GUILayout.Space(10);
		GUILayout.BeginVertical("box");	
		GUILayout.Label("Gizmos");
		GUILayout.Space(5);
		GUILayout.BeginHorizontal("box");	
		DrawPathChilds();
		DrawFilledSphereOnChilds();
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		GUILayout.BeginHorizontal("box");	
		DrawEmptySphereOnSelf();
		DrawFilledSphereOnSelf();
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		GUILayout.BeginHorizontal("box");
		DrawWiredBoxOnSelf();
		DrawIconsOnChilds();
		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		RadiusAndSize();
		GUILayout.EndVertical();

		GUILayout.Space(10);
		GUILayout.BeginVertical("box");	
		GUILayout.Label("Functions");
		GUILayout.Space(5);
		GUILayout.BeginHorizontal("box");	

		EditorGUILayout.PropertyField(ChildName);
		_gameGizmos.childName = ChildName.stringValue;

		EditorGUILayout.PropertyField(StartingNumber);
		_gameGizmos.startingNumber = StartingNumber.intValue;

		GUILayout.EndHorizontal();
		GUILayout.Space(5);
		GUILayout.BeginHorizontal("box");	
		if(GUILayout.Button("Rename Childs")) 
		{
			_gameGizmos.renameChildsNumerically = true;
			_gameGizmos.Update();
		}

		if(GUILayout.Button("Reverse All Childs")) 
		{
			_gameGizmos.reverseAllChilds = true;
			_gameGizmos.Update();
		}
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();

	}
	#region Radius Size
	void RadiusAndSize()
	{
		GUILayout.BeginHorizontal("box");
		EditorGUILayout.PropertyField(Radius ,new GUIContent("Radius for Sphere"));
		_gameGizmos.radius = Radius.floatValue;
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		EditorGUILayout.PropertyField(SizeForWiredBox,new GUIContent("Size For Wired Box"));
		_gameGizmos.size  = SizeForWiredBox.vector3Value;
		GUILayout.EndHorizontal();
	}
	#endregion
	#region DrawPathChilds
	bool DrawPathChildsToggle;
	string DrawPathChildsName="Draw Path on Childs";
	void DrawPathChilds()
	{
		if(GUILayout.Button(DrawPathChildsName)) 
		{
			DrawPathChildsToggle = !DrawPathChildsToggle;
			if(DrawPathChildsToggle){
				_gameGizmos.drawPathOnChilds = true;
				DrawPathChildsName="Remove Path on Childs";
			}
			else
			{
				_gameGizmos.drawPathOnChilds = false;
				DrawPathChildsName="Draw Path on Childs";
			}
			EnableDisableFunctionDrawGizmos();
		}
	}
	#endregion
	#region DrawFilledSphereOnChilds
	bool DrawFilledSphereChildsToggle;
	string DrawFilledSphereChildsName="Draw Filled Sphere on Childs";
	void DrawFilledSphereOnChilds()
	{
		if(GUILayout.Button(DrawFilledSphereChildsName)) 
		{
			DrawFilledSphereChildsToggle = !DrawFilledSphereChildsToggle;
			if(DrawFilledSphereChildsToggle){
				_gameGizmos.drawFilledSphereOnChilds = true;
				DrawFilledSphereChildsName="Remove Filled Sphere on Childs";
			}
			else
			{
				_gameGizmos.drawFilledSphereOnChilds = false;
				DrawFilledSphereChildsName="Draw Filled Sphere on Childs";
			}
			EnableDisableFunctionDrawGizmos();
		}
	}
	#endregion
	#region DrawEmptySphereOnSelf
	bool DrawEmptySphereOnSelfToggle;
	string DrawEmptySphereOnSelfName="Draw Empty Sphere on Self";
	void DrawEmptySphereOnSelf()
	{
		if(GUILayout.Button(DrawEmptySphereOnSelfName)) 
		{
			DrawEmptySphereOnSelfToggle = !DrawEmptySphereOnSelfToggle;
			if(DrawEmptySphereOnSelfToggle){
				_gameGizmos.drawEmptySphereOnSelf = true;
				DrawEmptySphereOnSelfName="Remove Empty Sphere on Self";
			}
			else
			{
				_gameGizmos.drawEmptySphereOnSelf = false;
				DrawEmptySphereOnSelfName="Draw Empty Sphere on Self";
			}
			EnableDisableFunctionDrawGizmos();
		}
	}
	#endregion
	#region DrawFilledSphereOnSelf
	bool DrawFilledSphereOnSelfToggle;
	string DrawFilledSphereOnSelfName="Draw Filled Sphere on Self";
	void DrawFilledSphereOnSelf()
	{
		if(GUILayout.Button(DrawFilledSphereOnSelfName)) 
		{
			DrawFilledSphereOnSelfToggle = !DrawFilledSphereOnSelfToggle;
			if(DrawFilledSphereOnSelfToggle){
				_gameGizmos.drawFilledSphereOnSelf = true;
				DrawFilledSphereOnSelfName="Remove Filled Sphere on Self";
			}
			else
			{
				_gameGizmos.drawFilledSphereOnSelf = false;
				DrawFilledSphereOnSelfName="Draw Filled Sphere on Self";
			}
			EnableDisableFunctionDrawGizmos();
		}
	}
	#endregion

	#region DrawWiredBoxOnSelf
	bool DrawWiredBoxOnSelfToggle;
	string DrawWiredBoxOnSelfName="Draw Wired Box on Self";
	void DrawWiredBoxOnSelf()
	{
		if(GUILayout.Button(DrawWiredBoxOnSelfName)) 
		{
			DrawWiredBoxOnSelfToggle = !DrawWiredBoxOnSelfToggle;
			if(DrawWiredBoxOnSelfToggle){
				_gameGizmos.drawBoxonSelf = true;
				DrawWiredBoxOnSelfName="Remove Wired Box on Self";
			}
			else
			{
				_gameGizmos.drawBoxonSelf = false;
				DrawWiredBoxOnSelfName="Draw Wired Box on Self";
			}
			EnableDisableFunctionDrawGizmos();
		}
	}
	#endregion

	#region DrawIcons
	bool  DrawIconsOnChildsToggle;
	string DrawIconsOnChildsName="Draw Icons On Childs";
	void DrawIconsOnChilds()
	{
		if(GUILayout.Button(DrawIconsOnChildsName)) 
		{
			DrawIconsOnChildsToggle = !DrawIconsOnChildsToggle;
			if(DrawIconsOnChildsToggle){
				_gameGizmos.drawIconsOnChilds = true;
				DrawIconsOnChildsName="Remove Icons On Childs";
			}
			else
			{
				_gameGizmos.drawIconsOnChilds = false;
				DrawIconsOnChildsName="Draw Icons On Childs";
			}
			_gameGizmos.Update();
		}
	}
	#endregion
	void EnableDisableFunctionDrawGizmos()
	{
		_gameGizmos.enabled=false;
		_gameGizmos.enabled=true;
	}
	#endif
}
#endregion

#region CustomMenusInspector
[CustomEditor(typeof(CustomMenuDatabase)), CanEditMultipleObjects]
public class CustomMenusInspector:Editor
{
	#if UNITY_EDITOR
	CustomMenuDatabase _database;

	public SerializedProperty 
	Scene1Path,Scene2Path,Scene3Path,PrefabSavePath
	;

	void OnEnable()
	{
		_database = (CustomMenuDatabase)target	;
		Scene1Path = serializedObject.FindProperty("_inputValues.ScenePath1");
		Scene2Path = serializedObject.FindProperty("_inputValues.ScenePath2");
		Scene3Path = serializedObject.FindProperty("_inputValues.ScenePath3");
		PrefabSavePath = serializedObject.FindProperty("_inputValues.PrefabSavePath");
	}



	public override void OnInspectorGUI () 
	{
		WriteCustomMenusData();
		serializedObject.Update ();
		EditorUtility.SetDirty(_database);
		//AssetDatabase.SaveAssets();
		GUILayout.Space(5);
		EditorGUILayout.BeginHorizontal("Box");
		GUILayout.Label("Custom Menus Input");
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(5);
		EditorGUILayout.BeginVertical("Box");
		GetScenePath1();
		GetScenePath2();
		GetScenePath3();
		PrefabSavePathFunction();
		EditorGUILayout.EndVertical();

	}

	#region GetScenePath1
	void GetScenePath1()
	{
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.PropertyField(Scene1Path);
		_database._inputValues.ScenePath1 = Scene1Path.stringValue;
		if(GUILayout.Button("Get Current Scene Path"))
		{			
			bool value = EditorApplication.SaveScene();
			if(value)
			{
				_database._inputValues.ScenePath1 = SceneManager.GetActiveScene().path.ToString();
			}
		}

		CustomMenus.Path1 =_database._inputValues.ScenePath1;
	
		EditorGUILayout.EndVertical();
	} 
	#endregion
	#region GetScenePath2
	void GetScenePath2()
	{
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.PropertyField(Scene2Path);
		_database._inputValues.ScenePath2 = Scene2Path.stringValue;
		if(GUILayout.Button("Get Current Scene Path"))
		{
			bool value = EditorApplication.SaveScene();
			if(value)
			{
				_database._inputValues.ScenePath2 = SceneManager.GetActiveScene().path.ToString();
			}
		}
		CustomMenus.Path2 =_database._inputValues.ScenePath2;
		EditorGUILayout.EndVertical();
	}	
	#endregion
	#region GetScenePath3
	void GetScenePath3()
	{
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.PropertyField(Scene3Path);
		_database._inputValues.ScenePath3 = Scene3Path.stringValue;
		if(GUILayout.Button("Get Current Scene Path"))
		{
			bool value = EditorApplication.SaveScene();
			if(value)
			{
				_database._inputValues.ScenePath3 = SceneManager.GetActiveScene().path.ToString();
 				
			}
		}
		CustomMenus.Path3 =_database._inputValues.ScenePath3;
		EditorGUILayout.EndVertical();
	}	
	#endregion
	#region PrefabSavePath
	void PrefabSavePathFunction()
	{
		GUILayout.Space(5);
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.PropertyField(PrefabSavePath);
		_database._inputValues.PrefabSavePath = PrefabSavePath.stringValue;
		CustomMenus.PrefabSavePath =_database._inputValues.PrefabSavePath;
		/*if(GUILayout.Button("Get Current Scene Path"))
		{
			
		}*/
		EditorGUILayout.EndVertical();
	}
	#endregion


	public  void WriteCustomMenusData()
	{
		string createText = Scene1Path.stringValue + Environment.NewLine + Scene2Path.stringValue + Environment.NewLine  + Scene3Path.stringValue + Environment.NewLine +
		PrefabSavePath.stringValue ;
		File.WriteAllText(CustomMenus.textFilePath, createText);
	}

	public static void AssignValues()
	{
//		Debug.Log("CustomMenus.Path1:"+CustomMenus.Path1);
		if(CustomMenus.Path1 !="")
		{
			return;
		}
	
		if(File.Exists(CustomMenus.textFilePath))
		{
			int i=0;
			foreach(string s in File.ReadAllLines(CustomMenus.textFilePath))
			{
				if(i==0){CustomMenus.Path1 = s;}
				else if(i==1){CustomMenus.Path2 = s;}
				else if(i==2){CustomMenus.Path3 = s;}
				else if(i==3){CustomMenus.PrefabSavePath = s;}
				else{Debug.Log("Assign This:"+s);}
				i++;
			}
		}
	}
	#endif								
}



#endregion

[InitializeOnLoad]
public class Startup
{
	#if UNITY_EDITOR
	//On unity Startup i want to Assign all custom Variables Values 
    static Startup()
    {
		CustomMenusInspector.AssignValues();
    }
	#endif
}


