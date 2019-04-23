using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Transform terrain;

    public int x;
    public int y;

    private void Awake()
    {
        Transform buff;
        Vector3 terrainPosition = new Vector3();
        for(int i = 0; i < y; i++)
        {
            for(int j = 0; j < x; j++)
            {
                buff = Instantiate(terrain);
                terrainPosition.Set(j*40,0,i*40);
                buff.position = terrainPosition;
            }
        }
    }
}
