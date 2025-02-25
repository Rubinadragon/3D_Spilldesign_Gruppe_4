using UnityEngine;

public class CrystalDestroy : MonoBehaviour
{
    //Hente fram riftkrystaller
    //Finne dynamisk antall krystaller basert på scenen
    //Når spilleren rører bort i en krystall, ødelegg krystall og reduser total mengde krystaller i scenen med 1
    //Hvis spilleren har samlet nok krystaller, spawn en siste stor riftkrystall
    //HUSK: Ha en variabel på spilleren som teller antall krystaller ødelagt.
    public GameObject RiftCrystals;
    public GameObject RiftHome;
    public GameObject RiftVector;
    private int TotalCrystalsDestroyed = 0;
    private int CrystalsDestroyedinLVL = 0;

    //private int CrystalAmountInLvl;
    private void OnTriggerEnter(Collider other)
    {
        Vector3 RiftPosition = RiftVector.transform.position;
        Quaternion RiftRotation = RiftVector.transform.rotation;
        if(other.transform.tag == "RiftCrystal")
        {
            TotalCrystalsDestroyed++;
            CrystalsDestroyedinLVL++;
            Debug.Log(TotalCrystalsDestroyed);
            Destroy(other.gameObject);
        }
        if(CrystalsDestroyedinLVL == 5)
        {
            //Lag en empty game object som holder på posisjonen til siste krystall
            //Spawne en riften tilbake når nok krystaller er samlet,.
            Instantiate(RiftHome, RiftPosition, RiftRotation);
            CrystalsDestroyedinLVL = 0;
        }
    }

}
