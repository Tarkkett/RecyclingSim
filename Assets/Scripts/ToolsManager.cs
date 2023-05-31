using System;
using System.Collections;
using System.Collections.Generic;
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
            if (allButtons[i].gameObject.name == "Menu") {
                menuShowButton.GetComponent<Button>().onClick.RemoveAllListeners();
                menuShowButton.GetComponent<Button>().onClick.AddListener(() => OnMenuClicked());

            }

        }
    }

    private void OnMenuClicked()
    {
        if (menuPanel.activeSelf == false) { menuPanel.SetActive(true); }
        else { menuPanel.SetActive(false); }

    }

    private void Update()
    {
        //if (allButtons != null) {}
        
    }

    private void OnButtonClicked(int buttonIndex)
    {
        ToolClass individualTool = toolDatabase.GetTool(buttonIndex);
        movement.ChangeTool(individualTool);

    }
    
}
