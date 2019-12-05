using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;

//[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    //private PlayerWeapon currentWeapon;
    //private GunEquipper currentWeapon;

    public Vector3 aimPos_Laser;
    public Vector3 aimPos_AR;
    public Vector3 aimPos_SMG;
    public Vector3 hipRecoilPosLaser;
    public Vector3 hipRecoilPosAR;
    public Vector3 hipRecoilPosSMG;
    public Vector3 aimRecoilPosLaser;
    public Vector3 aimRecoilPosAR;
    public Vector3 aimRecoilPosSMG;
    public Vector3 hipPos;
    public Vector3 normalPos;
    public Vector3 aimRecoilPos;
    public Vector3 hipRecoilPos;
    public AudioClip reloadSound;

    public List<Vector3> aimPoses;
    //this list has the corresponding ADS poses for each gun--
   public Animator armsAnimator;

    public bool isAimed = false;
    public bool isRunning = false;
    public float smooth = 5f;
    public float speed = 0.2f;
    public int damage = 10;
    public float range = 1000f;
    public GameObject weaponHolder;
    public GameObject muzzle;

    public float reloadTime = 3f;
    public bool isReloading = false;
    public int bullets;
    public int maxBullets;
    public int maxBulletsPerMag;

    

    //public GameObject bulletEmitter;
    public GameObject bulletHole;
    public GameObject bulletImpact;
    public GameObject groundImpact;

    public AudioSource audioSource;
    public AudioClip laserSound;
    public GameObject activeGun;
    public GameObject muzzleFlash;
    public int zoom = 46;
    public int normal = 67;

    private const string PLAYER_TAG = "Player";
    public Animator smgAnimator;
  
    //private WeaponManager weaponManager;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;


    void Awake()
    {
        
        
    }


    void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        bullets = maxBulletsPerMag = 25;
        maxBullets = 500;

        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No Camera refferenced");
            this.enabled = false;
        }
        //weaponManager = GetComponent<WeaponManager>();
    }
    public void ADS()
    {
        armsAnimator.enabled = false;
        if (GameObject.FindWithTag("Laser"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, aimPos_Laser, Time.deltaTime * smooth);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * smooth);
            isAimed = true;

        }

        if (GameObject.FindWithTag("AssaultRifle"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, aimPos_AR, Time.deltaTime * smooth);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * smooth);
            isAimed = true;

        }

        if (GameObject.FindWithTag("SMG"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, aimPos_SMG, Time.deltaTime * smooth);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * smooth);
            isAimed = true;

        }
    }
    void Update()
    {

        if ((bullets <= 0) && (Input.GetKeyDown(KeyCode.R)) && (!isReloading))
        {
            Debug.Log("reload");
            Reload();
        }

        if (Input.GetAxis("Fire2")> 0)
        {
            ADS();
        }       

        else 
        {
            armsAnimator.enabled = true;
            //weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, hipPos, Time.deltaTime * smooth);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normal, Time.deltaTime * smooth);
            isAimed = false;
            //reset to normal position
        }

        if (Input.GetAxis("Fire3") > 0)
        {
            armsAnimator.Play("Run");
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        

        //if (currentWeapon.fireRate <= 0f)
        //{
        if (Input.GetButtonDown("Fire1"))
                {
                
                    Shoot();
                    armsAnimator.enabled = false;
                }
        
        //}

        
        //else
        //{
        //    if(Input.GetButtonDown("Fire1"))
        //    {
        //        InvokeRepeating("Shoot", 0f, 1f/currentWeapon. fireRate);
                
        //    }
        //    else if(Input.GetButtonUp("Fire1"))
        //    {
        //        CancelInvoke("Shoot");
        //    }
        //}
        
    }
#region ADS

    void AimRecoil()
    {
       
        //weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, aimRecoilPos, Time.deltaTime * smooth);
        if (GameObject.FindWithTag("Laser"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(aimPos_Laser, aimRecoilPosLaser, Time.deltaTime * smooth);
            
           
        }

        if (GameObject.FindWithTag("AssaultRifle"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(aimPos_AR, aimRecoilPosAR, Time.deltaTime * smooth);
            
           
        }

        if (GameObject.FindWithTag("SMG"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(aimPos_SMG, aimRecoilPosSMG, Time.deltaTime * smooth);
            
            
        }

        
       
    }


    void HipRecoil()
    {
        
        //Debug.Log("hipRecoil");
        //weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, hipRecoilPos, Time.deltaTime * smooth);
        if (GameObject.FindWithTag("Laser"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, hipRecoilPosLaser, Time.deltaTime * smooth);
           
        }

        if (GameObject.FindWithTag("AssaultRifle"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, hipRecoilPosAR, Time.deltaTime * smooth);
            
            
        }

        if (GameObject.FindWithTag("SMG"))
        {
            weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, hipRecoilPosSMG, Time.deltaTime * smooth);
            
           
        }
      



    }
#endregion
    #region Shooting
    [Client]
    void Shoot()
    {
        
        
        if(bullets <= 0)
        {
            Debug.Log("Out of Ammo");
           

            return;
        }

        

        bullets--;

        if (!isAimed)
        {
            
            HipRecoil();//hip fire recoil 
        }
        else
        
        if (isAimed)
        {
            AimRecoil();//aim down sights recoil
           
        }
       

        

        //Debug.Log("shooting");
        audioSource.clip = laserSound;
        audioSource.Play();
        var _muzzleFlash = Instantiate(muzzleFlash, weaponHolder.transform.position, weaponHolder.transform.rotation);
        Destroy(_muzzleFlash, .01f);

        
        RaycastHit _hit;
        

        Debug.DrawRay(cam.transform.position, cam.transform.forward*30,Color.red);
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, range))
        {

           // Debug.Log(_hit.transform.name);

            if(_hit.collider.tag != PLAYER_TAG)
            {
                GameObject bulletHoleHandler;
                //GameObject impactHandler;
                var hitRotation = Quaternion.FromToRotation(Vector3.up, _hit.normal);
                //impactHandler = Instantiate(bulletImpact, _hit.point, hitRotation);
                bulletHoleHandler = Instantiate(bulletHole, _hit.point, hitRotation);
                Destroy(bulletHoleHandler, 5f);
                //Destroy(impactHandler, 5f);
            }

            if (_hit.collider.tag == PLAYER_TAG)
            {
                
                GameObject impactHandler;
                var hitRotation = Quaternion.FromToRotation(Vector3.up, _hit.normal);
                impactHandler = Instantiate(bulletImpact, _hit.point, hitRotation);
                
                Destroy(impactHandler, 2f);


                Debug.Log("player hit");
                CmdPlayerShot(_hit.collider.name, damage, transform.name);
            }

            if (_hit.collider.tag == "Ground")
            {

                GameObject impactHandler;
                var hitRotation = Quaternion.FromToRotation(Vector3.down, _hit.normal);
                impactHandler = Instantiate(groundImpact, _hit.point, hitRotation);

                Destroy(impactHandler, 2f);                
            }
        }
    }

    #endregion

#region Reloading

    public void Reload()
    {
        if (isReloading)
            return;
        StartCoroutine(Reload_Coroutine());

    }

    private IEnumerator Reload_Coroutine()
    {
        isReloading = true;
        audioSource.clip = reloadSound;
        audioSource.Play();
        //works - 11/6
        if (GameObject.FindWithTag("Laser"))
        {
            armsAnimator.Play("Reload");
        }

        if (GameObject.FindWithTag("AssaultRifle"))
        {
            armsAnimator.Play("mp5Reload");
        }

        if (GameObject.FindWithTag("SMG"))
        {
            armsAnimator.Play("smgReload");
        }
        
        Debug.Log("Reloading");
        yield return new WaitForSeconds(1.5f);

        bullets = maxBulletsPerMag;
        maxBullets -= maxBulletsPerMag;

        isReloading = false;
    }

#endregion

    [Command]
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + " has been shot.");

        GameManager.GetPlayer(_playerID);

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, _sourceID);

    }
    
}
