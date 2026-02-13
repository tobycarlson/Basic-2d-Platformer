using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{


    float damage = 1f;
    float speed = 20f;
    float direction = -1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * direction * Time.deltaTime, 0,0);
    }

    void SelfDirection(float dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Spike spike = collision.GetComponent<Spike>();



            if (spike != null)
            {
                Debug.Log("Bullet hit spike");
                spike.destroyed();
                Destroy(gameObject);
            }
        }
    }
}
