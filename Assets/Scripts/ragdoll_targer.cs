using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;


public class ragdoll_targer : MonoBehaviour
{
    public int life = 0;
    //public GameObject enemySpawnHandler;
    //public GameObject spawnArea;
    //public GameObject Enemy;
    //public GameObject Player;
    //public bool enemyKilled = false;
    public int maxHp = 10;
    private int hp;



    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    void Start()
    {
        SetKinematic(true);
        hp = maxHp;
    }

    //RAYCAST METHOD
    //public float health = 50f;

    //public void TakeDamage(float amount)
    //{
    //    health -= amount;
    //    if (health <= 0f)
    //    {
    //        Die();
    //    }
    //}

    //void Die()
    //{
    //    Destroy(gameObject);

    //}

    //PROJECTILE METHOD

    public void OnCollisionEnter(Collision boom)
    {
        //If the object that triggered this collision is tagged "bullet"
        if (boom.gameObject.tag == "bullet")
        {
            //GetComponent<AudioSource>().Play();

            life ++;
            
            if (life == 2)
            {
                //Destroy(gameObject);
                Die();
            }

        }
    }




    //public void Damage(DamageInfo info)
    //{
    //    if (hp <= 0) return;
    //    hp -= info.damage;
    //    if (hp <= 0) Die();
    //}
    void Die()
    {
        Debug.Log("Die");
        Destroy(GetComponent<AICharacterControl>());
        Destroy(GetComponent<ThirdPersonCharacter>());
        Destroy(GetComponent<NavMeshAgent>());


        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 15f);

        //enemyKilled = true;
        //if (enemyKilled == true)
        //{
        //    SpawnEnemy();
        //}
    }

    //spawn an enemy each time one is killed WORKING 7/4/18
    //void SpawnEnemy()
    //{
    //    enemyKilled = false;
    //    life = 0;
    //    enemySpawnHandler = Instantiate(Enemy, enemySpawnHandler.transform.position, enemySpawnHandler.transform.rotation) as GameObject;
    //    Enemy.GetComponent<AICharacterControl>().target = Player.transform;
    //    Enemy.GetComponent<Target>().enemySpawnHandler = spawnArea;
    //    Enemy.GetComponent<Target>().Enemy = Enemy;
    //    Enemy.GetComponent<Target>().Player = Player;

    //}




}




