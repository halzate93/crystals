using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 7.5f, 0f);

    public static FollowTarget Instance
    {
        get; private set;
    }

    public Transform Target
    {
        get; set;
    }

    private void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy (this);
    }

    private void LateUpdate()
    {
        if (Target != null)
            transform.position = Target.position + offset;
    }
}

