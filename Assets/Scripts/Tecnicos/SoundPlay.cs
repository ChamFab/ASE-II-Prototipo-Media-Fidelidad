using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource Audio1;
    public AudioSource Audio2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            sonido();
        }
    }


    public void sonido()
    {
        Audio1.Play();
    }
    public void sonido2()
    {
        Audio2.Play();
    }
}
