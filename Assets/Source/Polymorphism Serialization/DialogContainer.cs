using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DialogContainer : ScriptableObject
{
	[SerializeField]
	public List <BaseNode> Nodes;

	[SerializeField]
	public int NodesCounter;

	public void OnEnable ()
	{
		if (Nodes == null)
			Nodes = new List<BaseNode> ();

	}

	public void OnGUI()
	{
		foreach (var instance in Nodes)
		{
			instance.OnGUI();
			EditorGUILayout.Space ();
		}

	}


}