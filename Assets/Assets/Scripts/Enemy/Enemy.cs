using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    public GameObject diamondPrefabs;

    public Vector3 currentTarget;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected bool isHit = false;
    protected bool isDead = false;
    protected Player player;

    public virtual void Init()
    {
        animator =GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        currentTarget = pointB.position;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("InCombat") == false)
        {
            return;
        }

        if (!isDead)
            Movement();
        if (isDead)
            player.inAttackDistance = false;

    }



    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }

        if(!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0f || player.Death)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
            player.inAttackDistance = false;
        }
        else
        {
            player.inAttackDistance = true;
        }

        Vector3 direction = player.transform.localPosition - transform.position;

        if(direction.x > 0 && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = false;
        }else if (direction.x < 0 && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = true;
        }
    }
}
