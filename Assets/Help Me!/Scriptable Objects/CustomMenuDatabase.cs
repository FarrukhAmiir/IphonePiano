using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomMenuDatabase" ,menuName = "HelpMe!/CustomMenuDatabase" ,order=1)]
public class CustomMenuDatabase : ScriptableObject {
	public CustomMenusInput _inputValues = new CustomMenusInput();

	//public List<CustomMenusInput> _inputValues = new List<CustomMenusInput>();

}
