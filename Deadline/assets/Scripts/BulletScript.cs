using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public int bulletSpeed;
    private float bulletX;
    private Rigidbody2D bullet;
    PlayerScript player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        bullet = this.GetComponent<Rigidbody2D>();

        bulletX = bullet.position.x;

        if (player.facingRight)
        {
            bullet.velocity = transform.right * 5;
        }
        else
        {
            bullet.velocity = -transform.right * 5;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Mathf.Abs(bullet.position.x - bulletX) > 10)
        {
            Destroy(bullet.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            Destroy(colObj.gameObject);
            Destroy(this.gameObject);
        }
        else if(colObj.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
