using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{

     [SerializeField]private GameObject gameController;
     private GameController controllerScript;
     private bool dragged = false;
     private bool needReset = false;


     private void Start()
     {
         controllerScript = gameController.GetComponent<GameController>();
     }

     private void OnMouseDrag()
     {
         Debug.Log("Mouse is over GameObject.");
     }

     
     public void OnEndDrag(PointerEventData eventData)
     {
         
         Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
         var direction = GetDragDirection(dragVectorDirection);
         Debug.Log(direction);
         if (!needReset)
         {
             MoveToDirection(direction);
         }
         else
         {
             needReset = false;
         }

         dragged = false;
     }

     private void MoveToDirection(DraggedDirection direction)
     {
         StartCoroutine(controllerScript.MovePlayerToDirection(direction));
     }
 
     private DraggedDirection GetDragDirection(Vector3 dragVector)
     {
         float positiveX = Mathf.Abs(dragVector.x);
         float positiveY = Mathf.Abs(dragVector.y);
         DraggedDirection draggedDir;
         
         if (positiveX > positiveY)
         {
             draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
         }
         else
         {
             draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
         }
         return draggedDir;
     }

     
     public void OnBeginDrag(PointerEventData eventData)
     {
         
     }

     public void EnterPointOnPlayer()
     {
         needReset = true;
     }
     
     public void ExitPointOnPlayer()
     {
         needReset = false;
     }

     public void OnDrag(PointerEventData eventData)
     {
         
     }

     public void SetGridCell(GameObject gridCell)
     {
         Vector3 vct3 = gridCell.transform.localPosition;
         transform.localPosition = vct3;
     }
}
