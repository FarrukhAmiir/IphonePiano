using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;
    public bool drag;
	Canvas canvas;
	public GameObject m_DraggingIcon;
	private RectTransform m_DraggingPlane;

	public UnityEngine.UI.Extensions.UILineRenderer Line;
    RectTransform startObject;
    RectTransform endObject;
 
	public GameObject Dragger;

	public void OnBeginDrag(PointerEventData eventData)
	{

		 canvas = transform.root.gameObject.GetComponent<Canvas>();
		if (canvas == null)
			return;

		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
		m_DraggingIcon = new GameObject("icon");
//		m_DraggingIcon.transform.parent = GameObject.Find ("Shape").transform;
		m_DraggingIcon.transform.parent = gameObject.transform.parent.gameObject.transform;
//		m_DraggingIcon.transform.SetParent(canvas.transform, false);
//		m_DraggingIcon.transform.SetAsLastSibling();

		var image = m_DraggingIcon.AddComponent<Image>();

		image.sprite = GetComponent<Image>().sprite;
		//image.SetNativeSize();
        startObject = m_DraggingIcon.GetComponent<RectTransform>();
        
		if (dragOnSurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;

		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
		if (m_DraggingIcon != null)
			SetDraggedPosition(data);


		Dragger.transform.position = m_DraggingIcon.transform.position ;
	}

	public void SetDraggedPosition(PointerEventData data)
	{
		Line.gameObject.SetActive (true);
		if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
			m_DraggingPlane = data.pointerEnter.transform as RectTransform;

		 endObject = m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
		{


            endObject.position = globalMousePos;
            endObject.rotation = m_DraggingPlane.rotation;
            drag = true;
            //Line.OnRebuildRequested();
            //Line.Rebuild(CanvasUpdate.PreRender);
			//Line.enabled=true;
            Line.Points[0] = GetComponent<RectTransform>().anchoredPosition;
            Line.Points[1] = endObject.anchoredPosition;
            Line.SetAllDirty();
            
		}
	}



	public void OnEndDrag(PointerEventData eventData)
	{

        drag = false;
		if (m_DraggingIcon != null)
			Destroy(m_DraggingIcon);

//		if (MatchingCollisonManager.collisionChecker) 
//		{
//			Dragger.transform.parent.GetChild(2).GetComponent<MatchingCollisonManager>().DestroyDragger ();
//			MatchingCollisonManager.collisionChecker = false;
//			Line.Points [1] = GetComponent<RectTransform> ().anchoredPosition;	
//			Matching_ShapesManager.NumberOfShapes--;
//			Debug.Log (Matching_ShapesManager.NumberOfShapes);
//			if (Matching_ShapesManager.NumberOfShapes <= 0) 
//			{
//
//				FindObjectOfType<Matching_ShapesManager> ().LevelComplete ();
//			}
//
//
//		}
//		else 
//		{
//			 
//			Dragger.transform.position = transform.position;
//			Line.gameObject.SetActive (false);
//
//		}



	}
    public void OnGUI()
    {
        
            // DrawBoxAroundPoints(endObject.position, startObject.position);
        //if (endObject )
        //{
        //    Line.Points[0] = GetComponent<RectTransform>().anchoredPosition;
        //    Line.Points[1] = endObject.anchoredPosition;
        //    Line.SetAllDirty();
        //}
        
    }

    void DrawBoxAroundPoints(Vector2 p0, Vector2 p1, float height = 6, float extraLength = 0)
    {
        // Draw a thin, rotated box around the line between the given points.
        // Our approach is to rotate the GUI transformation matrix around the center
        // of the line, and then draw an unrotated (horizontal) box at that point.
        float width = (p1 - p0).magnitude + extraLength;
        Vector2 center = (p0 + p1) * 0.5f;
        Rect horizontalRect = new Rect(center.x - width / 2, center.y - height / 2, width, height);
        float angle = Mathf.Atan2(p1.y - p0.y, p1.x - p0.x) * Mathf.Rad2Deg;

        Matrix4x4 savedMatrix = GUI.matrix;
        Vector3 centerScreen = GUIUtility.GUIToScreenPoint(center);
        GUI.matrix =
            Matrix4x4.TRS(centerScreen, Quaternion.identity, Vector3.one)
                * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one)
                * Matrix4x4.TRS(-centerScreen, Quaternion.identity, Vector3.one)
                * GUI.matrix;

        GUI.Box(horizontalRect, "");
        GUI.matrix = savedMatrix;
    }

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;

		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}