using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AnnouncerText : MonoBehaviour {
    [SerializeField]
    private Text target;

    private void Start ( )
        {
            StartCoroutine ( AnnounceText());
        }
    private IEnumerator AnnounceText ()
        {
        while(true)
            {
            target.text = "Als je niet kunt betalen worden de rekeningen automatisch links bewaard.";
            yield return new WaitForSeconds ( 4 );
            target.text = "Wacht niet te lang met betalen, de rekening word duurder hoe langer je wacht!";
            yield return new WaitForSeconds ( 4 );
            }
        }
    }
