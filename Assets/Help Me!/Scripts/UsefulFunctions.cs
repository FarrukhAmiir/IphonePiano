using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Specialized;


public class UsefulFunctions :MonoBehaviour {

	#region OverlapSphereSystem
	//Give this functions parameters
	//1 - Overlap Sphere Position
	//2 - Overlap Sphere Radius 
	//3 - Targeted Tags (Array)
	//and this code will return us a list of your targeted objects
	//e.g TargetedObjectList = DrawOverlapSphere(Vector3.Zero , 50 ,string[] array = new string[]{"NPC","Humans"});
	public static List<GameObject> DrawOverlapSphere(Vector3 position , float radius , string[] targetedTags)
	{
		Collider[] colliders = Physics.OverlapSphere(position , radius);
		List<GameObject> objectsList = new List<GameObject>();
		for(int i=0;i<colliders.Length;i++)
		{
			for(int j=0 ; j<targetedTags.Length ; j++)
			{
				if(targetedTags[j].Equals(colliders[i].tag))
				{
					objectsList.Add(colliders[i].gameObject);
					//Debug.Log(colliders[i].tag);
				}
			}
		}
		return objectsList;
	}
	#endregion

	#region GetChilds
	//This Function Will Return all the childs(in a Transform Array) of the Given Object
	//e.g childs = GetChilds(ChildsParent);
	public static Transform[] GetChilds(Transform Parent)
	{
		Transform[] childs=new Transform[Parent.childCount];
		for(int i=0;i<Parent.childCount;i++)
		{
			childs[i]=Parent.GetChild(i);
		}//End of loop
		return childs;
	}//End of GetPath
	#endregion


	public static void CallFunction(List<GameObject> objs , string scriptName , string functionName , List<Object> parameters)
	{
		
		foreach(GameObject obj in objs)
		{
			MonoBehaviour c = (MonoBehaviour)obj.GetComponent(scriptName.ToString());
			//MethodInfo m = c.GetType().GetMethod(functionName);

			c.BroadcastMessage(functionName.ToString());     
			 
			//c.functionName() ;  
		
		}
	}


	#region FindWithTagChildern
	public static GameObject FindObjectWithTagAllChildren(Transform parent, string tag)
    {
             List<GameObject> taggedGameObjects = new List<GameObject>();
 
             for (int i = 0; i < parent.childCount; i++)
             {
                 Transform child = parent.GetChild(i);
                 Debug.Log(""+child.name);
                 if (child.tag == tag)
                 {
                     return child.gameObject;
                     break;
                 }
                 if (child.childCount > 0)
                 {
					taggedGameObjects.AddRange(FindObjectsWithTagAllChildren(child, tag));
                 }
             }
             Debug.Log("Child with Tag Not Found");
             return null;
    }


	public  static List<GameObject> FindObjectsWithTagAllChildren(Transform parent, string tag)
    {
             List<GameObject> taggedGameObjects = new List<GameObject>();
 
             for (int i = 0; i < parent.childCount; i++)
             {
                 Transform child = parent.GetChild(i);
                 if (child.tag == tag)
                 {
                     taggedGameObjects.Add(child.gameObject);
                 }
                 if (child.childCount > 0)
                 {
					taggedGameObjects.AddRange(FindObjectsWithTagAllChildren(child, tag));
                 }
             }
             return taggedGameObjects;
    }

	#endregion
}
