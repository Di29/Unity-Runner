using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private int Score;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private int scoreCount = 5;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private int fallPos = -5;
    

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(0,0,speed*Time.deltaTime);

        if(Input.GetKey("a")){
            //move left
            rb.AddForce(-turnSpeed*Time.deltaTime,0,0, ForceMode.Impulse);
        }
        else if(Input.GetKey("d"))
        {
            //move right
            rb.AddForce(turnSpeed*Time.deltaTime,0,0, ForceMode.Impulse);
        }
        else if(Input.GetKey("w"))
        {
            //move forward
            //rb.AddForce(turnSpeed*Time.deltaTime,0,0, ForceMode.Impulse);
            rb.AddForce(0,0,speed*Time.deltaTime);
        }
        else if(Input.GetKey("s"))
        {
            //stop, move back
            rb.AddForce(0,0, -speed*Time.deltaTime);
        }
        else if(isGrounded)
        {
            if(Input.GetKey("space"))
            {
                //jump
                rb.AddForce(Vector3.up*jumpHeight);
            }
        }

        if(Score>scoreCount){
                speed += scoreCount*Time.deltaTime;
            }
        
        if(gameObject.transform.position.y <= fallPos){
            transform.position = startPos;
        }
    }


    private void OnCollisionEnter(Collision other) {
        //выполняем проверку по тэгу объекта
        if(other.gameObject.tag == "Obstacle"){
            //game over
            this.enabled = false;   
        }
        if(other.gameObject.tag == "Ground"){
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Ground"){
            isGrounded = false;
        }
    }

    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Coin"){
            //увеличить счет
            Score++;
            Debug.Log("Score " + Score);
            //уничтожить объект
            Destroy(other.gameObject);
        }
        
        
    }

    
}
