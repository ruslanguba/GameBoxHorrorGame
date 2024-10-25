using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLetter : Item
{
    [SerializeField] private int _id;
    private LetterPanelController _letterPanelController => FindObjectOfType<LetterPanelController>();

    protected override void Collect()
    {
        _letterPanelController.ActivatePanel(_id);
    }
}
