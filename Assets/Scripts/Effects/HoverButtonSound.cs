using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonSound : MonoBehaviour, IPointerEnterHandler 
{
[SerializeField]
private GameObject buttons;

public void OnPointerEnter ( PointerEventData eventData )
    {
    GlobalAudio.instance.SoundHover ( );
    }
}
