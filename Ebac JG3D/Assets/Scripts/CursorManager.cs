using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Bloquear o cursor na tela
        Cursor.lockState = CursorLockMode.Locked;
        // Tornar o cursor invisível
        Cursor.visible = false;
    }

    void Update()
    {
        // Liberar o cursor ao apertar Esc (para sair do modo de bloqueio)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Libera o cursor
            Cursor.visible = true; // Torna o cursor visível
        }

        // Voltar a bloquear o cursor se o jogador apertar o botão direito
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked; // Bloqueia o cursor de novo
            Cursor.visible = false; // Esconde o cursor
        }
    }
}

