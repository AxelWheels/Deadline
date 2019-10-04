using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed = 0f;
    public float jumpSpeed = 0f;
    public int health = 3;
    public bool facingRight = true;

    private Vector3 position;
    private Rigidbody2D body;
    private Vector2 gravity;
    private Vector2 velocity;
    private Quaternion gunTip;

    private int coinCount;
    private bool doubleJump;
    private bool slam;
    private bool canJump;
    public GameObject bullet;
    // Use this for initialization
    void Start () 
    {
        coinCount = 0;
        canJump = true;
        doubleJump = false;
        slam = false;
        position = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        velocity = body.velocity;
        gunTip = new Quaternion(0,0,0,0);
    }
	
	// Update is called once per frame
	void Update () 
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 trns = transform.position;
        trns.z = -15f;
        camera.transform.position = trns;

        position = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        velocity = body.velocity;

        PlayerMovement();

        if (health == 0)
        {
            Destroy(body.gameObject);
        }
    }

    void FixedUpdate()
    {
       
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if (body.IsTouching(colObj.collider))
        {
            if (colObj.gameObject.tag == "Death")
            {
                Destroy(body.gameObject);
            }
            else if (colObj.gameObject.tag == "Floor")
            {
                if (slam)
                {
                    Collider2D[] slammedObjects = Physics2D.OverlapCircleAll(body.position, 3);
                    Rigidbody2D temp;
                    foreach (Collider2D obj in slammedObjects)
                    {
                        if (obj.GetComponent<Rigidbody2D>() != null)
                        {
                            temp = obj.GetComponent<Rigidbody2D>();

                            float forceX; 
                            if (body.position.x - temp.position.x > 0)
                            {
                                forceX = -3 - (temp.position.x - body.position.x);
                            }
                            else
                            {
                                forceX = 3 - (temp.position.x - body.position.x);
                            }

                            temp.AddForce(new Vector2(forceX, 2), ForceMode2D.Impulse);
                        } 
                    }

                    slam = false;
                }
                
                canJump = true;
            }
            else if (colObj.gameObject.tag == "Wall")
            {
                canJump = true;
            }
            else if(colObj.gameObject.tag == "Enemy")
            {
                health--;
            }
            else if (colObj.gameObject.tag == "Coin")
            {
                coinCount++;
            }
        }
        else
        {
            canJump = false;
        }
    }

    void OnCollisionStay2D(Collision2D colObj)
    {
        if (body.IsTouching(colObj.collider))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    void OnCollisionExit2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag == "Floor")
        {
            canJump = false;
        }
        else if (colObj.gameObject.tag == "Wall")
        {
            canJump = true;
        }
        else
        {

        }  
    }

    void PlayerMovement()
    {      
        float x = Input.GetAxis("Horizontal");
        velocity.x = x * speed;
        body.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (facingRight)
            {
                gunTip = Quaternion.Euler(0, 0, 90);
                Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), gunTip);
            }
            else
            {
                gunTip = Quaternion.Euler(0, 0, -90);
                Instantiate(bullet, this.transform.position + new Vector3(-1, 0, 0), gunTip);
            }

        }

        if (x < 0)
        {
            if (facingRight)
            {
                facingRight = false;

                // Multiply the x component of localScale by -1.
                Vector3 playerscale = transform.localScale;
                playerscale.x *= -1;
                transform.localScale = playerscale;
            }
        }
        else if (x > 0)
        {
            if (!facingRight)
            {
                facingRight = true;

                // Multiply the x component of localScale by -1.
                Vector3 playerscale = transform.localScale;
                playerscale.x *= -1;
                transform.localScale = playerscale;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(canJump)
            {
                velocity.y = 0;
                body.velocity = velocity;
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                canJump = false;
                slam = false;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                velocity.y = 0;
                body.velocity = velocity;
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                slam = false;
                doubleJump = false;
            }
        }
        else if (!canJump && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            velocity.y = 0;
            body.velocity = velocity;
            body.AddForce(new Vector2(0, -jumpSpeed * 4), ForceMode2D.Impulse);
            slam = true;
        }
    }
}
