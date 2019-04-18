using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float yValue =  0f;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeMeshData();
        CreateMesh();
    }

    private void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void MakeMeshData()
    {
        vertices = new Vector3[] {new Vector3(0f,yValue,0f),
            new Vector3(0f,0f,1f),
            new Vector3(1f,0f,0f),
            new Vector3(1f,0,1f)};

        triangles = new int[] { 0, 1, 2, 2, 1, 3 };
    }

    [ContextMenu("Update Mesh")]
    void UpdateMesh()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData();
        CreateMesh();
        Debug.Log("Mesh updated");
    }
}
