using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }
    public PlayerShoot playerShoot;

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    public Text healthText;
    public Text ammoText;
    public GameObject body;
    

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    public string username = "Loading...";

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    


    public void Setup()
    {
       

        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length ; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();

    }

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string _sourceID)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health. ");

        if(currentHealth <= 0)
        {
            Die(_sourceID);
        }
    }

    private void Die(string _sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(_sourceID);
        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
            //GameManager.instance.onPlayerKilledCallback.Invoke(username, sourcePlayer.username);
        }
       
        deaths++;

        GameManager.instance.deaths += 1;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is dead.");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()        
    {
      
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);
       
        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        Debug.Log(transform.name + " respawned.");
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
        if (isLocalPlayer)
        {
            body.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        
        
        
        isDead = false;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if(_col != null)        
            _col.enabled = true;
        
    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            healthText.text = currentHealth.ToString() + "%";
            ammoText.text = "[" + playerShoot.bullets.ToString() + "/" + playerShoot.maxBullets.ToString() + "]";
        }
    }

}
