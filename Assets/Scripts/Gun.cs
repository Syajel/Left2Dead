using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 20;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int heldAmmo = 100;
    public int maxAmmo = 10;
    [HideInInspector] public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpact;

    private float nextTimeToFire = 0f;

    public bool isRageModeOn;
    public JoelMeters joelMeters;

    CompanionManager cmp;
    void Start()
    {
        cmp = GameObject.Find("CompanionManager").GetComponent<CompanionManager>();
        
        if(cmp.instance.name.Equals("Bill") || cmp.instance.name.Equals("Bill(Clone)"))
            maxAmmo += (int)(maxAmmo*0.5f);
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0 && (heldAmmo > 0 | transform.gameObject.name == "pistol_1"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        FindObjectOfType<AudioManager>().play("reload");
        yield return new WaitForSeconds(reloadTime);
        if(transform.gameObject.name == "pistol_1")
        {
            currentAmmo = maxAmmo;
            isReloading = false;
        }
        else
        {
            if (heldAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
                heldAmmo -= maxAmmo;
                isReloading = false;
            }
            else
            {
                currentAmmo = heldAmmo;
                heldAmmo = 0;
                isReloading = false;
            }
        } 
    }

    void Shoot()
    {
        muzzleFlash.Play();
        FindObjectOfType<AudioManager>().play("bullet");
        currentAmmo--;

        RaycastHit hit;
        if (name == "shotgun_1")
        {
            Vector3 pelletRotation = new Vector3(fpsCam.transform.forward.x, fpsCam.transform.forward.y, fpsCam.transform.forward.z);
            for(int i = 0; i < 10; i++)
            {
                pelletRotation.x += UnityEngine.Random.Range(-0.15f, 0.15f);
                pelletRotation.y += UnityEngine.Random.Range(-0.1f, 0.1f);
                if (Physics.Raycast(fpsCam.transform.position, pelletRotation, out hit, range))
                {
                    InfectedController ic = hit.transform.GetComponent<InfectedController>();

                    if (ic != null)
                    {
                        isRageModeOn = joelMeters.isRageModeOn(); ;
                        if (isRageModeOn)
                            ic.TakeDamage(damage * 2);
                        else
                            ic.TakeDamage(damage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }

                    GameObject impactGO = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 1f);
                }
            }
        }
        else
        {   
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                InfectedController ic = hit.transform.GetComponent<InfectedController>();

                if (ic != null)
                {
                    isRageModeOn = joelMeters.isRageModeOn();
                    if (isRageModeOn)
                        ic.TakeDamage(damage * 2);
                    else
                        ic.TakeDamage(damage);
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
     }
    
    public void AddAmmo()
    {
        if(currentAmmo < heldAmmo - 100)
        {
            currentAmmo += 100;
        }
        else
        {
            currentAmmo = heldAmmo;
        }
    }
}
