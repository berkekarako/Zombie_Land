namespace Sc.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; set; }
        void TakeDamage(float damageCount);
        void Die();
    }
}