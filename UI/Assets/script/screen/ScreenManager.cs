
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Screens
{
    public class ScreenManager : Singles<ScreenManager>
    {
        public List<screenBase> screenBases;

        public ScreenType startScreen = ScreenType.Panel;

        private screenBase _currentScreen;



        private void Start()
        {
            hideAll();
            ShowByType(startScreen);
        }

        public void ShowByType(ScreenType type)
        {
            if (_currentScreen != null) _currentScreen.Hide();

            var nextScreen = screenBases.Find(i => i.screenType == type);


            nextScreen.Show();
            _currentScreen = nextScreen;
        }
        public void hideAll()
        {
            screenBases.ForEach(i => i.Hide());
        }

    }

    public class Singles<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<T>();
            else
                Destroy(gameObject);
        }
    }
}
