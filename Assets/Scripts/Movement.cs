using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //global variables
    public float movementSpeed = 5;
    public float sprintingSpeed = 5;
    private bool coroutineRunning = false;
    public GameObject footprints;
    Rigidbody2D rb;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        float horizontalX = Input.GetAxisRaw("Horizontal");
        float verticalY = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(horizontalX, verticalY);

        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, transform.position.x, 2f * Time.deltaTime), Mathf.Lerp(cam.transform.position.y, transform.position.y, 2f * Time.deltaTime), -10);
        rb.velocity = movementVector.normalized * movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintingSpeed;
        }
        else { movementSpeed = 5f; }
        if (movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            StartCoroutine(FootprintRoutine(angle));
        }
        
        
    }
    public IEnumerator FootprintRoutine(float angle)
    {
        if (coroutineRunning == true)
        {
            yield break;
        }
        coroutineRunning = true;
        yield return new WaitForSecondsRealtime(0.2f);
        
        Instantiate(footprints, transform.position, Quaternion.Euler(0f, 0f, angle + 90));

        coroutineRunning = false;
    }
}
