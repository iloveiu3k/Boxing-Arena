public interface IDamageable
{
    public float CurrentHealth { get; }
    public void TakeDamage(float amount);
}
