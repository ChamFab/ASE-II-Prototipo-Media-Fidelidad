using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed; // velocidad de Movimiento

    public Transform orientation; //Orientación 

    //declaración de ejes
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection; // dirección de movimiento en Vector3
    Rigidbody rb;
    public float groundDrag;

    // declaración de variables del sprint 
    [SerializeField] private float runSpeed, WalkSpeed;
    [SerializeField] private float runBuildUp;
    [SerializeField] private KeyCode runKey;

    [Header("Salto")] // Salto del personaje
    public KeyCode jumpkey = KeyCode.Space;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;


    [Header("Ground Check")] // checar si está tocando el suelo
    public float playerHeight;// altura del jugador
    public LayerMask whatIsGround; //identifica el suelo
    bool grounded;// corrobora si está tocado el suelo

    [Header("Item Speed")] //recoger item de velocidad
    public float multiplier = 1.5f;
    public GameObject ObjSpeed;
    [SerializeField] private float speedBoostDuration = 2; //duración del boost de velocidad 
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
        // calcular la distancia del piso a la que esta del piso con el playerHeight y suma o multiplica un valor extra para asegurar que esté en el piso
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
        // se manda a llamar aquí por la física del personaje
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
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, Time.deltaTime * runBuildUp); // math.lerp hace alusión a una función que consiste en interpolación lineal matemática
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, WalkSpeed, Time.deltaTime * runBuildUp);
        }
    }
    private void MovePlayer() //void que mueve al personaje
    {
        //para mover al player en la dirección en la que se está viendo
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // para que se mueva con determinada fuerza
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Jump() // void de salto
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // añade fuerza a el RB
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
    // immediately calls MoveNext, in order for the code to reach the first yield return,, el método no comienza de forma independiente y debe llamar a MoveNext
    IEnumerator SpeedBoostCoolDown()
    {
        //Este float restringe la movespeed del personaje 
        float originalMoveSpeed = moveSpeed;

        //Aplicar el numero de velocidad
        moveSpeed *= multiplier;

        //Esperar durante la duración del aumentod de velocidad
        yield return new WaitForSeconds(speedBoostDuration);

        //Restablecer la velocidad del jugador
        moveSpeed = originalMoveSpeed;

        speedControlEnabled = true;
    }
}
