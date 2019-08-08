using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow
{
    [SerializeField]
    private SerializeMe m_SerialziedThing;

    [SerializeField]
    private SerializeMe2 m_SerialziedThing2;
    [SerializeField]
    private SerializeMe3 m_SerialziedThing3;


    [MenuItem("Window/Serialization")]
    static void Init()
    {
        GetWindow(typeof(MyWindow));
    }

    void OnEnable()
    {
        hideFlags = HideFlags.DontSave;
        if (m_SerialziedThing == null)
            m_SerialziedThing = new SerializeMe();
        if (m_SerialziedThing2 == null)
            m_SerialziedThing2 = ScriptableObject.CreateInstance<SerializeMe2>();
        if (m_SerialziedThing3 == null)
            m_SerialziedThing3 = ScriptableObject.CreateInstance<SerializeMe3>();
    }

    void OnGUI()
    {
        GUILayout.Label("Serialized Things", EditorStyles.boldLabel);
        m_SerialziedThing.OnGUI();
        GUILayout.Label("Serialized Things2", EditorStyles.boldLabel);
        m_SerialziedThing2.OnGUI();
        GUILayout.Label("Serialized Things3", EditorStyles.boldLabel);
        m_SerialziedThing3.OnGUI();
    }
}