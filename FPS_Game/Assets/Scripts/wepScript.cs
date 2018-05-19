using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wepScript : MonoBehaviour {

    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject bulletHole;
    //[SerializeField] private GameObject bulletSpark;
    [SerializeField] private int damage = 30;
    [SerializeField] private int range = 1000;
    [SerializeField] private Animator anim;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private GameObject muzzleFlash;

    private PhotonView photonView;
    private float fireTimer;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            FireShot();
            muzzleFlash.SetActive(true);
        }

        if(Input.GetMouseButtonUp(0))
        {
            muzzleFlash.SetActive(false);
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
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

        if (Physics.Raycast(ray, out hit, range))
        {
            if (photonView.isMine)
            {
                GameObject bulletImpactHole = PhotonNetwork.Instantiate(bulletHole.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
            }
            if (hit.transform != null && LayerMask.LayerToName(hit.transform.gameObject.layer) == "Player")
            {
                hit.transform.GetComponent<PhotonView>().RPC("ApplyDamage", PhotonTargets.AllBuffered, damage);
            }
            //GameObject bulletImpactEffect = PhotonNetwork.Instantiate(bulletSpark.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
        }

        anim.SetBool("Firing", true);

        fireTimer = 0.0f;

        //Debug.Log(hit.transform.name);
    }
}
