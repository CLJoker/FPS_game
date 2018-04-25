using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wepScript : MonoBehaviour {

    public Camera fpsCam;
    public GameObject hitPar;
    public int damage = 30;
    public int range = 10000;
    [SerializeField] private Animator anim;
    private PhotonView photonView;

    public float fireRate = 0.1f;
    float fireTimer;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(photonView.isMine)
        {
            if (Input.GetMouseButton(0))
            {
                FireShot();

            }

            if (fireTimer < fireRate)
            {
                fireTimer += Time.deltaTime;
            }
        }
        
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("Shoot"))
        {
            anim.SetBool("Firing", false);
        }
    }

    public void FireShot()
    {
        if (fireTimer < fireRate) return;

        RaycastHit hit;

        Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if(Physics.Raycast(ray, out hit,range))
        {


            if(hit.transform.tag == "Soldier_Player")
            {
                hit.transform.GetComponent<PhotonView>().RPC("applyDamage", PhotonTargets.All, damage);
            }
            GameObject par;

            
            par = PhotonNetwork.Instantiate(hitPar.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
        }

        anim.SetBool("Firing", true);

        fireTimer = 0.0f;
    }
}
