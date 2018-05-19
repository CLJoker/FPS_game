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

    private int currentBullets = 30;
    private int currentTotalBullets = 120;
    private float reloadTime = 2f;

    private PhotonView photonView;
    private float fireTimer;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnGUI()
    {
        if (photonView.isMine)
        {
            GUI.Box(new Rect(400, 280, 100, 25), currentBullets + "/" + currentTotalBullets);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && currentBullets > 0)
        {
            FireShot();
            muzzleFlash.SetActive(true);

 
        }

        if (Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            FireShot();
            muzzleFlash.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(currentBullets < 30)
            {
                StartCoroutine(ReloadWeapon());
            }
        }


        if (Input.GetMouseButtonUp(0) || currentBullets <= 0)
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
        currentBullets -= 1;
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

    IEnumerator ReloadWeapon()
    {
        yield return new WaitForSeconds(reloadTime);
        if(currentTotalBullets >= 30)
        {
            int numberBulletReload = 30 - currentBullets;
            currentBullets = 30;
            currentTotalBullets -= numberBulletReload;
        }
        else if(currentTotalBullets > 0)
        {
            currentBullets += currentTotalBullets;
            currentTotalBullets = 0;
        }

    }
}
