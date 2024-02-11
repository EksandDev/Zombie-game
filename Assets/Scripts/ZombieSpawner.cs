using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _zombiePrefabs;
    [SerializeField] private GameObject[] _spawnpoints;

    public float TimeToSpawn { get => _timeToSpawn; set => _timeToSpawn = value; }

    private float _timeToSpawn = 3f;
    private float _timer;

    private void Start()
    {
        _timer = _timeToSpawn;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer <= 0)
        {
            _timer = _timeToSpawn;
            Vector3 spawnpointPosition = _spawnpoints[Random.Range(0, _spawnpoints.Length)].transform.position;
            Instantiate(_zombiePrefabs[Casino()], spawnpointPosition, Quaternion.identity);
        }
    }

    private int Casino()
    {
        int randomInt = Random.Range(0, 100);

        if (GetDifficultLevel() == 0)
        {
            if (randomInt <= 20)
                return 1;

            else
                return 2;
        }

        else if (GetDifficultLevel() == 1)
        {
            if (randomInt >= 0 && randomInt <= 10)
                return 0;

            else if (randomInt >= 11 && randomInt <= 50)
                return 1;

            else
                return 2;
        }

        else if (GetDifficultLevel() == 2)
        {
            if (randomInt >= 0 && randomInt <= 20)
                return 0;

            else if (randomInt >= 21 && randomInt <= 70)
                return 1;

            else
                return 2;
        }

        else if (GetDifficultLevel() == 3)
        {
            if (randomInt < 50)
                return 0;

            else if (randomInt >= 50 && randomInt < 100)
                return 1;

            else
                return 3;
        }

        else
        {
            return 3;
        }
    }

    private int GetDifficultLevel()
    {
        if (ScoreAndMoney.Score < 1000)
            return 0;

        else if (ScoreAndMoney.Score >= 1000 && ScoreAndMoney.Score < 3000)
            return 1;

        else if (ScoreAndMoney.Score >= 3000 && ScoreAndMoney.Score < 6000)
            return 2;

        else if (ScoreAndMoney.Score >= 6000 && ScoreAndMoney.Score < 20000)
            return 3;

        else
            return 4;
    }

    public void HordeSpawn()
    {
        for (int i = 0; i < _spawnpoints.Length; i++)
        {
            Vector3 spawnpointPosition = _spawnpoints[i].transform.position;
            Instantiate(_zombiePrefabs[Casino()], spawnpointPosition, Quaternion.identity);
        }
    }
}
