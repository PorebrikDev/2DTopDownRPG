using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActiveWeapon : MonoBehaviour
{
    public static UIActiveWeapon Instance;
    [SerializeField] private Image image;
    [SerializeField] private Sprite imageZero;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();

    }

    public void ChangeIcone(Sprite i)
    { 
    image.sprite = i;
    }
    public void ZeroIcone()
    {
        image.sprite = imageZero;
    }
}
