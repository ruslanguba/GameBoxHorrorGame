using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelConroller : MonoBehaviour
{
    [SerializeField] private PlayerDeathHandler _playerDeathHandler;

    private string _pauseText = "PAUSED";
    private string _deathText = "YOU ARE DEAD";

    [SerializeField] private Text _panelText;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _continueButton;

    private bool _isPaused;

    private void Start()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        _playerDeathHandler.OnPlayerDead += ActivatePanel;
    }

    private void OnDisable()
    {
        _playerDeathHandler.OnPlayerDead -= ActivatePanel;
    }

    public void DeactivatePanel()
    {
        _panel.SetActive(false);
    }

    public void ActivatePanel(bool isPaused)
    {
        _panel.SetActive(isPaused);
        if (_isPaused)
        {
            _continueButton.SetActive(true);
            _panelText.text = _pauseText;
        }
        else
        {
            _panelText.text = _deathText;
            _continueButton.SetActive(false);
        }
    }
}
