using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    
    //  MOVE VARS

    private float moveInput;
    private float jumpInput;
    private float speedMultiplier = 10f;
    public float jumpMultiplier = 5f;
    private bool isGrounded;


    public int Health = 10;

    // BULLET VARS

    public float cooldown = 0.5f;
    float defaultCooldown = 0.5f;
    public GameObject original_bullet;

    Vector3 offset = new Vector3(0, 0, 0);
    float bullet_direction = 1;

    public int coinCount;

    GameManager gm;

   void Start()
    {
        gm= FindFirstObjectByType<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        coinCount = 0;
    }




    public void OnMove(InputAction.CallbackContext context)
    {
       //Debug.Log("Moved");
       moveInput = context.ReadValue<float>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            //Debug.Log("Jumped");
            jumpInput = context.ReadValue<float>();
            rigidbody.AddForce(new Vector2(0f, jumpInput * jumpMultiplier), ForceMode2D.Impulse);
        }
            
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {


        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (cooldown <= 0 && Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            GameObject bullet = Instantiate(original_bullet, transform.position + offset, Quaternion.identity);
            cooldown = defaultCooldown;
        }

    }

    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(moveInput * speedMultiplier, 0f), ForceMode2D.Force);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }


    public void changeCoinCount(int amount)
    {
        coinCount += amount;
        if (gm != null)
        {
            gm.coinCountChanged(coinCount);
        }
     else
        {
            Debug.LogWarning("GameManager not found when updating coin count");
        }
    }

    public void changeHealth(int amount)
    {
        Debug.Log("PlayerController.cs: player health changed");
        Health += amount;
        if (gm != null)
        {
            gm.healthChanged(Health);
        }
    }

}


