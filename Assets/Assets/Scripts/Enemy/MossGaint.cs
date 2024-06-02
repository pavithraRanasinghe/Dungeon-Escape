

using System.Security.Cryptography;
using UnityEngine;

public class MossGaint : Enemy, IDamageable
{
    public int Health { get; set; }
    public bool Death { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        Health--;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);

        if(Health < 1) 
        {
            animator.SetTrigger("Death");
            isDead = true;
            Death = true;
            GameObject diamond =  Instantiate(diamondPrefabs, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
