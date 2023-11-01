using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private int _monoBehaviourSceneIndex = 0;
    [SerializeField] private int _DotsEcsSceneIndex = 1;

    private int _currentSceneIndex;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    public void OnClickLoadMonoBehaviourScene()
    {
        SceneManager.LoadScene(_monoBehaviourSceneIndex);
    }

    public void OnClickLoadDotsEcsScene()
    {
        SceneManager.LoadScene(_DotsEcsSceneIndex);
    }

    public void OnClickRespawnFewMono()
    {
        if (_currentSceneIndex == _monoBehaviourSceneIndex)
        {
            var objects = GameObject.FindGameObjectsWithTag("MonoObject");
            foreach (var o in objects) { DestroyImmediate(o); }

            var spawner = FindObjectOfType<ParticleSpawnerMono>();
            spawner.SpawnFewObjects();

            var tu = FindObjectOfType<TextUpdater>();
            tu.UpdateObjectCountText();
        }
    }

    public void OnClickRespawnManyMono()
    {
        if (_currentSceneIndex == _monoBehaviourSceneIndex)
        {
            var objects = GameObject.FindGameObjectsWithTag("MonoObject");
            foreach (var o in objects) { DestroyImmediate(o); }

            var spawner = FindObjectOfType<ParticleSpawnerMono>();
            spawner.SpawnManyObjects();

            var tu = FindObjectOfType<TextUpdater>();
            tu.UpdateObjectCountText();
        }
    }

    public void OnClickRespawnLotsMono()
    {
        if (_currentSceneIndex == _monoBehaviourSceneIndex)
        {
            var objects = GameObject.FindGameObjectsWithTag("MonoObject");
            foreach (var o in objects) { DestroyImmediate(o); }

            var spawner = FindObjectOfType<ParticleSpawnerMono>();
            spawner.SpawnLotsObjects();

            var tu = FindObjectOfType<TextUpdater>();
            tu.UpdateObjectCountText();
        }


    }
}