using UnityEngine;

namespace Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        public abstract System.Type ManagerType { get; }
    }
}