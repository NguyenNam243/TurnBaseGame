using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    

    public void GoCombatScene()
    {
        LoadSceneExtension.LoadScene("Combat");
    }
}
