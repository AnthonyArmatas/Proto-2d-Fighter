using UnityEngine;


public class IceAttacktScript : MonoBehaviour {
    private float nextFire;

    public float IceBFireRate = .5f; //.5f need f to make it a float
    public Transform IceBSpwnP; //Ironically, the attack's position needs to be the attackspawn's transforms's position 
    public Transform IceBSpwnR; //While the rotaion needs to be the aimpositions transformations rotaion

    public GameObject iceBolt;

    void Start()
    {
        //iceBolt = GetComponent<IceAttacktScript>();
    }



    public void launchIceBolt(Animator tor)
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + IceBFireRate;
            Instantiate(iceBolt, IceBSpwnP.position, IceBSpwnR.rotation);
            tor.SetBool("Attack_IB", true);
        }
    }
}
