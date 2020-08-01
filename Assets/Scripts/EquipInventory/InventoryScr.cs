using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScr : MonoBehaviour
 {
     [SerializeField] private GameObject smallCard;
     private const int AMOUNT_CARDS = 15;
     private List<GameObject> smallCardsList;
     private void Start()
     {
         gameObject.SetActive(false);
         GenerateCardPlaces();
     }
 
     private void Awake()
     {
         Messenger.AddListener(GameEvent.SHOW_INVENTORY, ShowInventory);
     }
 
     private void OnDestroy()
     {
         Messenger.RemoveListener(GameEvent.SHOW_INVENTORY, ShowInventory);
     }
 
     public void ShowInventory()
     {
         if (!gameObject.activeSelf)
             gameObject.SetActive(true);
         else gameObject.SetActive(false);
     }
 
     private void GenerateCardPlaces()
     {
         smallCardsList =  new List<GameObject>();
         for (int i = 0; i < AMOUNT_CARDS; i++)
         {
             smallCardsList.Add(Instantiate(smallCard, transform, false));
         }
     }
 }