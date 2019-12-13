using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow
{
    private static string[] option2;
    public int index;

        [SerializeField]
    private DialogContainer Dialog;

    [MenuItem("Window/Node Editor")]
    static void Init()
    {
        NodeEditor w = GetWindow<NodeEditor>();
        DialogContainer openDialog = Selection.activeObject as DialogContainer;
        if (openDialog != null)
        {
            w.Dialog = openDialog;
        }

        
    }

    [MenuItem("Assets/Create/SerializationTest")]
    static void CreateAsset()
    {
        DialogContainer Dialog = CreateInstance<DialogContainer>();
        AssetDatabase.CreateAsset(Dialog, "Assets/test.asset");
    }


    void OnEnable()
    {
        if (Dialog == null)
            Dialog = CreateInstance<DialogContainer>();
    }

    void OnGUI()
    {
        if (Dialog == null)
        {
            Debug.Log("Dialog is null");
            return;
        }
        var optiontype = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
            from assemblyType in domainAssembly.GetTypes()
            where typeof(BaseNode).IsAssignableFrom(assemblyType)
            select assemblyType.ToString()).ToArray();
        option2 = optiontype;
        index = EditorGUILayout.Popup(index, option2);
        GUILayout.Label("Serialized Things", EditorStyles.boldLabel);
        Dialog.OnGUI();

        if (GUILayout.Button("Add BaseNode"))
        {
            BaseNode newAsset = CreateInstance<BaseNode>();
            AssetDatabase.AddObjectToAsset(newAsset, Dialog);

            // Reimport the asset after adding an object.
            // Otherwise the change only shows up when saving the project
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
            Dialog.Nodes.Add(newAsset);
            Debug.Log(AssetDatabase.GetAssetPath(Dialog));
        }

        if (GUILayout.Button("Add ChildNode"))
        {
            BaseNode newAsset = CreateInstance<DialogNode>();
            AssetDatabase.AddObjectToAsset(newAsset, Dialog);

            // Reimport the asset after adding an object.
            // Otherwise the change only shows up when saving the project
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
            Dialog.Nodes.Add(newAsset);
            Debug.Log(AssetDatabase.GetAssetPath(Dialog));
        }
    }
}