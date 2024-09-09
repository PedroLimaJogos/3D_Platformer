using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }


    public class itemManager : MonoBehaviour
    {
        public List<ItemSetup> itemSetups;
        public static itemManager Instance;
        public TextMeshProUGUI uiTextCoins;
        //public int coins;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            Reset();
        }

        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        private void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, (int)SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, (int)SaveManager.Instance.Setup.health);
        }

        // Update is called once per frame
        private void Reset()
        {
            foreach(var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }
        public void AddByType(ItemType itemType,int amount = 1)
        {
            if(amount <0 ) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType,int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount; 

            if(item.soInt.value < 0) item.soInt.value = 0;
        }


        [NaughtyAttributes.Button]
        private void AddCoid()
        {
            AddByType(ItemType.COIN);
        }
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOint soInt;
        public Sprite icon;
    }
}