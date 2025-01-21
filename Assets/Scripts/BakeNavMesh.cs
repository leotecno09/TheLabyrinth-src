using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class BakeNavMesh : MonoBehaviour
{
    public MazeGenerator mazeGenerator;
    public NavMeshSurface navMeshSurface;
    public GameObject enemy;
    public GameObject player;
    //public Transform enemySpawnPosition;
    private GameObject enemySpawnPoint;
    private GameObject playerSpawnPoint;
    public GameObject paperSheet;

    private bool navMeshBaked = false;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.FindWithTag("PlayerSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshBaked) {
            if (mazeGenerator.IsMazeReady()) {
                navMeshSurface.BuildNavMesh();

                navMeshBaked = true;

                Debug.Log("NavMesh Bake eseguito con successo.");

                //enemy.SetActive(true);
                //enemySpawnPoint = GameObject.FindWithTag("EnemySpawnPoint");
                //enemySpawnPosition = enemySpawnPoint.transform.position;

                //Instantiate(enemy, enemySpawnPoint.transform.position, Quaternion.identity); // Spawna enemy
                
                //enemy.transform.position = enemySpawnPoint.transform.position;
                //enemy.transform.rotation = enemySpawnPoint.transform.rotation;
                enemy.SetActive(true);               
                player.transform.position = playerSpawnPoint.transform.position;
                paperSheet.transform.position = playerSpawnPoint.transform.right * 10f;
                player.SetActive(true);
                Debug.Log("Player ok");
                
                
            }
        }

    }

    /*void Awake() {
        if (gameObject.CompareTag("Player")) {
            player.transform.position = playerSpawnPoint.transform.position;
            Debug.Log("Player spostato allo spawn");
        }

    }*/

    public bool IsNavMeshBaked() {
        return navMeshBaked;
    }

}
