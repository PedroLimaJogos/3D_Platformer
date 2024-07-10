using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Screens
{
    public enum ScreenType
    {
        Panel,
        Info_Panel,
        Shop
    }


    public class screenBase : MonoBehaviour
    {
        public ScreenType screenType;

        public List<Transform> listOfObjects;

        public Image uiBackGround;
        public List<Typper> listOfPhrases;
        public bool startHided = false;

        public float delayBetweenObjects = .05f;
        public float animationDuration = .3f;

        private void Start()
        {
            if (startHided)
            {
                HideObjects();
            }
        }

        [Button]
        public virtual void Show()
        {
            Debug.Log("Show");
            ShowObjects();
        }

        [Button]
        public virtual void Hide()
        {
            Debug.Log("Hide");
            HideObjects();
        }

        public void HideObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(false));
            uiBackGround.enabled = false;
        }

        public void ShowObjects()
        {
            for (int i = 0; i < listOfObjects.Count; i++)
            {
                var obj = listOfObjects[i];

                obj.gameObject.SetActive(true);
                obj.DOScale(0, animationDuration).From().SetDelay(i * delayBetweenObjects);
            }

            Invoke(nameof(StartType), delayBetweenObjects * listOfObjects.Count);
            uiBackGround.enabled = true;
        }

        private void StartType()
        {
            for (int i = 0; i < listOfObjects.Count; i++)
            {
                listOfPhrases[i].startType();
            }
        }
        private void ForceShowObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(true));
        }


    }
}
