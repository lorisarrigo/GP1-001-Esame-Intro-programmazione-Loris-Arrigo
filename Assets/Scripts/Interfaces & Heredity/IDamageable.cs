public interface IDamageable
{
    //interface used to manage the damage

    //Function used to deal Damage to the hittables (Enemy and Player),
    //based on the Damage comunicated by the Attackers (Bullets and Enemy)
    public void TakeDamage(float dmg);

    //Function used to Destroy the hittables when the Attackers finds out that the health is finished
    public void Despawn();
}
