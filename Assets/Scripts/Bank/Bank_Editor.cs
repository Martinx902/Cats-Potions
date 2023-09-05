using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(BankAccount))]
public class Bank_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BankAccount bankAccount = (BankAccount)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Money", EditorStyles.miniButton))
        {
            bankAccount.AddFunds(150);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Subtract Money", EditorStyles.miniButton))
        {
            bankAccount.AddFunds(-50);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Actual Balance: ", bankAccount.GetAccountBalance().ToString());
    }
}

#endif