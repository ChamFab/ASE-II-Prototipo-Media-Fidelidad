using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public NPC npc; // Se usa el codigo ScriptableObject llamado NPC
    bool isTalking = false;

    float distance;// distancia del NPC al jugador 
    float curResponseTracker = 0; // Identifica las cajas de texto del NPC

    public GameObject player;
    public GameObject dialogueUI; //el panel donde irá introducido el texto

    public Text npcName;
    public Text npcDialogueBox; //dialogo del Npc
    public Text playerResponse; //Respuesta del Jugador 

   
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }
      void FixedUpdate()
    {
        OnMouseOver();
    }
    void OnMouseOver() // al poner el mouse sobre el jugador 
    {
        //calcula la distancia al player 
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance <= 2.5f)
        {
            if(Input.GetAxis("Mouse ScrollWheel") < 0f) //Obtiene la rueda de Scroll

            {
                curResponseTracker = 0; //Incia en 0
                if (curResponseTracker >= npc.playerDialogue.Length - 1) //Elk largo del dialogo y el desglose de la rueda
                {
                    curResponseTracker++;
                    if (curResponseTracker >= npc.playerDialogue.Length) //Sigue bajando el scroll para bajar el diálogo 
                    {
                        curResponseTracker = npc.playerDialogue.Length - 1 ; //Toma un valor de -1 al bajar el scroll 
                    }
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker++;
                if(curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }
            //trigger del dialogo
            if(Input.GetKeyDown(KeyCode.E) && isTalking == false ) //Consigue la letra E
            {
                StartConversation();

            }
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true) //Si el  NPC está hablando 
            {
                EndDialogue();
            }
            //si el scroll es mayor o igual a 0 aquí comienzan los registros de texto de ambos
            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0) 
            {
                playerResponse.text = npc.playerDialogue[0]; //es igual al texto desplegado del NPC
                if(Input.GetKeyDown(KeyCode.Return)) //consigue Enter
                {
                    npcDialogueBox.text = npc.dialogue[1]; //Comienza a desplegar el texto 
                }

            }
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1 ) //se realizan manualmente los registros
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }

            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2) //se realizan manualmente los registros
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
        }
    }
    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true); //Activa la caja de Texto
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];// llamo al array de la caja de diálogo de NPC

    }
    void EndDialogue()
    {
        isTalking = true;
        dialogueUI.SetActive(false);//desactiva la caja de texto
    }
}

