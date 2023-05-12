
using UnityEngine;

public class Hero : Unit
{
 
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource runSound;
    private bool isGrounded = false;
    private Bullet bullet;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public Joystick joystick;
    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        runSound.Play();
        if (Instance == null) { Instance = this; }
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren <SpriteRenderer>();
        anim = GetComponent<Animator>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        
        CheckGrounded();
    }
    private void Update()
    {
        
        if (isGrounded) State = States.idle;
       // if (Input.GetButtonDown("Fire1")) Shoot();
        if (joystick.Horizontal != 0)
        {
            
            Run();
        }
        if (joystick.Vertical > 0.5f && isGrounded)
        {
             Jump();
           
        }
    }

    private void Run()
    {
        if (isGrounded)
        {
            
            State = States.run;
            
        }
        
        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed *Time.deltaTime);
        sprite.flipX = dir.x < 0;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpforce;
        jumpSound.Play();
        //rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse); 
        
    }
    public void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.7F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1f : 1f);
    }

    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded) State = States.jump;
    }
    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }


}


public enum States
{
    idle, run, jump
}