using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallScript : MonoBehaviour
{

    private Rigidbody lightBallRigid;
    private GameObject player;
    private bool returnLight;
    private Vector3 startPos;
    private Vector3 startPos2;
    private Vector3 startPos3;
    private int isShooting = 0;
    private bool cloneMade;
    private Vector3 newRot;
    private GameObject ball2;
    private GameObject ball3;


    public float maxDistance;
    public float minDistance;
    public float flyOutSpeed;
    public float recallSpeed;
    public Light light1;

    void Start()
    {
        lightBallRigid = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        ball2 = GameObject.FindGameObjectWithTag("LightBall");
        ball3 = GameObject.FindGameObjectWithTag("LightBall2");
        ball2.GetComponent<Light>().enabled = false;
        ball3.GetComponent<Light>().enabled = false;
        startPos = transform.localPosition;
        startPos2 = ball2.transform.localPosition;
        startPos3 = ball3.transform.localPosition;
        light1 = gameObject.GetComponent<Light>();
        light1.range = 5f;
        lightBallRigid.Sleep();
        gameObject.GetComponent<SphereCollider>().enabled = false;
        //     gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(gameObject.transform.parent != null)
        {
            transform.rotation = GameObject.Find("FPSCam").transform.rotation;
        }
     

        Physics.Raycast(transform.position, transform.forward);


        if (Input.GetButtonUp("Fire1") && isShooting == 0)
        {
            gameObject.transform.parent = player.transform;
            transform.localPosition = startPos;
           
            if (gameObject.tag == "LightBall")
            {
                gameObject.transform.parent = player.transform;
                transform.localPosition = startPos2;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                ball2.GetComponent<Light>().enabled = true;
                lightBallRigid.AddForce(transform.forward + transform.right / 2 * flyOutSpeed);
            }
            else if (gameObject.tag == "LightBall2")
            {
                gameObject.transform.parent = player.transform;
                transform.localPosition = startPos3;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                ball3.GetComponent<Light>().enabled = true;
                lightBallRigid.AddForce(transform.forward + transform.right / 2 * -flyOutSpeed);
            }

            gameObject.transform.parent = null;
            lightBallRigid.AddForce(transform.forward * flyOutSpeed);





            gameObject.GetComponent<SphereCollider>().enabled = true;

            if (lightBallRigid.IsSleeping())
            {
                lightBallRigid.WakeUp();
            }


            isShooting = 1;
        }

        if (Input.GetButtonDown("Fire1") && isShooting == 1)
        {

            lightBallRigid.Sleep();


        }

        if (Input.GetButtonUp("Fire2") && isShooting == 1)
        {
            lightBallRigid.WakeUp();
            gameObject.transform.LookAt(player.transform);
            lightBallRigid.AddForce(transform.forward * recallSpeed);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            isShooting = 2;
         //   gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        if (Input.GetButtonDown("Fire2") && isShooting == 2)
        {
            transform.localPosition = startPos;
            gameObject.transform.parent = player.transform;
            if (gameObject.tag == "LightBall")
            {
                gameObject.transform.parent = player.transform;
                transform.localPosition = startPos2;
            }
            else if (gameObject.tag == "LightBall2")
            {
                gameObject.transform.parent = player.transform;
                transform.localPosition = startPos3;
            }
        }


        if (isShooting == 1)
        {
           // PlayerCharacterScript.freezeMove = true;
           if(gameObject.tag == "MainBall")
            {
                while (light1.range < 50)
                {
                    light1.range += 1f;
                }
            }
            else
            {
                while (light1.range < 50)
                {
                    light1.range += 1f;
                }
            }
           
           
        }

        if(isShooting == 2)
        {
            while (light1.range > 5)
            {
                light1.range -= 0.1f;
            }

           

            if (Vector3.Distance(transform.position, player.transform.position) <= minDistance)
            {
                gameObject.transform.parent = player.transform;
                lightBallRigid.Sleep();
                transform.localPosition = startPos;


                
                if (gameObject.tag == "LightBall")
                {
                    transform.localPosition = startPos2;
                    isShooting = 0;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    ball2.GetComponent<Light>().enabled = false;
                }
                else if(gameObject.tag == "LightBall2")
                {
                    transform.localPosition = startPos3;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    ball3.GetComponent<Light>().enabled = false;
                    isShooting = 0;
                }
                isShooting = 0;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                PlayerCharacterScript.freezeMove = false;
               // gameObject.GetComponent<TrailRenderer>().startWidth = 0;

              
                
            }
        }

      
    }
}