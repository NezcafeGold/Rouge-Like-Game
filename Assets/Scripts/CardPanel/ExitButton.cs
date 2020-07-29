using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        Messenger.AddListener<GridCell>(GameEvent.PLAYER_MOVE_ON_CELL, SetActive);
        Messenger.AddListener(GameEvent.BLOCK_TO_ROTATE, SetNotActive);
    }

    private void SetNotActive()
    {
        gameObject.SetActive(false);
    }

    private void SetActive(GridCell arg1)
    {
        gameObject.SetActive(true);
    }
}
