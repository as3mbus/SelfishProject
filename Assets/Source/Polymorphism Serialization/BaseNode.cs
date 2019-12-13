using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BaseNode : ScriptableObject
{
    [SerializeField]
    public Rect window;

    public void OnEnable ()
    {
        if (window == null)
            window = new Rect(0,0,120,120);

        hideFlags = HideFlags.HideInInspector;
    }

    public virtual void OnGUI()
    {
        window = EditorGUILayout.RectField (window);
    }

    public BaseNode ()
    {
	
    }
}