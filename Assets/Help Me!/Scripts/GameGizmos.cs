
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;
using UnityEngine;
using System.Collections;
using System;

#region GameGizmos
[ExecuteInEditMode]
public class GameGizmos : MonoBehaviour {

#if UNITY_EDITOR
	// Use this for initialization
	Transform[] pointsArray;

	//[Header("Gizmos")]
	public float radius=1;
	public Vector3 size;
	public bool drawPathOnChilds=false;
	public bool drawFilledSphereOnChilds=false;
	public bool drawEmptySphereOnSelf=false;
	public bool drawFilledSphereOnSelf=false;
	public bool drawIconsOnChilds=false;
	public bool drawBoxonSelf=false;

	//[Header("Functions")]
	public string childName = "";
	public int  startingNumber=0;
	public bool renameChildsNumerically=false;
	public bool reverseAllChilds=false;

	Color color;
	int childCount;
	int randomNumber ;

	void Start () {
		childCount=transform.childCount;
		GetPath();
		color=new Color(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
		randomNumber = UnityEngine.Random.Range(0,Enum.GetNames(typeof(IconManager.LabelIcon)).Length);
	}




	public void OnDrawGizmos() {

		if(!this.enabled)
		{
			return;
		}

		Gizmos.color = color;
		if(childCount<transform.childCount)
		{
			GetPath();
		}

		#region DrawFillSphereChilds
		if(drawFilledSphereOnChilds)
		{

			for(int i=0;i<pointsArray.Length;i++)
			{
				if(pointsArray[i])
				{
					Gizmos.DrawSphere(pointsArray[i].position, radius);
//					IconManager.SetIcon(pointsArray[i].gameObject,IconManager.Icon.CircleBlue);
				}
					
			}
		}
		#endregion

		#region DrawPathChilds
		if(drawPathOnChilds)
		{			
			for(int i=1;i<pointsArray.Length;i++)
			{
				if(!pointsArray[i])
				{
					GetPath();
					childCount=transform.childCount;
					return;
				}
				Gizmos.DrawLine(pointsArray[i-1].position,pointsArray[i].position);
			}
		}
		#endregion

		#region DrawEmptySphereSelf
		if(drawEmptySphereOnSelf)
		{
			Gizmos.DrawWireSphere(transform.position,radius);
		}
		#endregion

		#region DrawFilledSphereSelf
		if(drawFilledSphereOnSelf)
		{
			Gizmos.DrawSphere(transform.position,radius);
		}
		#endregion

		#region DrawBoxOnSelf
		if(drawBoxonSelf)
		{
			Gizmos.DrawWireCube(transform.position,size);
		}
		#endregion


	}
		
	public void Update()
	{
		if(renameChildsNumerically)
		{
			RenameChilds();
			renameChildsNumerically = false;
		}

		if(reverseAllChilds)
		{
			ReverseChilds();
			reverseAllChilds = false ;
		}

		DrawIconsOnChilds();

	
	}

	void GetPath()
	{
		pointsArray = new Transform[transform.childCount];
		for(int i=0;i<transform.childCount;i++)
		{
			pointsArray[i]=transform.GetChild(i);
		}
	}

	#region renameChilds
	void RenameChilds()
	{ 
		int j=0;
		for(int i=startingNumber;i<transform.childCount+startingNumber;i++)
		{
			pointsArray[j].name = childName + " "+i;
			j++;
		}
	}
	#endregion

	#region ReverseChilds
	int counter;
	void ReverseChilds()
	{
		counter= 1;
		for(int i = 0; i<transform.childCount/2;i++)
		{
			Swap(transform.GetChild(i),transform.GetChild(transform.childCount-counter));
			counter++;
		}

		GetPath();
	}

	void Swap(Transform a ,Transform b)
	{
		int temp = a.GetSiblingIndex();
		a.transform.SetSiblingIndex(b.GetSiblingIndex());
		b.transform.SetSiblingIndex(temp);
	}
	#endregion

	#region DrawIconsOnChilds
	void DrawIconsOnChilds()
	{
		
		if(drawIconsOnChilds)
		{
			
			for(int i=0;i<pointsArray.Length;i++)
			{
				if(pointsArray[i])
				{
					IconManager.SetIcon(pointsArray[i].gameObject,(IconManager.LabelIcon)randomNumber);
				}
			}
		}
		else
		{
			for(int i=0;i<pointsArray.Length;i++)
			{
				if(pointsArray[i])
				{
					IconManager.UnAssignedIcon(pointsArray[i].gameObject);
				}
			}
		}
	}
	#endregion
	#endif
}
#endregion

#region IconManager
public class IconManager {

#if UNITY_EDITOR
    public enum LabelIcon {
        Gray = 0,
        Blue,
        Teal,
        Green,
        Yellow,
        Orange,
        Red,
        Purple
    }
 
    public enum Icon {
        CircleGray = 0,
        CircleBlue,
        CircleTeal,
        CircleGreen,
        CircleYellow,
        CircleOrange,
        CircleRed,
        CirclePurple,
        DiamondGray,
        DiamondBlue,
        DiamondTeal,
        DiamondGreen,
        DiamondYellow,
        DiamondOrange,
        DiamondRed,
        DiamondPurple
    }
 
    private static GUIContent[] labelIcons;
    private static GUIContent[] largeIcons;
 
    public static void SetIcon( GameObject gObj, LabelIcon icon ) {
        if ( labelIcons == null ) {
            labelIcons = GetTextures( "sv_label_", string.Empty, 0, 8 );
        }
 
        SetIcon( gObj, labelIcons[(int)icon].image as Texture2D );
		
    }

	public static void UnAssignedIcon (GameObject gObj)
    {
		SetIcon( gObj, null as Texture2D );
    }
 
    public static void SetIcon( GameObject gObj, Icon icon ) {
        if ( largeIcons == null ) {
            largeIcons = GetTextures( "sv_icon_dot", "_pix16_gizmo", 0, 16 );
        }
 
        SetIcon( gObj, largeIcons[(int)icon].image as Texture2D );
    }
 
    private static void SetIcon( GameObject gObj, Texture2D texture ) {
        var ty = typeof( EditorGUIUtility );
        var mi = ty.GetMethod( "SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static );
        mi.Invoke( null, new object[] { gObj, texture } );
    }
 
    public static GUIContent[] GetTextures( string baseName, string postFix, int startIndex, int count ) {
        GUIContent[] guiContentArray = new GUIContent[count];
 
        var t = typeof( EditorGUIUtility );
        var mi = t.GetMethod( "IconContent", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof( string ) }, null );
 
        for ( int index = 0; index < count; ++index ) {
            guiContentArray[index] = mi.Invoke( null, new object[] { baseName + (object)(startIndex + index) + postFix } ) as GUIContent;
        }
 
        return guiContentArray;
    }

    #endif
}

#endregion


