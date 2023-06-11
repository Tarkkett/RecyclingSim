using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToolsManager : MonoBehaviour
{
    public Button[] allButtons = null;
    public ToolDatabase toolDatabase;
    public Movement movement;
    public GameObject menuPanel;
    public GameObject menuShowButton;
    
    private void Start()
    {
        
        allButtons = GetComponentsInChildren<Button>();
        menuPanel.SetActive(false);


        for (int i = 0; i < allButtons.Length; i++)
        {
            int buttonIndex = i;
            allButtons[i].onClick.AddListener(() => OnButtonClicked(buttonIndex));
            allButtons[i].interactable = false;
            if (allButtons[i].gameObject.name == "Menu") {
                menuShowButton.GetComponent<Button>().onClick.RemoveAllListeners();
                menuShowButton.GetComponent<Button>().onClick.AddListener(() => OnMenuClicked());
                menuShowButton.GetComponent<Button>().interactable = true;

            }

        }
    }

    private void OnMenuClicked()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        if (menuPanel.activeSelf == false) { menuPanel.SetActive(true); }
        else { menuPanel.SetActive(false); }

    }

    private void Update()
    {
        foreach (var button in allButtons)
        {
            TextMeshProUGUI[] textObjects = button.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var textObject in textObjects) { 
                if (textObject.name == "PriceTag") {
                    string numberString = new string(textObject.text.Where(char.IsDigit).ToArray());
                    int extractedNumber;
                    if (int.TryParse(numberString, out extractedNumber))
                    {
                        if (movement.garbageRemoved >= extractedNumber) { button.interactable = true;} else { button.interactable = false; }
                    }
                }
            }
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {

        AudioManager.instance.PlaySFX("ButtonClick");
        ToolClass individualTool = toolDatabase.GetTool(buttonIndex);
        movement.ChangeTool(individualTool);
        TextMeshProUGUI[] indicators = allButtons[buttonIndex].GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var indicator in indicators)
        {
            if (indicator != null && indicator.gameObject.name == "Buy") { indicator.text = "Equip"; }
            if (indicator != null && indicator.gameObject.name == "PriceTag") { Destroy(indicator.gameObject); }
        }
        
        

    }
    
}
