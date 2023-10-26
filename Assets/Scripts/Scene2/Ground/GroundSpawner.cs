using UnityEngine;
using UnityEngine.UIElements;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

     public void SpawnTile()
    {
         GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity); //reconoce el objeto temporal que es temp o temporary 
         nextSpawnPoint = temp.transform.GetChild(1).transform.position; //Obtiene el objeto vac�o hijo mediante la jerarquia de Unity, compara esto con la posici�n.

    }

    // Start is called before the first frame update
    private void Start()
    {
        //usando la funci�n for se pueden crear varias instancias 
        for (int i = 0; i < 5; i++) {
            SpawnTile();
        }

    }


}
