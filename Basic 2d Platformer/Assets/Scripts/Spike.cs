using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float speed = 0.25f;

    public Vector3 move_dir;
    private float t = 0f;

    GameManager gm;

    PlayerController player;
    
    public float damageCooldown = 0.5f;
    private float nextDamageTime = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       move_dir = Vector3.left;
       gm = FindFirstObjectByType<GameManager>();
       gm.addEnemy();
       player = FindFirstObjectByType<PlayerController>();
    }



    // Update is called once per frame
    void Update()
    {
    t = Mathf.PingPong(Time.time * speed, 1f);
    transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time < nextDamageTime) return; // Still in cooldown, do nothing


        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.changeHealth(-1);
                nextDamageTime = Time.time + damageCooldown; // Set next damage time
                Debug.Log("Spike.cs: Player health changed");
            }
        }
    }

    public void destroyed()
    {
        Destroy(gameObject);
    }
    
}