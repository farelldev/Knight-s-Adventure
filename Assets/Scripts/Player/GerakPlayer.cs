using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GerakPlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPower;

    [Header("Sound")]
    [SerializeField] private AudioClip running;
    [SerializeField] private AudioClip jumping;
    [SerializeField] private AudioClip landing;
    [SerializeField] private AudioClip dashing;

    [Header("Dash")]
    [SerializeField] private TrailRenderer trainRenderer;
    [SerializeField] private Rigidbody2D Rigidbody2D;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    
    private void Awake()
    {   
        //Ngambil referensi dari objek game
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
 
    private void Update()
    {
        if (isDashing)
            return;

        //Jalan
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
 
        //Balik player kanan/kiri
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && grounded)
        {
            anim.SetTrigger("dash");
            StartCoroutine(Dash());
            SoundManager.instance.PlaySound(dashing);
        }
             
        //Loncat
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && grounded)
            Jump();
 
        //Ngatur animasi
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void FixedUpdate()
    {
        if (isDashing)
            return;
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        grounded = false;
        SoundManager.instance.PlaySound(jumping);
        anim.SetTrigger("jump");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
        SoundManager.instance.PlaySound(landing);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }

    private void Fall()
    {
        anim.SetTrigger("fall");
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = Rigidbody2D.gravityScale;
        Rigidbody2D.gravityScale = 0f;
        Rigidbody2D.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trainRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trainRenderer.emitting = false;
        Rigidbody2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}