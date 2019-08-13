using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class NestedClass : ScriptableObject
{
    [SerializeField]
    private float m_StructFloat;

    public void OnEnable() { hideFlags = HideFlags.HideAndDontSave; }

    public void OnGUI()
    {
        m_StructFloat = EditorGUILayout.FloatField("Float", m_StructFloat);
    }
}

[Serializable]
public class SerializeMe2 : ScriptableObject
{
    [SerializeField]
    private NestedClass m_Class1;

    [SerializeField]
    private NestedClass m_Class2;

    private void OnEnable()
    {
        hideFlags = HideFlags.HideAndDontSave;
        m_Class1 = ScriptableObject.CreateInstance<NestedClass>();
        m_Class2 = m_Class1;
    }

    public void OnGUI()
    {
        m_Class1.OnGUI();
        m_Class2.OnGUI();
    }
}