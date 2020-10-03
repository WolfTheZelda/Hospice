using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float Sensibilidade;
    public float andar;
    public float correr;
    public float gravity;

    int ctrlCursor;
    CharacterController controle;
    Vector3 moveDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        ctrlCursor = 0;
        controle = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }
    void Controller()
    {
        //Rotação do player
        //float MouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        //transform.Rotate(0,MouseX * Sensibilidade * Time.deltaTime, 0);
        //Para Desabilitar a seta do Mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ctrlCursor++;
            if (ctrlCursor > 1)
            {
                ctrlCursor = 0;
            }

        }
        if (ctrlCursor==0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //Character Controller
        if (controle.isGrounded)
        {
            //movimentação do player
            moveDirection = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal")*Sensibilidade*Time.deltaTime, 0, CrossPlatformInputManager.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //para andar 
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= andar;
            }
            else
            {
                // Correr
                if (CrossPlatformInputManager.GetAxis("Vertical")> 0)
                {
                    moveDirection *= correr;
                }
                
            }
            
            
        }
        //Simulador - Apply Gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //aplicando fidica e movimentação ao character Controller
        controle.Move(moveDirection * Time.deltaTime);
    }
}
