using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HeroController))]
public class HeroControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        HeroController controller = (HeroController)target;

        GUILayout.Space(10);
        if (GUILayout.Button("Show position"))
        {
            Debug.Log(controller.transform.position);
        }

        if (GUILayout.Button("Attack"))
        {
            controller.DoAttack(controller.targetAttack);
        }
    }
}
