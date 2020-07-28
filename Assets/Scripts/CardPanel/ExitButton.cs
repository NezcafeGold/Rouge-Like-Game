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
    }

    private void SetActive(GridCell arg1)
    {
        gameObject.SetActive(true);
    }
}
