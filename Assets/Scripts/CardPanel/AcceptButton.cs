using UnityEngine;

public class AcceptButton : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }
    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.ACTIVE_ACCEPT_BUTTON, SetActive);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ACTIVE_ACCEPT_BUTTON, SetActive);
    }
    
    private void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void OnClickButton()
    {
        Messenger.Broadcast(GameEvent.HANDLE_CHOSEN_CARD);
        Messenger.Broadcast(GameEvent.CLOSE_CARDS_PANEL);
    }
}
