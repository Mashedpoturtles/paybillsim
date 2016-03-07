using UnityEngine;
using System.Collections;

public class SpawnAtTime : MonoBehaviour {
 
    void Update ()
    {
        IssueElectricityBill();
    }
  
    void IssueElectricityBill()
    {
        if (TimeManager.instance.Day == 1 || TimeManager.instance.Day == 7 || TimeManager.instance.Day == 14 || TimeManager.instance.Day == 21)
        {
           if(BillTypes.instance.Exists != true)
            {
                BillTypes.instance.ElectricityBill();
            }
        }
    }
}
