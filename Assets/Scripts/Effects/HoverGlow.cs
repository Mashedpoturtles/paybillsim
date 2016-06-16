using UnityEngine;

public class HoverGlow : MonoBehaviour
    {
    [SerializeField]
    private Renderer rend;
    [SerializeField]
    private Material glow;
    [SerializeField]
    private Material defaultShader;

    private void Start ( )
        {
        rend = GetComponent<Renderer> ( );
        }
    private void OnMouseEnter ( )
        {
        rend.material = glow;
        }
    private void OnMouseExit ( )
        {
        rend.material = defaultShader;
        }
    }
