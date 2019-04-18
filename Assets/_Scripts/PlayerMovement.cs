using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float mHorizontal;
    float mVertical;
    Vector3 movement;


    Rigidbody rb;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mHorizontal = Input.GetAxis("Horizontal");
        mVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        movement.Set(mHorizontal, 0f, mVertical);
        movement = movement.normalized;
        movement *= Time.deltaTime * Speed;

        rb.MovePosition(rb.position + movement);
    }
}
