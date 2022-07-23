using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Comparison : MonoBehaviour
{
    public GameObject EntityTemplate;
    public GameObject AnimatorTemplate;
    private Random r;
    public int Count;
    public bool UseEntity;

    void Start()
    {
        r = new Random((uint)UnityEngine.Random.Range(1,1000));
        for (int i = 0; i < Count; i++)
        {
            var pos = r.NextFloat3(-20, 20);
            pos.y = 0;
            Instantiate(UseEntity ? EntityTemplate : AnimatorTemplate, pos, quaternion.identity);
        }
    }
    
}
