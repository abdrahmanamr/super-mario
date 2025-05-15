using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    public float chaseRange = 20f;
    public float walkSpeed = 2f;

    private enum State { Walking, Chasing, Shelled, Pushed, Dead }
    private State currentState = State.Walking;

    private Player targetPlayer;
    private EntityMovement entityMovement;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AnimatedSprite animatedSprite;
    private DeathAnimation deathAnimation;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            targetPlayer = playerObject.GetComponent<Player>();
        }

        entityMovement = GetComponent<EntityMovement>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatedSprite = GetComponent<AnimatedSprite>();
        deathAnimation = GetComponent<DeathAnimation>();
    }

    private void Update()
    {
        if (currentState == State.Dead) return;

        switch (currentState)
        {
            case State.Walking:
                entityMovement.direction = Vector2.left;
                entityMovement.speed = walkSpeed;
                if (targetPlayer != null && Vector2.Distance(transform.position, targetPlayer.transform.position) <= chaseRange)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                entityMovement.speed = 4f; 
                if (targetPlayer != null)
                {
                    float directionX = targetPlayer.transform.position.x - transform.position.x;
                    if (directionX > 0)
                    {
                        entityMovement.direction = Vector2.right;
                    }
                    else if (directionX < 0)
                    {
                        entityMovement.direction = Vector2.left;
                    }
                }
                break;


            case State.Shelled:
                entityMovement.enabled = false;
                break;

            case State.Pushed:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState == State.Dead) return;

        if (currentState != State.Shelled && collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.starpower)
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentState == State.Dead) return;

        if (currentState == State.Shelled && other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (currentState != State.Pushed)
            {
                Vector2 direction = new(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                if (player.starpower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else if (currentState != State.Shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        currentState = State.Shelled;
        spriteRenderer.sprite = shellSprite;
        animatedSprite.enabled = false;
        entityMovement.enabled = false;
    }

    private void PushShell(Vector2 direction)
    {
        currentState = State.Pushed;
        rb.isKinematic = false;
        entityMovement.enabled = true;
        entityMovement.direction = direction.normalized;
        entityMovement.speed = shellSpeed;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        currentState = State.Dead;
        animatedSprite.enabled = false;
        entityMovement.enabled = false;
        deathAnimation.enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (currentState == State.Pushed)
        {
            Destroy(gameObject);
        }
    }
}
