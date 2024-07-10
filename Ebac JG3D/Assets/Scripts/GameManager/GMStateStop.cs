using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class GMStateStop : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Entering Stop State");
        // Adicione lógica para quando entrar no estado de parar
    }

    public override void OnStateStay()
    {
        Debug.Log("Stopped.");
        // Adicione lógica para enquanto estiver no estado de parar
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Stop State");
        // Adicione lógica para quando sair do estado de parar
    }
}