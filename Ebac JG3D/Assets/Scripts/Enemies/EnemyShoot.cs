using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;
        public Material alertMaterial;       // Material para quando o player estiver na área (textura de alerta)
        public Material defaultMaterial;     // Material padrão do inimigo (textura padrão)
        public MeshRenderer enemyRenderer;  // Renderer do inimigo para mudar o material
        private bool playerInRange = false;  // Verifica se o player está na área

        protected override void Init()
        {
            base.Init();
            enemyRenderer.material = defaultMaterial;      // Define o material padrão
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                gunBase.StartShoot();                      // Começa a atirar
                enemyRenderer.material = alertMaterial;    // Muda o material para o de alerta (muda a textura)
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                gunBase.StopShoot();                       // Para de atirar
                enemyRenderer.material = defaultMaterial;  // Volta para o material padrão (muda a textura de volta)
            }
        }
    }
}
