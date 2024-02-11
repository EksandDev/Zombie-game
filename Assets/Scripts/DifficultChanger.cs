using UnityEngine;

public class DifficultChanger : MonoBehaviour
{
    [SerializeField] private ZombieSpawner _zombieSpawner;
    [SerializeField] private Zombie[] _zombies;

    private int _priceForSpawnTime = 100;
    private int _priceForHordeSpawn = 1000;
    private float _minTimeToSpawn = 0.5f;

    private void FixedUpdate()
    {
        DecreaseTimeToSpawn();
        HordeSpawn();
    }

    private void DecreaseTimeToSpawn()
    {
        if (ScoreAndMoney.Score >= _priceForSpawnTime && _zombieSpawner.TimeToSpawn >= _minTimeToSpawn)
        {
            _zombieSpawner.TimeToSpawn -= 0.1f;
            _priceForSpawnTime += 100;
        }
    }

    private void HordeSpawn()
    {
        if (ScoreAndMoney.Score >= _priceForHordeSpawn)
        {
            _zombieSpawner.HordeSpawn();
            _priceForHordeSpawn += 1000;
        }
    }
}
