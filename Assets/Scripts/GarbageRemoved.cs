using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GarbageRemoved : MonoBehaviour
{
    
    private bool coroutineRunning = false;
    private float startTime;
    private float duration = 3f;
    private float currentScale = 0.3f;
    private float targetScale = 0f;
    [SerializeField]private int garbageValue = 0;
    public TextMeshProUGUI valueText;
    private Spawner spawner;
    Canvas[] allWorldCanvases;
    Canvas gameCanvas;

    private void Start()
    {
        garbageValue = 0;
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
        AudioManager.instance.PlaySFX("FallSound");
        coroutineRunning = true;
        startTime = Time.time;
        for (int i = 0; i < spawner.spawnedObjects.Count; i++)
        {
            if (spawner.spawnedObjects[i].name == gameObject.name)
            {
                switch (spawner.spawnedObjects[i].state)
                {
                    case myState.Trash:
                        garbageValue += spawner.spawnedObjects[i].value;
                        spawner.SpawnEntity(2, myState.Trash);
                        
                        break;
                    case myState.Recycled:
                        garbageValue += spawner.spawnedObjects[i].value;
                        break;
                    default:
                        break;
                }
                
                break;
            }
        }

        while (currentScale > targetScale) {
            var currentTime = (Time.time - startTime) / duration;

            currentScale = Mathf.Lerp(currentScale, targetScale, currentTime);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale <= 0.1f)
            {
                Destroy(gameObject);
                
                
                FindObjectOfType<Movement>().garbageRemoved += garbageValue;
                
                spawner.spawnedObjects.Remove(GetClassFromPrefab(gameObject));
                UpdateText();
                

                yield break;
            }
            yield return null;
        }
        currentScale = 0f;
        coroutineRunning = false;
        yield return null;
    }

    public GarbageClass GetClassFromPrefab(GameObject obj)
    {
        GarbageClass gClass = null;
        for (int i = 0; i < spawner.spawnedObjects.Count; i++)
        {
            if (spawner.spawnedObjects[i].name == obj.name)
            {
                gClass = spawner.spawnedObjects[i];
                return gClass;
            }
        }
        return null;
    }

    private void UpdateText()
    {
        valueText.text = garbageValue.ToString();
        valueText.color = new Color(0f, UnityEngine.Random.Range(0.2f, 1f), 0);
        Vector2 randomizedPosition = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));
        var valueInstantiated = Instantiate(valueText.gameObject, randomizedPosition, Quaternion.identity, gameCanvas.transform);
        Destroy(valueInstantiated.gameObject, 2);
    }
}
