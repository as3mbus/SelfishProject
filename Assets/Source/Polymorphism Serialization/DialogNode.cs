using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogNode : BaseNode
{
	[SerializeField]
	public string Text;
	
	
	public override void OnGUI()
	{
		base.OnGUI();
		Text = EditorGUILayout.TextArea (Text);
		
	}
	
	public DialogNode ()
	{
		
	}
}