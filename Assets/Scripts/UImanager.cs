using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class UIManager : MonoBehaviour
{
    private int score;
    private int energy;
    private int intedPollution;

    public float maxCloudRate = 50;
    public Movement movement;
    public Pollution pollutionClass;
    public TextMeshProUGUI garbageRemovedText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI pollutionText;
    public VisualEffect fogEffect;

    private void Start()
    {
        fogEffect.SetFloat("Fog Amount", 0f);
        score = 0;
    }
    private void Update()
    {
        if (score < 10)
        {
            garbageRemovedText.text = "0000" + score.ToString();
        }
        else if (score < 100) { garbageRemovedText.text = "0000" + score.ToString(); }
        else if (score < 1000) { garbageRemovedText.text = "000" + score.ToString(); }
        else if (score < 10000) { garbageRemovedText.text = "00" + score.ToString(); }
        else if (score < 100000) { garbageRemovedText.text = "0" + score.ToString(); }
        else if (score < 1000000) { garbageRemovedText.text = "" + score.ToString(); }
        else if (score < 10000000) { garbageRemovedText.text = (score/1000000).ToString() +"M"; }
        score = movement.garbageRemoved;
        energy = movement.energyStored;
        energyText.text = energy.ToString() + "%";
        intedPollution = (int)pollutionClass.tempTemperature;
        pollutionText.text = intedPollution.ToString();

        



        if (fogEffect != null && pollutionClass.PollutionLevel == Pollution.MyQuality.Extreme) {
            fogEffect.SetFloat("Fog Amount", maxCloudRate);
            fogEffect.SetVector4("Fog Color", new Vector4(0.15f,0.15f,0.15f,1));

        }
        else if(fogEffect != null && pollutionClass.PollutionLevel == Pollution.MyQuality.Bad)
        {
            fogEffect.SetFloat("Fog Amount", maxCloudRate * 0.75f);
            fogEffect.SetVector4("Fog Color", new Vector4(0.5f, 0.5f, 0.5f, 1));

        }
        else if (fogEffect != null && pollutionClass.PollutionLevel == Pollution.MyQuality.Medium)
        {
            fogEffect.SetFloat("Fog Amount", maxCloudRate * 0.5f);
            fogEffect.SetVector4("Fog Color", new Vector4(0.75f, 0.75f, 0.75f, 1));

        }
        else if (fogEffect != null && pollutionClass.PollutionLevel == Pollution.MyQuality.Good)
        {
            
            fogEffect.SetFloat("Fog Amount", maxCloudRate * 0.25f);
            fogEffect.SetVector4("Fog Color", new Vector4(1, 1, 1, 1));

        }
        else { print("No FogMap"); }
        
        
        
    }
}
