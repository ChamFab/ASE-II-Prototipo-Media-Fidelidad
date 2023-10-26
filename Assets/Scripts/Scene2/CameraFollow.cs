using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = player.position + offset;
        Vector3 targetPos = player.position + offset; // crea la variable local de Target Pos
        targetPos.x = 0;
        transform.position = targetPos;
    }
}
