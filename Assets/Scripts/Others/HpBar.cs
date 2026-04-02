using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();

    }
    private void Start()
    {
        Player.Instance.OnChangeHp += UpdateHpBur;
        image.fillAmount = Player.Instance.MaxHealth;
    }
    public void UpdateHpBur(object sender, System.EventArgs e)
    {
        Debug.Log("υο ηΰψελ");
        image.fillAmount = Player.Instance.CurrentHealth / Player.Instance.MaxHealth;
    }
    private void OnDisable()
    {
        Player.Instance.OnChangeHp -= UpdateHpBur;
    }

}
