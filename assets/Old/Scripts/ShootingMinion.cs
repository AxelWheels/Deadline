using UnityEngine;
using System.Collections;

public class ShootingMinion : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform frontPoint;
    private Transform frontBottomPoint;
    private Transform bottomPoint;
    private bool grounded;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool facingLeft;
    private float nextShot = 0.0f;
    private GameObject player;

    public float moveSpeed;
    public float detectDistance;
    public float bulletSpeed;
    public float shotReload;
    public GameObject bulletPrefab;
    public Transform gunTip;

    void Start()
    {
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();                     //Gets a reference of the rigid body to apply forces too
        frontPoint = transform.Find("Front Point").transform;   //Gets the transform component of the front point for checking if hitting walls.
        frontBottomPoint = transform.Find("Front Down Point").transform; //Gets the transfrom component of the front bottom point to avoid falling of ledges
        bottomPoint = transform.Find("Bottom Point").transform; //Gets the transform component for the point below the feat to determine if the sprite is on the ground
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Create an array of all collisions for the point below the feat
        Collider2D[] bottomHits = Physics2D.OverlapPointAll(bottomPoint.position);

        //See if any collisions were with level elements
        int collisions = 0;
        foreach (Collider2D collider in bottomHits)
        {
            if (collider.tag == "Obstacle")
            { collisions++; }
        }

        if (collisions == 0)
        {
            //No ground detected
            grounded = false;
        }
        else
        {
            //Ground detected
            grounded = true;

            if (body.velocity.y < -4)
            {
                //Apply a bounce
                float newYVel = -body.velocity.y / 4;
                body.velocity = new Vector2(body.velocity.x, newYVel);

                //Todo start landing animation; Slightly squash the sprite

            }
        }

        if (grounded)
        {

            #region Wall turn
            //Create an array of the colliders the front point it over
            Collider2D[] frontHits = Physics2D.OverlapPointAll(frontPoint.position);

            //Check if any of the colliders are for an Obstacle (Tag)
            foreach (Collider2D collider in frontHits)
            {
                if (collider.tag == "Obstacle")
                {
                    Flip();
                    break;
                }
            }
            #endregion
            #region Ledge Turn
            //Create an array of the colliders the front bottom point are over
            Collider2D[] frontDownHits = Physics2D.OverlapPointAll(frontBottomPoint.position);

            collisions = 0;
            //Check if any of the colliders are for an Obstacle (Tag)
            foreach (Collider2D collider in frontDownHits)
            {
                if (collider.tag == "Obstacle")
                { collisions++; }
            }
            if (collisions == 0)
            {
                Flip();
            }
            #endregion
            #region Player Detection
            if ( player != null && Vector2.Distance(transform.position, player.transform.position) < detectDistance //Checks if the player is close enough to be noticed
            &&  Time.time > nextShot)
            {
                nextShot = Time.time + shotReload;
                if (player.transform.position.x < transform.position.x)//The player is to the left
                {
                    if (!facingLeft)
                    { Flip(); }
                    GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation) as GameObject;
                }
                else //The player must be to the right
                {
                    if (facingLeft)
                    { Flip(); }
                    GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation) as GameObject;

                }
            }
            #endregion
            // Set the enemy's velocity to moveSpeed in the x direction.
            body.velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetTrigger("Walk");
        }
    }
    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
        gunTip.Rotate(0, 0, 180);
        if (facingLeft)
        { facingLeft = false; }
        else
        { facingLeft = true; }
        // Set the enemy's velocity to moveSpeed in the x direction.
        body.velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
