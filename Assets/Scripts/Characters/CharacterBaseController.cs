using UnityEngine;

public class CharacterBaseController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public CharacterStats stats;

    public float direction;
    public bool isGrounded;
    public bool isRunning;
    public bool jumpRequested;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
    }
}