using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    
    enum levelloader {none,next,previous  }
    levelloader levelload=levelloader.none;
    enum coll { none,On,off}
    coll currentcoll = coll.On;
    enum state {alive,dead,none }
    state currentState = state.none;
    private Rigidbody rb;
    AudioSource audios;
    [SerializeField] AudioClip audioClip0 ;
    [SerializeField] AudioClip audioClip1 ;
    [SerializeField] AudioClip audioClip2 ;
    [SerializeField] ParticleSystem thrust ;
    [SerializeField] ParticleSystem dead ;
    [SerializeField] ParticleSystem win ;
    [SerializeField] float RotationPS = 100f;
    [SerializeField] float thrustPS = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
        currentState = state.alive;
        rb= GetComponent<Rigidbody>();
        audios = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState==state.alive)
        {
            thrusting();
            controling();
            lloader();
            col();
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentcoll == coll.off) { return; }
        if (currentState == state.dead) { return; }
        if (collision.gameObject.CompareTag("deadly"))
        {
            Invoke("loader0",2f);
            
            if (audios.isPlaying)
            {
                audios.Stop();
            }
            audios.PlayOneShot(audioClip1);
            dead.Play();
            thrust.Stop();
            currentState = state.dead;
        }
      
        if(collision.gameObject.CompareTag("finish"))
        {
            Invoke("loader1", 2f);

            if (audios.isPlaying)
            {
                audios.Stop();
            }
            audios.PlayOneShot(audioClip2);
            win.Play();
            thrust.Stop();
            currentState = state.dead;
        }


    }

    void thrusting()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up*thrustPS*Time.deltaTime);
            if (!audios.isPlaying)
            {
                audios.PlayOneShot(audioClip0);
                thrust.Play();
            }

        }
        else
        {

            audios.Stop();
                thrust.Stop();

        }

    }

    private void controling()
    {
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward *RotationPS*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotationPS * Time.deltaTime);
            Debug.Log("Right");
        }
        rb.freezeRotation = false;
    }
    void loader0()
    {
        SceneManager.LoadScene(0);
       
    }
    void loader1()
    {
        SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
        
    }
    void lloader()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            
            int a = SceneManager.GetActiveScene().buildIndex;
            if (a==0)
            {
                loader1();
            }
            if (a==1)
            {

                loader0();
            }
        }


    }
    void col()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentcoll==coll.On)
            {
                currentcoll = coll.off;
                Debug.Log("Collision off");
            }
            else if (currentcoll == coll.off)
            {
                Debug.Log("Collision on");
                currentcoll = coll.On;
            }
        }
    }
    void Rt()
    {
        //float horizoontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 dir = new Vector3(horizoontal, 0, 0).normalized;
        //float taegetangel = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //float angel = Mathf.SmoothDampAngle(transform.eulerAngles.z, -taegetangel, ref velocity, 0.1f);

        //transform.rotation = Quaternion.Euler(0, 0, angel);
         
    }
}
