using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int totalTile;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private float tileLenght;
    
    public GameObject tile = null;




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(tileLenght * sizeX, tileLenght * sizeY, 0.1f));
    }
}
