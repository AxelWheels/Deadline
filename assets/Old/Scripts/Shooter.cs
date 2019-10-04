using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public float detectDistance;
    public float bulletSpeed;
    public float shotReload;
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform gunTip;

    private bool facingLeft;
    private float nextShot = 0.0f;
   

	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	void Update ()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < detectDistance //Checks if the player is close enough to be noticed
        &&  Time.time > nextShot) 
        {
            nextShot = Time.time + shotReload;
            if(player.transform.position.x < transform.position.x)//The player is to the left
            {
                if (!facingLeft)
                { Flip(); }
                GameObject bullet = Instantiate(bulletPrefab, gunTip.position,gunTip.rotation) as GameObject;
            }
            else //The player must be to the right
            {
                if(facingLeft)
                { Flip(); }
                GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation) as GameObject;     
                
            }
        }
	}
    Vector2 getVector()//Calculates the normalised vector from the minion towards to the player
    {
        return (player.transform.position - transform.position).normalized;
    }

    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
        gunTip.Rotate(0, 0, 180);
        if(facingLeft)
        { facingLeft = false; }
        else
        { facingLeft = true; }
    }
}
