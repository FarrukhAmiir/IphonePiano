using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CustomTreeBrush : EditorWindow
{
	
    Terrain terrain;
	
    GameObject[] trees = new GameObject[4];

	
    [MenuItem("Tools/Custom/Terrain")]
    static void Init()
    {
        //CustomTreeBrush window = (CustomTreeBrush)GetWindow (typeof(CustomTreeBrush));
    }

    void OnGUI()
    {
        terrain = (Terrain)EditorGUILayout.ObjectField(terrain, typeof(Terrain), true);
		
        trees[0] = (GameObject)EditorGUILayout.ObjectField(trees[0], typeof(GameObject), true);
        trees[1] = (GameObject)EditorGUILayout.ObjectField(trees[1], typeof(GameObject), true);
        trees[2] = (GameObject)EditorGUILayout.ObjectField(trees[2], typeof(GameObject), true);
        trees[3] = (GameObject)EditorGUILayout.ObjectField(trees[3], typeof(GameObject), true);
		
        if (GUILayout.Button("Convert to objects"))
        {
            Convert();
        }
        //    if(GUILayout.Button("Debug"))
        //    {
        //    }
    }

    public void Convert()
    {
        TerrainData data = terrain.terrainData;
        float width = data.size.x;
        float height = data.size.z;
        float y = data.size.y;
        foreach (TreeInstance tree in data.treeInstances)
        {
            Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);
            Instantiate(trees[tree.prototypeIndex], position, Quaternion.identity);
        }
    }
}