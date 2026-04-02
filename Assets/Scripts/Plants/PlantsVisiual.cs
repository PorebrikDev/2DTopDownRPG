using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsVisiual : MonoBehaviour
{
    [SerializeField] private Bush _destroyPlants;
    [SerializeField] private GameObject _particle;

    private void Start()
    {

        _destroyPlants.OnTakeDamage += DestroyPlants_OnTakeDamage;
    }
    private void ShowParticleBoom()
    {
        Instantiate(_particle, _destroyPlants.transform.position, Quaternion.identity, null);
    }
    private void DestroyPlants_OnTakeDamage(object sender, System.EventArgs e)
    {
        ShowParticleBoom();
    }

    private void OnDestroy()
    {

        _destroyPlants.OnTakeDamage -= DestroyPlants_OnTakeDamage;
    }

}
