using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public int kills;
    public int deaths;

    public MatchSettings matchSettings;
    public delegate void OnPlayerKilledCallback(string player, string source);
    public OnPlayerKilledCallback onPlayerKilledCallback;


#region Player tracking 


private const string PLAYER_ID_PREFIX = "Player";
private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }


    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }


    /*void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string _playerID in players.Keys)
        {
            GUILayout.Label(_playerID + " - " + players[_playerID].transform.name + "-" + "Deaths" + deaths);
        }



        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/

    #endregion

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Game Manager in Scene.");

        }else
        {
            instance = this;
        }
        
    }

}
