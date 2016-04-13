using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
    [SerializeField]
    private Text target;

    private void Start ( )
        {
        target.text = "";
        }
    public void OnPointerEnter ( PointerEventData eventData )
        {
        target.text = "You can drop your card here.";
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        target.text = "";
        }
    }
