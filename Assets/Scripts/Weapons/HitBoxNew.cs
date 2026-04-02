using UnityEngine;

public class HitBoxNew : MonoBehaviour
{
    [SerializeField] private float _hitboxOffset = 1f;

    private Tool _tool;
    private PolygonCollider2D _PolCol;

    private void Awake()
    {
        _tool = GetComponentInParent<Tool>();
        _PolCol = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _PolCol.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeHit(_tool);
        }
    }

    public void HitBoxOn()
    {
        _PolCol.enabled = true;

        Vector2 dir = Player.Instance.LastMoveDirection;

        transform.localPosition = new Vector3(
            dir.x * _hitboxOffset,
            dir.y * _hitboxOffset,
            0f);
    }

    public void HitBoxOff()
    {
        _PolCol.enabled = false;
    }
}