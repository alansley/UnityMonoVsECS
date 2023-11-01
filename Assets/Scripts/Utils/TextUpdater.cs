using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private int _monoBehaviourSceneIndex = 0;
    [SerializeField] private int _dotsEcsSceneIndex = 1;

    [SerializeField] private TextMeshProUGUI _fpsLabel;
    [SerializeField] private float _updateIntervalSecs = 0.2f;

    [SerializeField] private TextMeshProUGUI _objectCountLabel;

    [SerializeField] private TextMeshProUGUI _loadOtherSceneButtonText;

    private float _cumulativeTime = 0f;

    private int _currentObjectCount;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        UpdateObjectCountText();
        UpdateLoadSceneButtonText();
    }

    /// <summary>
    /// Unity Update hook.
    /// </summary>
    void Update()
    {
        float deltaTime = Time.deltaTime;
        _cumulativeTime += deltaTime;

        if (_cumulativeTime >= _updateIntervalSecs)
        {
            float fps = 1.0f / deltaTime;
            float frameTimeMS = deltaTime * 1000f;
            _fpsLabel.text = "FPS: " + fps.ToString("#.##") + ", Frame time: " + frameTimeMS.ToString("#.##") + "ms";
            _cumulativeTime = 0f;
        }
    }

    public void UpdateLoadSceneButtonText()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == _monoBehaviourSceneIndex)
        {
            _loadOtherSceneButtonText.text = "Load DOTS/ECS Scene";
        }
        else
        {
            _loadOtherSceneButtonText.text = "Load MonoBehaviour Scene";
        }
    }

    public void UpdateObjectCountText()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == _monoBehaviourSceneIndex)
        {
            _currentObjectCount = GameObject.FindGameObjectsWithTag("MonoObject").Length;
        }
        else
        {
            _currentObjectCount = GameObject.FindGameObjectsWithTag("DotsEcsEntity").Length;
        }

        _objectCountLabel.text = "Current object count: " + _currentObjectCount.ToString("N0");
    }
}