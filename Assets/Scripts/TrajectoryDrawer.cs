using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryDrawer : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int _maxIterations = 50;
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private List<GameObject> _obstacles = new List<GameObject>();

    private Scene _currentScene;
    private Scene _predictionScene;
    private PhysicsScene _currentPhysicsScene;
    private PhysicsScene _predictionPhysicsScene;
    private GameObject _discCopy = null;
    private List<GameObject> _wallCopies = new List<GameObject>();
    #endregion

    #region Methods
    private void Start()
    {
        Physics.autoSimulation = false;

        _currentScene = SceneManager.GetActiveScene();
        _currentPhysicsScene = _currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        _predictionScene = SceneManager.CreateScene("TrajectionPrediction", parameters);
        _predictionPhysicsScene = _predictionScene.GetPhysicsScene();

        foreach(GameObject obstacle in _obstacles)
        {
            if (obstacle.GetComponent<Collider>() == null)
            {
                continue;
            }

            GameObject copy = Instantiate(obstacle);
            copy.transform.position = obstacle.transform.position;
            copy.transform.rotation = obstacle.transform.rotation;

            Renderer renderer = copy.GetComponent<Renderer>();
            if (renderer == null)
            {
                continue;
            }

            renderer.enabled = false;

            SceneManager.MoveGameObjectToScene(copy, _predictionScene);
        }
    }
    private void FixedUpdate()
    {
        if (_currentPhysicsScene.IsValid())
        {
            // to move somewhere else
            _currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void DrawTrajectory(GameObject disc, Vector3 force, ForceMode mode)
    {
        if (!_currentPhysicsScene.IsValid() || !_predictionPhysicsScene.IsValid())
        {
            return;
        }

        if (_discCopy == null)
        {
            _discCopy = Instantiate(disc.gameObject);
            SceneManager.MoveGameObjectToScene(_discCopy, _predictionScene);
        }

        _discCopy.transform.position = disc.transform.position;
        _discCopy.GetComponent<Rigidbody>().AddForce(force, mode);
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = _maxIterations;

        for (int i = 0; i < _maxIterations; ++i)
        {
            _predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, _discCopy.transform.position);
        }

        Destroy(_discCopy);
    }
    public void Clear()
    {
        _lineRenderer.positionCount = 0;
    }
    #endregion
}
