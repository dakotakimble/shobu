using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WeaponManager : NetworkBehaviour {
    [SerializeField]
    private string weaponLayerName = "weapon";

    [SerializeField]
    private Transform weaponHolder;

    public GameObject emitter;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    public PlayerWeapon secondaryWeapon;

    private PlayerWeapon currentWeapon;
	
	void Start ()
    {
        EquipWeapon(primaryWeapon);
	}
	
    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

       GameObject _weaponIns = (GameObject) Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        if(isLocalPlayer)
        {
            _weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

	void Update ()
    {
        //if (Input.GetKeyDown("2"))
        //{
        //    EquipWeapon(secondaryWeapon);
        //    primaryWeapon.graphics.SetActive(false);
        //}
       
	}
}
