using UnityEditor;
 using UnityEngine;
 
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
 public class ReadOnlyDrawer : PropertyDrawer
 {
     public override float GetPropertyHeight(SerializedProperty property,
                                             GUIContent label)
     {
         return EditorGUI.GetPropertyHeight(property, label, true);
     }
 
     public override void OnGUI(Rect position,
                                SerializedProperty property,
                                GUIContent label)
     {
		
        GUI.enabled = false;
      
        Color color = GUI.color;
		Color backgroundColor = GUI.backgroundColor;

	
        GUI.Box(position, new GUIContent());
		
		GUI.backgroundColor = new Color(255,0,0);
		GUI.color = new Color(0,255,0);

        EditorGUI.PropertyField(position, property, label, true);

		GUI.color = color;
		GUI.backgroundColor = backgroundColor;

        GUI.enabled = true;
     }
 }

