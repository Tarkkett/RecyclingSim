using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    //global variables
    public Tilemap tilemap = new Tilemap();

    public float movementSpeed = 5;
    public float sprintingSpeed = 5;

    private float targetForce = -100;
    private float currentForce = 0f;
    private float changeForceDuration = 3f;
    private float changeStartTime;

    public GameObject footprints;
    public GameObject tool;
    private bool placingStep = false;
    Rigidbody2D rb;
    Camera cam;

    private float angleToMouse;

    public int garbageRemoved;

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

        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, transform.position.x, 4f * Time.deltaTime), Mathf.Lerp(cam.transform.position.y, transform.position.y, 2f * Time.deltaTime), -10);
        rb.velocity = movementVector.normalized * movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintingSpeed;
        }
        else { movementSpeed = 5f; }
        if (movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;

            if (!placingStep) StartCoroutine(FootprintRoutine(angle));
        }

        
        if (!gameObject.GetComponent<Collider2D>().bounds.Intersects(tilemap.localBounds))
        {
            var directionToCenter = new Vector3(0, 0, 0) - transform.position;
            directionToCenter.Normalize();
            StartCoroutine(ChangeForce(directionToCenter));
            print(directionToCenter);
           
            
        }

        var worldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        Vector3 directionToMouse = worldMousePosition - transform.position;
        directionToMouse.Normalize();
        angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        tool.transform.rotation = Quaternion.AngleAxis(angleToMouse - 90, Vector3.forward);//Euler(0f, 0f, angleToMouse-90);
        print(angleToMouse);
        print(directionToMouse);
        
    }
    
    
    IEnumerator ChangeForce(Vector3 directionToCenter)
    {
        
        changeStartTime = Time.time;

        while (currentForce > targetForce)
        {
            float t = (Time.time - changeStartTime) / changeForceDuration;
            currentForce = Mathf.Lerp(0f, targetForce, t);
            rb.AddForce(new Vector3(-currentForce * directionToCenter.x, -currentForce * directionToCenter.y, 0));
            if (gameObject.GetComponent<Collider2D>().bounds.Intersects(tilemap.localBounds)) yield break;
            yield return currentForce;
        }

        currentForce = 0f;
        yield return null;
        
    }
    public IEnumerator FootprintRoutine(float angle)
    {
        placingStep = true;
        Instantiate(footprints, transform.position, Quaternion.Euler(0f, 0f, angle + 90));
        yield return new WaitForSecondsRealtime(0.2f);
        placingStep = false;
        yield break;

    }
    
}
