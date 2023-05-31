using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GarbageRemoved : MonoBehaviour
{
    
    private bool coroutineRunning = false;
    private float startTime;
    private float duration = 3f;
    private float currentScale = 0.3f;
    private float targetScale = 0f;
    private int garbageValue = 10000;
    public TextMeshProUGUI valueText;
    private Spawner spawner;
    Canvas[] allWorldCanvases;
    Canvas gameCanvas;

    private void Start()
    {
        spawner =  FindObjectOfType<Spawner>();
        allWorldCanvases = FindObjectsOfType<Canvas>();
        for (int i = 0; i < allWorldCanvases.Length; i++)
        {
            if (allWorldCanvases[i].gameObject.name == "GameCanvas")
            {
                gameCanvas = allWorldCanvases[i];
            }
        }
    }
    private void Update()
    {
        
        Tilemap tilemap = FindObjectOfType<Movement>().tilemap;
        if (!gameObject.GetComponent<Collider2D>().bounds.Intersects(tilemap.localBounds))
        {
            if (!coroutineRunning) { StartCoroutine(GarbageOutideBounds()); }

        };

    }
    private IEnumerator GarbageOutideBounds()
    {
        coroutineRunning = true;
        startTime = Time.time;
        

        while (currentScale > targetScale) {
            var currentTime = (Time.time - startTime) / duration;

            currentScale = Mathf.Lerp(currentScale, targetScale, currentTime);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale <= 0.1f)
            {
                Destroy(gameObject);
                
                
                FindObjectOfType<Movement>().garbageRemoved += garbageValue;
                spawner.spawnedObjects.Remove(gameObject);
                UpdateText();
                

                yield break;
            }
            yield return null;
        }
        currentScale = 0f;
        coroutineRunning = false;
        yield return null;
    }

    private void UpdateText()
    {
        valueText.text = garbageValue.ToString();
        valueText.color = new Color(0f, UnityEngine.Random.Range(0.2f, 1f), 0);
        Vector2 randomizedPosition = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));
        var valueInstantiated = Instantiate(valueText, randomizedPosition, Quaternion.identity, gameCanvas.transform);
        Destroy(valueInstantiated.gameObject, 2);
    }
}
