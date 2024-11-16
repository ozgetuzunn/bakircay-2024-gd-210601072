using UnityEngine;

public class MovingPlacementArea : MonoBehaviour
{
    public float speed = 2f; // Hareket hızı
    public float range = 3f; // Hareket aralığı
    public bool moveHorizontally = true; // Yatay mı, dikey mi hareket edecek?

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (moveHorizontally)
        {
            // Yatay hareket
            transform.position = startPos + new Vector3(Mathf.PingPong(Time.time * speed, range), 0, 0);
        }
        else
        {
            // Dikey hareket
            transform.position = startPos + new Vector3(0, 0, Mathf.PingPong(Time.time * speed, range));
        }
    }
}
