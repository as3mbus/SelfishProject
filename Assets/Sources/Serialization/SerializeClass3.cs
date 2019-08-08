using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BaseClass
{
	[SerializeField]
	private int m_IntField;
	public virtual void OnGUI() { m_IntField = EditorGUILayout.IntSlider("IntField", m_IntField, 0, 10); }
}

[Serializable]
public class ChildClass : BaseClass
{
	[SerializeField]
	private float m_FloatField;
	public override void OnGUI()
	{
		base.OnGUI ();
		m_FloatField = EditorGUILayout.Slider("FloatField", m_FloatField, 0f, 10f);
	}
}

[Serializable]
public class SerializeMe3 : ScriptableObject
{
	[SerializeField]
	private List<BaseClass> m_Instances;

	public void OnEnable ()
	{
		if (m_Instances == null)
			m_Instances = new List<BaseClass> ();

		hideFlags = HideFlags.HideAndDontSave;
	}

	public void OnGUI ()
	{
		foreach (var instance in m_Instances)
			instance.OnGUI ();

		if (GUILayout.Button ("Add Base"))
			m_Instances.Add (new BaseClass ());
		if (GUILayout.Button ("Add Child"))
			m_Instances.Add (new ChildClass ());
	}
}