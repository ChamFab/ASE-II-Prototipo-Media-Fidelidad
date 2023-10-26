using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    public float speed = 6;
    public Rigidbody rb;

    float horizontalInput; //consigue el eje Horizontal
    public float horizontalMultiplier = 2;

    // Update is called once per frame
    private void FixedUpdate() //50 times per second 
    {
        if (!alive) return;//si no esta vivo parar la función
        
            
        Vector3 forwardMove = transform.forward * Time.fixedDeltaTime * speed; //mueve el personaje hacia adelante usando FixedUpdate
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove+ horizontalMove);
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(transform.position.y  < -5)
        {
            Die();
        }

    }
    public void Die()
    {
        alive = false;
        //Restart Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recarga la escena actual

    }
}
