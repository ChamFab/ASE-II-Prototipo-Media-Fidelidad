using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed; // velocidad de Movimiento

    public Transform orientation; //Orientaci�n 

    //declaraci�n de ejes
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection; // direcci�n de movimiento en Vector3
    Rigidbody rb;
    public float groundDrag;

    // declaraci�n de variables del sprint 
    [SerializeField] private float runSpeed, WalkSpeed;
    [SerializeField] private float runBuildUp;
    [SerializeField] private KeyCode runKey;

    [Header("Salto")] // Salto del personaje
    public KeyCode jumpkey = KeyCode.Space;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;


    [Header("Ground Check")] // checar si est� tocando el suelo
    public float playerHeight;// altura del jugador
    public LayerMask whatIsGround; //identifica el suelo
    bool grounded;// corrobora si est� tocado el suelo

    [Header("Item Speed")] //recoger item de velocidad
    public float multiplier = 1.5f;
    public GameObject ObjSpeed;
    [SerializeField] private float speedBoostDuration = 2; //duraci�n del boost de velocidad 
    private bool speedControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

    }

    // Update is called once per frame
    void Update()
    {
        // calcular la distancia del piso a la que esta del piso con el playerHeight y suma o multiplica un valor extra para asegurar que est� en el piso
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        MyInput();
        SpeedControl();
        // handle drag = valor del drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }
    private void FixedUpdate()
    {
        // se manda a llamar aqu� por la f�sica del personaje
        MovePlayer();

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Obtener el valor de los ejes 
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false; //listo para saltar 

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void SpeedControl() //control de velocidad
    {
        if (!speedControlEnabled) //Desactivar controlador
            return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limity velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            //normalizar usando la variable moveSpeed
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            // limita la velocidad de rb 
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if (Input.GetKey(runKey))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, Time.deltaTime * runBuildUp); // math.lerp hace alusi�n a una funci�n que consiste en interpolaci�n lineal matem�tica
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, WalkSpeed, Time.deltaTime * runBuildUp);
        }
    }
    private void MovePlayer() //void que mueve al personaje
    {
        //para mover al player en la direcci�n en la que se est� viendo
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // para que se mueva con determinada fuerza
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Jump() // void de salto
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // a�ade fuerza a el RB
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump() // revisar el salto 
    {
        readyToJump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //comunmente se hace con tag pero ahora se usa el nombre en el editor
        if (other.name == "ObjSpeed")
        {
            moveSpeed *= multiplier;
            Destroy(ObjSpeed);
            ActiveSpeedBoost();
        }
    }

    public void ActiveSpeedBoost()
    {
        speedControlEnabled = false;
        StartCoroutine(SpeedBoostCoolDown()); //Inicia la subrutina 
    }
    // immediately calls MoveNext, in order for the code to reach the first yield return,, el m�todo no comienza de forma independiente y debe llamar a MoveNext
    IEnumerator SpeedBoostCoolDown()
    {
        //Este float restringe la movespeed del personaje 
        float originalMoveSpeed = moveSpeed;

        //Aplicar el numero de velocidad
        moveSpeed *= multiplier;

        //Esperar durante la duraci�n del aumentod de velocidad
        yield return new WaitForSeconds(speedBoostDuration);

        //Restablecer la velocidad del jugador
        moveSpeed = originalMoveSpeed;

        speedControlEnabled = true;
    }
}
