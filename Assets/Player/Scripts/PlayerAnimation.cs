using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    bool andar;
    bool correr;
    bool andarParatrás;


    // Start is called before the first frame update
    void Start()
    {
        andar = false;
        correr = false;
        andarParatrás = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Sistema de Animação
        if (Input.GetKey(KeyCode.LeftShift) && CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            correr = true;
            andar = false;
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftShift) && CrossPlatformInputManager.GetAxis("Vertical") > 0)
            {
                correr = false;
                andar = true;
            }
            else
            {
                correr = false;
                andar = false;
            }               
        }
        //Andando para trás
        if (CrossPlatformInputManager.GetAxis("Vertical")<0)
        {
            andarParatrás = true;
        }
        else
        {
            andarParatrás = false;
        }
        
        // Aplicando valores para os parametros do AnimatorControle
        anim.SetBool("Correr", correr);
        anim.SetBool("Andar", andar);
        anim.SetBool("AndarParaTrás", andarParatrás);
    }
}
