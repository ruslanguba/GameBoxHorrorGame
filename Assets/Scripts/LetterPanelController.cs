using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _letterPanel;
    [SerializeField] private List<Text> _texts;
    
    public void ActivatePanel(int index)
    {
        _letterPanel?.SetActive(true);
        foreach (var item in _texts)
        {
            item.gameObject.SetActive(false);
        }
        if (index < _texts.Count) 
        {
            _texts[index].gameObject.SetActive(true);
        }
    }

    public void OnCloseButtonClick()
    {
        _letterPanel?.SetActive(false);
    }
}
