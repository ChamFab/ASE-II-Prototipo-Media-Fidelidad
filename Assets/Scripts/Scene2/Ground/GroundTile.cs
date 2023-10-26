using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner; //accder otro script
   

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();//llama al código GroundSpawner
        SpawnObstacle();
    }
    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2);// destruir el Object dos segundos después de que lo toca 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject obstaclePrefab;
    void SpawnObstacle()
    {
        //choose a random point to spawn the obstacle 
        int obstacleSpawnIndex = Random.Range(2,5); //variable local spawn index
    
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        //spawn the obstacle at position
        //identifies if tiles destoy so they are not multiple amounts of obstacles
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform); //quaternion significa que no tendrá rotación
    }

}
