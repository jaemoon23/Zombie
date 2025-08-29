using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public float rangeX = 22.0f;
    public float rangeZ = 22.0f;
    public float interval = 1f;
    public float destroyTime = 3f;
    public GameObject[] itemPrefab;

    private float time;

    private void Awake()
    {
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        
        if (time >= interval)
        {
            if (RandomPoint(transform.position, rangeX, rangeZ, out Vector3 point))
            {
                //Debug.DrawRay(point, Vector3.up, Color.red, 5.0f);
                point.y += 0.5f;
                var item = Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)], point, Quaternion.identity);
                Destroy(item, destroyTime);
            }
            time = 0f;
        }
    }

    public bool RandomPoint(Vector3 center, float rangeX, float rangeZ, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            // 설정해둔 범위 내에 포지션 하나 선정
            float x = Random.Range(-rangeX, rangeX);
            float z = Random.Range(-rangeZ, rangeZ);
            Vector3 randomPoint = new Vector3(center.x + x, center.y, center.z + z);

            // NavMeshHit hit에 찾아온 포지션이 담김 메시 지역이 아니면 false리턴
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    
}
