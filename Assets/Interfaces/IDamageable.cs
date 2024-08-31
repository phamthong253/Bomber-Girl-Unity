using UnityEngine;

public interface IDamageable
{
    public int Health { set; get; }
    public bool Targetable { set; get; }
    public bool Invicible { set; get; }
    public void OnHit(int damage, Vector2 knockback);
    public void OnHit(int damage);
    public void OnObjectDestroyed();
}