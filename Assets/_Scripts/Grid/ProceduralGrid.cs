using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer),typeof(MeshCollider))]
public class ProceduralGrid : MonoBehaviour
{
    public float cellSize = 1f;
    public float gridOffset;
    public float gridOffsetAdd;
    public int gridSize;
    public float terrainMovementTime = 0.01f;
    Color[] vertexColor;
    Mesh mesh;
    MeshCollider collider;
    Vector3[] vertices;
    int[] triangles;
    float timePassedSinceLastMove;
    int _gridSize;
    bool currentGrid = false;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        timePassedSinceLastMove = 0;
        MakeProceduralGrid();
        UpdateMesh();
        collider.sharedMesh = mesh;
        _gridSize = gridSize;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            currentGrid = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            currentGrid = false;
        }
    }

    void Update()
    {
        gridOffset += gridOffsetAdd;
        if(_gridSize == gridSize)
        {
            RecalculateVertexPosition();
        }
        else
        {
            _gridSize = gridSize;
            MakeProceduralGrid();
        }
        UpdateMesh();
        if(currentGrid)
        {
            collider.sharedMesh = mesh;
            Debug.Log("Qaz");
        }
    }

    private void MakeProceduralGrid()
    {
        vertices = new Vector3[(gridSize+1)*(gridSize+1)];
        vertexColor = new Color[(gridSize+1)*(gridSize+1)];
        triangles = new int[6 * gridSize* gridSize];
        
        RecalculateVertexPosition();

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
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColor;
        mesh.RecalculateNormals();
    }

    private void RecalculateVertexPosition()
    {
        float vertexOffset = cellSize * 0.5f;

        for(int i = 0; i <= gridSize; i++)
        {
            for(int j = 0; j <= gridSize; j++)
            {
                vertices[i * (gridSize + 1) + j] = new Vector3(i * cellSize - vertexOffset, Mathf.PerlinNoise((float)(i)*0.1f+gridOffset,(float)(j)*0.1f+gridOffset)*10f, j * cellSize - vertexOffset);
                vertexColor[i * (gridSize + 1) + j] = new Color(vertices[i * (gridSize + 1) + j].y*0.1f,1f - vertices[i * (gridSize + 1) + j].y*0.1f,0,1);
            }
        }
    }

    [ContextMenu("Create Mesh")]
    public void CreateMesh()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        MakeProceduralGrid();
        UpdateMesh();
        Debug.Log("Mesh Created");
    }
    
    public ProceduralGrid()
    {
    cellSize = 1f;
    gridOffset = 0;
    gridOffsetAdd = 0.01f;
    gridSize = 20;
    terrainMovementTime = 0.01f;
    }

    public ProceduralGrid(float x, float y, float z)
    {
    cellSize = 1f;
    gridOffset = 0;
    gridOffsetAdd = 0.01f;
    gridSize = 20;
    terrainMovementTime = 0.01f;
    transform.position.Set(x,y,z);
    }

    public ProceduralGrid(float cellSize, float gridOffset, float gridOffsetAdd, int gridSize,float terrainMovementTime)
    {
    this.cellSize = cellSize;
    this.gridOffset = gridOffset;
    this.gridOffsetAdd = gridOffsetAdd;
    this.gridSize = gridSize;
    this.terrainMovementTime = terrainMovementTime;
    }
}
