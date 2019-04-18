using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    MeshCollider collider;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize;
    public float gridOffset;
    public int gridSize;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        MakeProceduralGrid();
        UpdateMesh();
    }

    private void MakeProceduralGrid()
    {
        vertices = new Vector3[(gridSize+1)*(gridSize+1)];
        triangles = new int[6 * gridSize* gridSize];

        float vertexOffset = cellSize * 0.5f;

        System.Random r = new System.Random() ;
        
        for(int i = 0; i <= gridSize; i++)
        {
            for(int j = 0; j <= gridSize; j++)
            {
                vertices[i * (gridSize + 1) + j] = new Vector3(i * cellSize - vertexOffset, (float)r.NextDouble(), j * cellSize - vertexOffset);
            }
        }

        /*
                vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset);
                vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset);
                vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset);
                vertices[3] = new Vector3(vertexOffset, 0, vertexOffset);
                */

        //triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        int k = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                triangles[k++] = i* (gridSize + 1) + j;
                triangles[k++] = i * (gridSize + 1) + j + 1;
                triangles[k++] = (i + 1) * (gridSize + 1) + j;
                triangles[k++] = (i + 1) * (gridSize + 1) + j;
                triangles[k++] = i * (gridSize + 1) + j + 1;
                triangles[k++] = (i + 1) * (gridSize + 1) + j + 1;
            }
                
          //  Debug.Log($"{triangles[k-6]},{triangles[k-5]},{triangles[k-4]},{triangles[k-3]}," +
        //        $"{triangles[k-2]},{triangles[k-1]}");
        }
        
/*
        triangles = new int[] { 0, 1, 3, 3, 1, 4,
        1,2,4,4,2,5,
        3,4,6,6,4,7,
        4,5,7,7,5,8};
        */
       
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        collider.sharedMesh = mesh;
    }

    [ContextMenu("Create Mesh")]
    public void CreateMesh()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        MakeProceduralGrid();
        UpdateMesh();
//        Debug.Log("Mesh Created");
    }
}
