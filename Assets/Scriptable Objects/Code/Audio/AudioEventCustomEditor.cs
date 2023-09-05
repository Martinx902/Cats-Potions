//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 07/11/2022

using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(AudioEvent), true)]
public class AudioEventCustomEditor /*: Editor*/
{
    [SerializeField] private AudioSource previewer;

    public void OnEnable()
    {
        //previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
        //DestroyImmediate(previewer.gameObject);
    }

    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    //Creates a preview audio button on the Audio Events Inspector

    //    EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
    //    if(GUILayout.Button("Preview"))
    //    {
    //        ((AudioEvent)target).Play(previewer);
    //    }
    //    EditorGUI.EndDisabledGroup();
    //}
}
