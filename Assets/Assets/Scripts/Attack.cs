using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable hit = collision.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (_canDamage && !hit.Death)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
        }
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
