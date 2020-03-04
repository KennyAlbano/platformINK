using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKoolScript : MonoBehaviour
{
    //Drag and drop the kool kids rigidBody2D component onto koolRigidBody
    //serializeField makes components accesible and modifiable in inspector and they can still remain private
    [SerializeField] private Rigidbody2D koolRigidBody;

    [SerializeField] private GameObject cameraFollowingKoolKid;

    [SerializeField] private GameObject koolKid;

    [SerializeField] private CircleCollider2D koolKidCollider;

    [SerializeField] private Camera cammy;

    private bool isGrounded = false;

    [SerializeField] private GameObject badGuy;

    private static Vector2 horzMovementSpeed = new Vector2(10f, 0f);
    private Vector2 reverseHorzMovementDirection = horzMovementSpeed * new Vector2(-1f,0f);
    private Vector2 jump = new Vector2(0f, 350f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //move The Kool Kid left and right
        //if press right button : move Kool Kid right
        //if press left button : move Kool Kid left
        //insted of directly setting Kool kid's position or speed set (relative velocity)***
        //total velocity vector (X,Y) = sum of horizontal velocities (sum X) , sum of vertical velocities (sum Y)
        //gravity = negative constant vertical acceleration or should it be more simply programmed as negtive vertical velocity? (-delta X) or (-X)
        //need variable totalVelocityVector of type Vector2(totalHoizontalVelocityVector, totalVerticalVelocityVector)
        //need variable totalHorizontalVelocityVector of type float (sum of all horizontalVelocityVectors)
        //need variable totalVerticalVelocityVector of type float (sum of gravity and all other verticalVelocityVectors)
        if(koolKid.transform.position.y <= -60)
        {
            koolKid.transform.position = new Vector3(0, 0, 0);
            cameraFollowingKoolKid.transform.position = new Vector3(0, 0, -10);
            cammy.orthographicSize = 10;
        }

    }
    void FixedUpdate()
    {
        //if press right 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            koolRigidBody.AddForce(horzMovementSpeed);
        }
        //if press left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            koolRigidBody.AddForce(reverseHorzMovementDirection);
        }
        //need isgrounded function and isGrounded variable to detect if the player is directly above and touching a solid surface. 
        //Might also include a double jump feature which checks if player isGrounded OR if # of jumps since isGrounded was true is less than or equal to 1
        //each successful jump action will increment the # of jumps counter by 1 until isGrounded is true again
        //if press up (jump)
        if((Input.GetKeyDown(KeyCode.UpArrow)) && (isGrounded == true))
        {
            koolRigidBody.AddForce(jump);
        }
        //camera should only follow the players x position when in the air but when grounded should follow x player's x and y position
        //if (isGrounded == true)
        //{
        //    cameraFollowingKoolKid.transform.position = koolKid.transform.position + new Vector3(0f, -1f, -10f);
        //}
        //if (isGrounded == false)
        //{
        cameraFollowingKoolKid.transform.position = new Vector3(koolKid.transform.position.x, koolKid.transform.position.y, -10f);
        //}
        if (koolKid.transform.position.y <= -20f)
        {
            cammy.orthographicSize = 10+koolKid.transform.position.y;
        }
    }
    //determines if koolKid is touching any solid ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == badGuy)
        {
            koolKid.transform.position = new Vector3(0, 0, 0);
        }
    }
}
