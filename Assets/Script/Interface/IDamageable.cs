public interface IDamageable
{
    public float CurrentHealth { get; }
    public void TakeDamage(float amount,E_DamgeType type);
    public bool Dying{ get; } 
}
