using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;
using System;

public class Movement : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap tilemap = new Tilemap();

    [Header("Movement")]
    public float movementSpeed = 5;
    public float sprintingSpeed = 5;
    public bool isCamFollow = true;
    Rigidbody2D rb;
    Camera cam;

    [Header("PushBack")]
    public float targetForce = -300;
    private float currentForce = 0f;
    private float changeForceDuration = 2f;
    private float changeStartTime;

    [Header("Footprints")]
    public GameObject footprints;
    private bool placingStep = false;

    [Header("Tools")]
    private GameObject tool = null;
    private ToolClass toolClassIN;
    private float angleToMouse;

    [Header("Garbage Management")]
    public int garbageRemoved;

    [Header("Energy Management")]
    public int energyStored;
    public bool isDrainingEnergy = false;
    public bool isRestoringEnergy = false;

    [Header("Visual Effects")]
    public VisualEffect vfxRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        
    }

    void FixedUpdate()
    {
        WASDMovement();
        PlayerOutsideMap();
        ControlTools();

        vfxRenderer.SetVector3("transformPos", transform.position);
    }

    private void ControlTools()
    {
        var worldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        Vector3 directionToMouse = worldMousePosition - transform.position;
        directionToMouse.Normalize();

        angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        if (tool != null)
        {
            tool.transform.rotation = Quaternion.AngleAxis(angleToMouse - 90, Vector3.forward);

            if (toolClassIN.isRepulsive)
            {
                
                RepulsiveTool();
            }
        }
    }

    private void RepulsiveTool()
    {
        PointEffector2D forceField = tool.GetComponent<PointEffector2D>();
        forceField.forceMagnitude = toolClassIN.repulsionStrength;
        var point = forceField.forceSource;
        print(point);
        if (Input.GetKey(KeyCode.Space) && energyStored > toolClassIN.powerUsage)
        {
            if (!isDrainingEnergy) { StartCoroutine(DrainEnergy()); }
            forceField.enabled = true;

        }
        else
        {
            forceField.enabled = false;
        }

        if (!isDrainingEnergy && toolClassIN != null)
        {
            if (!isRestoringEnergy) { StartCoroutine(RestoreEnergy()); }
            
        }
    }

    IEnumerator RestoreEnergy()
    {
        isRestoringEnergy = true;
        yield return new WaitForSeconds(0.5f);
        if (energyStored < toolClassIN.maxEnergy) { energyStored += 10; }
        if (energyStored > toolClassIN.maxEnergy) { energyStored = toolClassIN.maxEnergy; }
        isRestoringEnergy = false;
    }

    IEnumerator DrainEnergy()
    {
        isDrainingEnergy = true;
        yield return new WaitForSeconds(toolClassIN.timeBetweenEnergyUseages);
        energyStored -= toolClassIN.powerUsage;
        if (energyStored < 0) { energyStored = 0; }
        isDrainingEnergy = false;
    }

    private void PlayerOutsideMap()
    {
        if (!gameObject.GetComponent<Collider2D>().bounds.Intersects(tilemap.localBounds))
        {
            var directionToCenter = new Vector3(0, 0, 0) - transform.position;
            directionToCenter.Normalize();
            StartCoroutine(ChangeForce(directionToCenter));
        }
    }

    private void WASDMovement()
    {
        float horizontalX = Input.GetAxisRaw("Horizontal");
        float verticalY = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(horizontalX, verticalY);

        if(isCamFollow) cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, transform.position.x, 4f * Time.deltaTime), Mathf.Lerp(cam.transform.position.y, transform.position.y, 2f * Time.deltaTime), -10);
        rb.velocity = movementVector.normalized * movementSpeed;

        movementSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintingSpeed : 5f;

        if (movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;

            if (!placingStep) StartCoroutine(FootprintRoutine(angle));
        }
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

    public void ChangeTool(ToolClass toolClass)
    {
        if(tool != null) {

            //tool.transform.SetParent(null);
            Destroy(tool);
            
            
        }
        toolClassIN = toolClass;
        tool = toolClass.toolPrefab;
        tool = Instantiate(tool, transform.position, Quaternion.identity, transform);
        
    }
    
}
