using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOfItems : MonoBehaviour
{
    public static UseOfItems Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void Use(ItemSO item)
    {
        if (item.isHealing)
        {
            PlayerEffects.instance.HillEfect();
            Player.Instance.HpRecovery(item);
            Debug.Log("хилинг на " + item.healingPower);
        }
        if (item.isTool)
        {
            ChangeWeapon(item);
            UIActiveWeapon.Instance.ChangeIcone(item.icon);
        }


    }

    public void ChangeWeapon(ItemSO tool)
    {
        ActiveWeapon.Instance.SetActiveWeapon(tool.toolIndex);
    }
    


}
