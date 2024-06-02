
public interface IDamageable
{
    int Health { get; set; }
    bool Death { get; set; }

    void Damage();

}
