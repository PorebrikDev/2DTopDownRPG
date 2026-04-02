using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_f1 : MonoBehaviour
{

    private void Awake()
    {

        GameInput.Instance.OnInteractionControlF1Started += OpenClose;
        gameObject.SetActive(false);
    }

    public void OpenClose(object sender, System.EventArgs e)
    { 
    gameObject.SetActive(!gameObject.activeSelf);
    }
}
