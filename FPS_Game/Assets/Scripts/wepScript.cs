using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wepScript : MonoBehaviour {

    public Camera fpsCam;
    public GameObject hitPar;
    public int damage = 30;
    public int range = 10000;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            FireShot();
        }
    }

    public void FireShot()
    {
        RaycastHit hit;

        Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.Log("shoot!");

        if(Physics.Raycast(ray, out hit,range))
        {


            if(hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PhotonView>().RPC("applyDamage", PhotonTargets.All, damage);
                Debug.Log("hit!");
            }
            GameObject par;
            par = PhotonNetwork.Instantiate(hitPar.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
            Destroy(par, 2f);
            Debug.Log(hit.transform.name);
        }
    }
}
