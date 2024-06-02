using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private Animator _swordAnimation;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        _anim.SetBool("Jumping", jump);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }

    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
