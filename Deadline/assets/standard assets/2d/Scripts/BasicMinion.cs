using UnityEngine;
using System.Collections;

public class BasicMinion : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform frontPoint;
    private Transform frontBottomPoint;
    private Transform bottomPoint;
    private bool grounded;
    private SpriteRenderer sprite;

    public float moveSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();                     //Gets a reference of the rigid body to apply forces too
        frontPoint = transform.Find("Front Point").transform;   //Gets the transform component of the front point for checking if hitting walls.
        frontBottomPoint = transform.Find("Front Down Point").transform; //Gets the transfrom component of the front bottom point to avoid falling of ledges
        bottomPoint = transform.Find("Bottom Point").transform; //Gets the transform component for the point below the feat to determine if the sprite is on the ground
        sprite = GetComponent<SpriteRenderer>();
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

            // Set the enemy's velocity to moveSpeed in the x direction.
            body.velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
