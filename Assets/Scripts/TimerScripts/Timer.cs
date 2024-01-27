using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
 [SerializeField]private Image uýFill;
    private int Duration = 10;
    private int remaningDuration;

    private void Start()
    {
        Being(Duration);
    }
    private void Being(int second)
    {
        remaningDuration = second;
        StartCoroutine(updateTimer());
    }
    private IEnumerator updateTimer()
    {
        while(remaningDuration >= 0)
        {
            uýFill.fillAmount = Mathf.InverseLerp(0, Duration, remaningDuration);
            remaningDuration--;
            yield return new WaitForSeconds(1f);
        }
       
    }

        

}
