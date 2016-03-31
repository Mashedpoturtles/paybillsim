using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    private Animator anim;
   
    public bool paused;
    IEnumerator aniOff()
    {
        yield return new WaitForSeconds(10);
        anim.enabled = false;
    }
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(aniOff());
        paused = false;
    }

    
    public void Paused()
    {

        paused = !paused;
        if(paused)
        {
            Time.timeScale = 0.0f;
        }
        if(!paused)
        {
            Time.timeScale = 1.0F;
        }
    }
}
