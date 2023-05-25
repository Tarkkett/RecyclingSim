using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //global variables
    public float movementSpeed = 5;
    public float sprintingSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        float horizontalX = Input.GetAxisRaw("Horizontal");
        float verticalY = Input.GetAxisRaw("Vertical");
        Vector3 movementVector = new Vector3(horizontalX, verticalY);

        transform.position += movementVector * Time.deltaTime * movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintingSpeed;
        }
        else { movementSpeed = 5f; }
       
        
    }
}
