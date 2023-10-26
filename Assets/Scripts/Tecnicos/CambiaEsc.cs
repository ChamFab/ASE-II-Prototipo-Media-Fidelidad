
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEsc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Escena1()
    {
        SceneManager.LoadScene(1);
    }
    public void Escena2()
    {
        SceneManager.LoadScene(2);
    }
}
