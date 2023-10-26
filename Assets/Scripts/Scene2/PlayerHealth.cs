using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 100)] public int startHealth = 3, currentHealth;

    public static bool isDead;
    [SerializeField] private TextMeshProUGUI VidaPlayerTXT;

    public GameObject CanvasPerdiste;// canvas de perder 
    // Start is called before the first frame update
    void Start()
    {
        // salud Actual
        currentHealth = startHealth;
        //Texto del enemigo
        VidaPlayerTXT.text = "Hp :" + currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        //contabiliza a 0 y lanza si el jugador ha muerto
        if (currentHealth == 0 && !isDead)
        {
            isDead = true;
            CanvasPerdiste.SetActive(true);
            Debug.Log("The player is dead");
        }

    }
    //trigger de colisión con el clindro
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Positivo"))
        {
            Destroy(other.gameObject);
            Debug.Log("Vida");
            if (currentHealth < startHealth)
            {
                currentHealth++;
                VidaPlayerTXT.text = "Hp:" + currentHealth;
            }

        }
        if (other.gameObject.CompareTag("Negativo"))
        {
            Destroy(other.gameObject);//destruye los items
            TakeDamage(1);
        }
    }
    public void TakeDamage(int amount)
    {
        //currentHealth -= amount;
        currentHealth--;
        VidaPlayerTXT.text = "Hp :" + currentHealth;
        Debug.Log("Muerte");
    }
}
