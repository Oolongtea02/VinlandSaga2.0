using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    float xspeed;
    float yspeed;
    Rigidbody2D playerRB;
    RaycastHit2D hit;
    RaycastHit2D enemyhit;
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnMove (InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        xspeed = movementVector.x;
        yspeed = movementVector.y;
    }
    void OnJump (InputValue movementValue){
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -1), Mathf.Infinity);
        if(hit.distance < 1.0f){
            playerRB.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        }
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector3.right, Color.green, 5.0f);
    }
    void OnFire (InputValue fireValue){
        enemyhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(1, 0), Mathf.Infinity);
        if(enemyhit.distance < 3.0f){
            if(enemyhit.rigidbody.tag == "Killable"){
                Destroy(enemyhit.rigidbody.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(xspeed, yspeed) * Time.deltaTime* 6);
        
    }
}
