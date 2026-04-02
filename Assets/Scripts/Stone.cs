using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int hp = 3;
    public void TakeHit(Tool tool)
    {
        if (tool.Type != ToolType.Pickaxe)
        {
            Debug.Log("Инструмент не тот");
            return;
        }

        Debug.Log("взаимодействие произлшло ");
        hp -= tool.DamageAmount;
        ChecHp();
        Instantiate(_particleSystem, gameObject.transform.position, Quaternion.identity);
    }
    private void ChecHp()
    {
        if (hp <= 0)
        { 
        gameObject.SetActive(false);
        }
    }

}