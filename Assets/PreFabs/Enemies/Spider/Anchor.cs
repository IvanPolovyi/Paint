using UnityEngine;

namespace PreFabs.Enemies.Spider
{
    public class Anchor : MonoBehaviour
    {

        void Update()
        {
            transform.position += Vector3.up*0.1f;
        }
    }
}
