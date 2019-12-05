using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

    //scenes
    public const string SceneBattle = "Battle";
    public const string SceneMenu = "MainMenu";

    //Enemy Types
    public const string RedRobot = "RedRobot";
    public const string BlueRobot = "BlueRobot";
    public const string YellowRobot = "YellowRobot";

    //Gun Types
    public const string Pistol = "Pistol";
    public const string Shotgun = "Shotgun";
    public const string AssaultRifle = "AssaultRifle";
    public const string SniperRifle = "SniperRifle";
    public const string SawedOff = "Shotgun2";
    public const string SubMachineGun = "SubMachine";
    public const string LaserRifle = "LaserRifle";

    //pickup types
    public const int PickUpPistolAmmo = 1;
    public const int PickUpAssaultAmmo = 2;
    public const int PickUpShotgunAmmo = 3;
    public const int PickUpHealth = 4;
    public const int PickUpArmor = 5;

    //Misc
    public const string Game = "Game";
    public const float CameraDefaultZoom = 60f;

    public static readonly int[] AllPickupTypes = new int[5]
    {
        PickUpPistolAmmo,
        PickUpAssaultAmmo,
        PickUpShotgunAmmo,
        PickUpHealth,
        PickUpArmor
    };





	
}
