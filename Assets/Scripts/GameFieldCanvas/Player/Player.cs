using System;
using System.Collections;
using DefaultNamespace;
using Singleton;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Singleton<Player>, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject gameField;
    [SerializeField] private Animator playerAnimator;
    private bool dragged;
    private bool needReset;


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
        StartCoroutine(MovePlayerToDirection(direction));
    }

    public IEnumerator MovePlayerToDirection(DraggedDirection direction)
    {
        var tileCell = transform.parent.GetComponent<TileCell>();
        int xMove = 0;
        int yMove = 0;
        switch (direction)
        {
            case DraggedDirection.Up:
                yMove--;
                break;
            case DraggedDirection.Down:
                yMove++;
                break;
            case DraggedDirection.Right:
                xMove++;
                break;
            case DraggedDirection.Left:
                xMove--;
                break;
        }

        GameObject cellToMove = null;
        try
        {
            cellToMove = gameField.GetComponent<GameField>().GetTileCellByCord(tileCell.TileСellValues.XCord + xMove,
                tileCell.TileСellValues.YCord + yMove);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        if (cellToMove != null && cellToMove.GetComponent<TileCell>().TileСellValues.IsEnableToMove)
        {
            yield return StartCoroutine(Animation(direction));

            playerAnimator.SetFloat("RunRight", 0);
            playerAnimator.SetFloat("RunLeft", 0);
            playerAnimator.SetFloat("RunUp", 0);
            playerAnimator.SetFloat("RunDown", 0);

            transform.SetParent(cellToMove.transform);
            transform.localPosition = new Vector3(0, 0, 0);
            cellToMove.GetComponent<TileCell>().VisitTile();
            Messenger.Broadcast(GameEvent.PLAYER_MOVE_ON_CELL,
                cellToMove.GetComponent<TileCell>()); //CardPanel.ShowCardsFromTile
            PlayerSetup.Instance.SubtractStamina(1);
        }


        yield return null;
    }


    private IEnumerator Animation(DraggedDirection direction)
    {
        if (direction == DraggedDirection.Left)
        {
            playerAnimator.SetFloat("RunLeft", 1);
        }

        if (direction == DraggedDirection.Right)
        {
            playerAnimator.SetFloat("RunRight", 1);
        }

        if (direction == DraggedDirection.Up)
        {
            playerAnimator.SetFloat("RunUp", 1);
        }

        if (direction == DraggedDirection.Down)
        {
            playerAnimator.SetFloat("RunDown", 1);
        }

        yield return new WaitForSeconds(2.8f);
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
}