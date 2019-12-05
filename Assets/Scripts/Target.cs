using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;


public class Target : MonoBehaviour {
    public int life = 0;
    public GameObject enemySpawnHandler;
    public GameObject spawnArea;
    public GameObject Enemy;
    public GameObject Player;
    public bool enemyKilled = false;
    

    //PROJECTILE METHOD

    public void OnCollisionEnter(Collision boom)
    {
        //If the object that triggered this collision is tagged "bullet"
        if (boom.gameObject.tag == "bullet")
        {
            GetComponent<AudioSource>().Play();

            life += 1;
            if (life == 4)
                //Destroy(gameObject);
                Die();
            
        }
    }




   
    void Die()
    {      
        Destroy(gameObject, 1);

        enemyKilled = true;
        if(enemyKilled == true)
        {
            SpawnEnemy();
        }
    }

    //spawn an enemy each time one is killed WORKING 7/4/18
    void SpawnEnemy()
    {
        enemyKilled = false;
        life = 0;
        enemySpawnHandler = Instantiate(Enemy, enemySpawnHandler.transform.position, enemySpawnHandler.transform.rotation ) as GameObject;
        Enemy.GetComponent<AICharacterControl>().target = Player.transform;
        Enemy.GetComponent<Target>().enemySpawnHandler = spawnArea;
        Enemy.GetComponent<Target>().Enemy = Enemy;
        Enemy.GetComponent<Target>().Player = Player;
        
    }




}




