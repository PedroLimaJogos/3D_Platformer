using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateBase : MonoBehaviour
    {
        // Start is called before the first frame update
        public virtual void OnStateEnter(object o = null)
        {
            Debug.Log("OnStateEnter");
        }
        public virtual void OnStateStay()
        {
            Debug.Log("OnStateStay");
        }
        public virtual void OnStateExit()
        {
            Debug.Log("OnStateExit");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}