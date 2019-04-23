using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed;
    public Vector3 cameraDistance = new Vector3(0,20f,10f);
    void Start()
    {
        
    }
    void LateUpdate()
    {
        LookAtObject();
        Move();
    }

    private void Move()
    {
        Vector3 targetPosition = objectToFollow.transform.position + cameraDistance;
        if(Vector3.Distance(targetPosition,transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPosition,Time.deltaTime*speed);
        }
    }

    private void LookAtObject()
    {
        if(objectToFollow != null)
        {
            transform.LookAt(objectToFollow.transform);
        }
    }
}
