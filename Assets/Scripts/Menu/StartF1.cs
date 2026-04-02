using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartF1 : MonoBehaviour
{
    [SerializeField] private int timeDelay = 4;

    private void Start()
    {
        StartCoroutine(PopUpWindow());

    }
    private IEnumerator PopUpWindow()
    {
        yield return new WaitForSeconds(timeDelay);
        gameObject.SetActive(false);

    }
}
