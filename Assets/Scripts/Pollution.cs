using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pollution : MonoBehaviour
{
    public MyQuality PollutionLevel;

    public static Pollution instance;

    public GameObject player;
    public Spawner spawner;

    public float tempTemperature;
    public float targetTemperature;
    public float currentTemperature = 0;
    public float maxTemperature = 80;
    public float t;
    public bool isAlive = true;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI startGameText;
    public float timeElapsed;
    

    public float startTime = 80;



    private void Awake()
    {

        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        
        gameOverText.enabled = false;
        startGameText.enabled = true;
    }

    private void StartGame()
    {
        print("Start!");
        Cursor.lockState = CursorLockMode.None;
        isAlive = true;
        startGameText.enabled = false;
        AudioManager.instance.musicSource.mute = false;
    }

    private void Update()
    {
        if (!isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (isAlive)
        {
            timeElapsed += Time.deltaTime;

            if (spawner.spawnedObjects.Count != 0)
            {

                float percent = ((float)spawner.spawnedObjects.Count / (float)spawner.maxObjects) * 100f;
                /*print("Max: " + spawner.maxObjects);
                print(spawner.spawnedObjects.Count);
                print(percent + "%");
                if (!isCounting) { StartCoroutine(changeValueToTarget(targetTemperature)); }*/
                targetTemperature = ((percent * maxTemperature) / 100f);
                float distance = targetTemperature - currentTemperature;
                if (distance < 0) { distance *= -1; }
                print("Dist: " + distance);
                t += Time.deltaTime / distance;
                tempTemperature = Mathf.Lerp(currentTemperature, targetTemperature, t);


                /*print(tempTemperature);
                print("Cur: "+ currentTemperature);
                print("Target: "+ targetTemperature);*/
                if (tempTemperature == targetTemperature)
                {
                    currentTemperature = targetTemperature;
                    t = 0;
                }
                if (tempTemperature >= maxTemperature)
                {
                    tempTemperature = maxTemperature;

                    if (isAlive) { gameOver(); }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        isAlive = true;
                    }
                }


            }

            SetAirQuality();
        }
        

    }

    private void gameOver()
    {
        AudioManager.instance.musicSource.mute = true;
        AudioManager.instance.PlaySFX("GameOverSound");
        
        
        gameOverText.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        isAlive = false;
        
        
        
        return;
    }

    private void SetAirQuality()
    {
        if (tempTemperature < maxTemperature/4)
        {
            PollutionLevel = MyQuality.Good;
        }
        else if (tempTemperature < maxTemperature/2 && tempTemperature > maxTemperature * 0.25f) { PollutionLevel = MyQuality.Medium; }
        else if (tempTemperature < maxTemperature* 0.75f && tempTemperature > maxTemperature / 2) { PollutionLevel = MyQuality.Bad; }
        else if (tempTemperature < maxTemperature && tempTemperature > maxTemperature * 0.75f) { PollutionLevel = MyQuality.Extreme; }
    }
    public enum MyQuality
    {
        Good,
        Medium,
        Bad,
        Extreme
    }
}
