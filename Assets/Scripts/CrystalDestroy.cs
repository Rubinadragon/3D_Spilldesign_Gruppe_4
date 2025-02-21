using UnityEngine;

public class CrystalDestroy : MonoBehaviour
{
    //Hente fram riftkrystaller
    //Finne dynamisk antall krystaller basert på scenen
    //Når spilleren rører bort i en krystall, ødelegg krystall og reduser total mengde krystaller i scenen med 1
    //Hvis spilleren har samlet nok krystaller, spawn en siste stor riftkrystall
    //HUSK: Ha en variabel på spilleren som teller antall krystaller ødelagt.
    public GameObject RiftCrystals;
    private int TotalCrystalsDestroyed = 0;
    private int CrystalAmountInLvl;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.tag == "RiftCrystal")
        {
            TotalCrystalsDestroyed++;
            Debug.Log(TotalCrystalsDestroyed);
            Destroy(other.gameObject);
        }
    }
}
