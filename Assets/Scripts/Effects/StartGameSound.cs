using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameSound : MonoBehaviour, IPointerDownHandler
{
[SerializeField]
private GameObject buttons;

public void OnPointerDown ( PointerEventData eventData )
    {
    GlobalAudio.instance.SoundConfirm ( );
    }
}