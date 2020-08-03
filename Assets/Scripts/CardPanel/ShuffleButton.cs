using UnityEngine;

public class ShuffleButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.BLOCK_TO_ROTATE, SetNotActive);
        Messenger.AddListener(GameEvent.SHUFFLE_BUTTON, SetActive);
    }

    private void SetNotActive()
    {
        gameObject.SetActive(false);
    }

    private void SetActive()
    {
        gameObject.SetActive(true);
    }
}
