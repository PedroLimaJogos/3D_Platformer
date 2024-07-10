using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateRun : StateBase
{
    private GameObject character; // Referência ao GameObject do personagem
    private float speed = 5f; // Velocidade do personagem

    public void definePersonagem(GameObject character)
    {
        this.character = character;
    }

    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entering Run State");
        // Adicione lógica para quando entrar no estado de correr
    }

    public override void OnStateStay()
    {
        Debug.Log("Running...");
        // Mover o personagem no eixo x
        if (character != null)
        {
            character.transform.position += Vector3.right * speed * Time.deltaTime;
            Debug.Log("Character position: " + character.transform.position); // Adicione esta linha para verificar a posição
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Run State");
        // Adicione lógica para quando sair do estado de correr
    }
}
