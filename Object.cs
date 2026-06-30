using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Object : MonoBehaviour
{  
    // ==========Attributes==========
    #region Attributes 

    // Object Properties
    [Header("Object Properties")]
    public bool Usable = false;

    // Object Information
    [Header("Object Information")]
    public string objectName;
    [TextArea(3, 10)]
    public string description;

    // Object Use (If Usable)
    [Header("Object Use (If Usable)")]
    [TextArea(3, 10)]
    public string prompt;

    //Choices
    public TMP_Text promptTextUI;
    public GameObject choicePrefab;
    public GameObject choiceContainer;
    public List<Choice> choices = new List<Choice>();

    #endregion

    // ==========Method==========
    #region Methods

    // Interact with the object
    public void Interact()
    {
        Debug.Log("This is a " + objectName + ". " + description);
        
        if (Usable) Use();
    }

    // Use the object
    public void Use()
    {
        Debug.Log("Using the " + objectName);
        SetPromptText(prompt);
        ViewChoices();
    }
    
    // View available choices for the object
    public void ViewChoices()
    {
        foreach (Choice choice in choices)
        {
            GameObject instantiatedChoice = Instantiate(choicePrefab, choiceContainer.transform);
            instantiatedChoice.GetComponentInChildren<TMP_Text>().text = choice.text;
            instantiatedChoice.GetComponent<Button>().onClick.AddListener(() => ActivateChoice(choice));
        }
    }

    // Activate a choice by index
    public void ActivateChoice(Choice choice)
    {
        choice.action.Invoke();
        ClearChoices();
        ClearPromptText();
    }

    //Start
    void Start()
    {
        ClearChoices();
        ClearPromptText();
    }

    // ----------Simple Methods----------

    // Setters-----
    public void SetPromptText(string text)
    {
        promptTextUI.text = text;
    }

    // Clearers-----
    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearPromptText()
    {
        SetPromptText("");
    }

    public void ClearObject()
    {
        Destroy(gameObject);
    }

    // DebugLog-----
    public void DebugLog(string text)
    {
        Debug.Log(text);
    }

    #endregion
}

#region Choice Class
[System.Serializable]
public class Choice
{
    public string text;
    public UnityEvent action;
}
#endregion