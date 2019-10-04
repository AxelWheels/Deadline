using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    public float speed = 0f;
    public float jumpSpeed = 0f;

    private Vector3 position;
    private Rigidbody2D body;
    private Vector2 gravity;
    private Vector2 velocity;
	
	private bool doubleJump;
	private bool wallJump;

    private bool canJump;
    // Use this for initialization
    void Start () 
    {
        canJump = true;
        position = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        velocity = body.velocity;
    }
	
	// Update is called once per frame
	void Update () 
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        
        Vector3 trns = transform.position;
        trns.z = -15f;
        camera.transform.position = trns;
    }

    void FixedUpdate()
    {
        position = this.transform.position;
        body = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
        velocity = body.velocity;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
                velocity.x = -speed;
                body.velocity = velocity;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
                velocity.x = speed;
                body.velocity = velocity;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump)
            {
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            canJump = false;
            body.velocity = velocity;
        }
		
		if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            velocity.x = 0;
            body.velocity = velocity;
        }
		
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if(colObj.gameObject.tag == "Floor")
        {
            canJump = true;
        }
		else if (colObj.gameObject.tag == "Wall")
		{
           
			wallJump = true;
		}
    }

    void OnCollisionStay2D(Collision2D colObj)
    {
        canJump = true;
    }
}
