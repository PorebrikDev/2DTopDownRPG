using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public float enemyHealth;
    public int enemyDamageAmount;
    public float damageRecoveryTime; //Задержка перед получением урона
    public float moveSpeed = 3.5f;
    public float acceleration = 8f;
}
