using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _CloseButton;
    [SerializeField] private Button _PlayButton;
    private void Awake()
    {
        gameObject.SetActive(false);
        _PlayButton.onClick.AddListener(TapPlay);
        _CloseButton.onClick.AddListener(CloseGame);
        GameInput.Instance.OnInteractionMenuStarted += Activate;
    }
    private void TapPlay()
    {
        gameObject.SetActive(false);
        GameInput.Instance.IsPaused = false;
    }
    private void CloseGame()
    { Application.Quit(); }
    
    private void Activate(object sender, System.EventArgs e)
    {
        if (GameInput.Instance.IsPaused)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
