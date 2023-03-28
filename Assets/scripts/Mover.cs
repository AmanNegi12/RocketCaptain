using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)] float slider;
    Vector3 endpos = new Vector3(0,-12,0);
    Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        startpos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset=endpos*slider;
        float cycle = Time.time / 2f;//using this we can half the time of a second

        float tau = Mathf.PI * 2;//this is 2PIE
        /*  Debug.Log(cycle*tau);*///this is speeding the time 
        float rawsinwave=Mathf.Sin(cycle*tau);  
        slider = rawsinwave/2f+0.5f;
        transform.position=startpos+offset;
      
    }
}
