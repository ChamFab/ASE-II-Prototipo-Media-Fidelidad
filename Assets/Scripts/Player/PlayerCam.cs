using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // para la sensibilidad de la cámara al rotar la vista
    public float sensX;
    public float sensY;

    //para saber hacia donde esta viendo la cámara
    public Transform orientation;

    // para la rotación de la cámara
    float RotationX = 0;
    float RotationY = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // tomamos el input del mouse 
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //rotación al girar la cabeza 
        // se invierten los valores entre X y Y para una correcta rotación

        RotationY += mouseX;

        RotationX -= mouseY;
        // para que la cámara no rote bruscamente se limita a 90 y -90
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        //Rotar la cámara y al jugador 
        transform.rotation = Quaternion.Euler(RotationX, RotationY, 0); //Euler haciendo refeencia el numero e en calculo diferencial
        //para la orientación de la capsula
        //La variable Orientation  definidad arriba

        orientation.rotation = Quaternion.Euler(0, RotationY, 0);
    }
}
