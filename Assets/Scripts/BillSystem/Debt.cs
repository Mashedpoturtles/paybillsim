using UnityEngine;
using UnityEngine.UI;

public class Debt : MonoBehaviour
    {
    [SerializeField]
    private Text debtInfo;
    [SerializeField]
    private Slider debtSlider;
    [SerializeField]
    public int currentDebt;
    public static Debt instance;

    public void Start ( )
        {
        instance = this;
        debtInfo = GameObject.FindWithTag ( "Debt" ).GetComponent<Text> ( );
        debtSlider.maxValue = 10000;
        debtSlider.minValue = 0;

        if ( instance == null )
            {
            instance = this;
            DontDestroyOnLoad ( gameObject );
            }
        else if ( instance != this )
            {
            Destroy ( gameObject );
            return;
            }
        }

    private void Update ( )
        {
        debtSlider.value = currentDebt;
        debtInfo.text = "Schuld: " + currentDebt;

        if ( currentDebt <= 0 )
            {
            currentDebt = 0;
            }
        }
    }
