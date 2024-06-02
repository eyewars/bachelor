using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InteractableHoverUi : MonoBehaviour
{
    [SerializeField, Tooltip("What interactor's hover text should this component show.")] 
    private interactor interactor;
    
    /// <summary>
    /// The text component to display the hover text with.
    /// </summary>
    private TextMeshProUGUI _textComponent;

    private void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textComponent.text = interactor.HoverText;
    }
}
