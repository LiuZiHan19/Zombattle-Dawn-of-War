using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManagerUI : MonoBehaviour
{
    [SerializeField] private RectTransform unitSelectionAreaUI;

    private void Awake()
    {
        UnitSelectionManager.Instance.OnSelectionStart += OnSelectionStart;
        UnitSelectionManager.Instance.OnSelectionEnd += OnSelectionEnd;
    }

    private void OnSelectionEnd(object sender, EventArgs e)
    {
        
    }

    private void OnSelectionStart(object sender, EventArgs e)
    {
        
    }
}
