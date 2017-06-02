using UnityEngine;
using System.Collections;

public class PopUpBarNotifAnim : MonoBehaviour {


    /* Script to control Pop up notification when food is bought in Supplier Scene*/


    public static bool ispopup;
    public static bool ispopuphide;
    public Animator popbarAnim;
    float cooldowntimer;
    // Use this for initialization
    void Start () {

        cooldowntimer = 5;
    }
	
	// Update is called once per frame
	void Update () {

        if (ispopup)
        {
            popbarAnim.SetTrigger("Show");
            ispopup = false;
            ispopuphide = true; //Start counter
        }
        if (ispopuphide)
        {

            cooldowntimer -= Time.deltaTime;
            if (cooldowntimer <= 0)
            {
                cooldowntimer = 5;     //Pop up stays for 5 seconds
                popbarAnim.SetTrigger("Hide");

                StartCoroutine(RunPopCloseAnimProcess());   //Check when Animation ends

                ispopup = false;
                ispopuphide = false;
            }
        }

        //Debug key
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    ispopup = true;
        //}
    }

    public IEnumerator RunPopCloseAnimProcess()
    {

        yield return new WaitForSeconds(popbarAnim.GetCurrentAnimatorStateInfo(0).length);

        //When Animation is ended in "Hide", set gameobject to false
        gameObject.SetActive(false);

    }
}
