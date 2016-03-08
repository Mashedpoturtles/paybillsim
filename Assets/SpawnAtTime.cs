using UnityEngine;
using System.Collections;

public class SpawnAtTime : MonoBehaviour {
 
    //issue all the bills at their set times in update.
    void Update ()
    {
        IssueElectricityBill();
        IssueInternetBill();
    }
  
    void IssueElectricityBill()
    {
        if (TimeManager.instance.Day == 1 || TimeManager.instance.Day == 7 || TimeManager.instance.Day == 14 || TimeManager.instance.Day == 21)
        {
            // so long as this bill does not already exist it may currently spawn.
            //TODO bill list.
            if (ElectricityBill.instance.Exists != true)
            {
                ElectricityBill.instance.Electricity();
            }
        }
    }

    void IssueInternetBill()
    {
        if (TimeManager.instance.Day == 3 || TimeManager.instance.Day == 9 || TimeManager.instance.Day == 18 || TimeManager.instance.Day == 26)
        {
            // so long as this bill does not already exist it may currently spawn.
            //TODO bill list.
            if(InternetBill.instance.Exists != true)
            {
                InternetBill.instance.Internet();
            }
        }
    }
}
