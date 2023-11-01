using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSceneOpener : MonoBehaviour
{
    [SerializeField] private SceneReference _foo;

    [SerializeField] private SubScene _subSceneToLoad;

    private SceneSystem _sceneSystem;

    private Entity _subSceneEntity;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("ECSParticlesSubScene", LoadSceneMode.Additive);

        /*
        var world = World.DefaultGameObjectInjectionWorld.Unmanaged;

        var sceneSystemLoadParameters = new SceneSystem.LoadParameters { Flags = SceneLoadFlags.NewInstance };
        var meta = SceneSystem.LoadSceneAsync(world, _subSceneToLoad.SceneGUID, sceneSystemLoadParameters);



        SceneSystem.UnloadScene(world, _subSceneToLoad.SceneGUID);
        */


        //var wang = world.EntityManager.Instantiate(meta);

        /*

        SceneManager.LoadScene(_subSceneToLoad.EditingScene, LoadSceneMode.Additive);



        var world = World.DefaultGameObjectInjectionWorld.Unmanaged;
        var sceneSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<SceneSystem>();
        var sceneSystemLoadParameters = new SceneSystem.LoadParameters { Flags = SceneLoadFlags.NewInstance };

        var stateptr = world.GetExistingSystemState<SceneSystem>();//(.Unmanaged.ResolveSystemState(sceneSystem);
        if (stateptr.SystemHandle != null)
        {

            //var loadParams = new SceneSystem.LoadParameters { Flags = flags };

            var sceneEntity = SceneSystem.LoadSceneAsync(world, _subSceneToLoad.SceneGUID, sceneSystemLoadParameters);
            stateptr.EntityManager.AddComponentObject(sceneEntity, this);
            //_AddedSceneGUID = _SceneGUID;
        }


        */

        //var world = World.DefaultGameObjectInjectionWorld.Unmanaged;
        //SceneSystem.LoadSceneAsync(world, _subSceneToLoad.SceneGUID);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}