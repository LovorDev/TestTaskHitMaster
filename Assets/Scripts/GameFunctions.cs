using ShootingSpotsScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctions : MonoBehaviour
{
    [SerializeField]
    private ActionIslandsQueue _actionIslandsQueue;

    private void Start()
    {
        _actionIslandsQueue.AllIslandsComplete += Restart;
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
