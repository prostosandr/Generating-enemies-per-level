using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3 GetRandomDirection()
    {
        float minNumber = -1f;
        float maxNumber = 1f;

        float randomNumber = Random.Range(minNumber, maxNumber);

        return new Vector3(randomNumber, randomNumber, randomNumber);
    }
}