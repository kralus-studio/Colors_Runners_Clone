using UnityEngine;

public class GunPoolPhysicsController: MonoBehaviour
{

    #region Self Variables
    
    #region Public Variables
    
    public bool IsTruePool = false;

    #endregion
    
    #region Serialized Variables
    
    [SerializeField] GunPoolManager manager;
    
    #endregion
    
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsTruePool.Equals(true))
            {
                manager.StopAsyncManager();
            }
            else
            {
                manager.StartAsyncManager();
            }
        }
    }
}
