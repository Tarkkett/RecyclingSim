using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GarbageRemoved : MonoBehaviour
{
    
    private bool coroutineRunning = false;
    private float startTime;
    private float duration = 3f;
    private float currentScale = 0.3f;
    private float targetScale = 0f;

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
                FindObjectOfType<Movement>().garbageRemoved++;
                yield break;
            }
            yield return null;
        }
        currentScale = 0f;
        coroutineRunning = false;
        yield return null;
    }

}
