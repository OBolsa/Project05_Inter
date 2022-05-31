using UnityEngine;

namespace BehaviourTree
{
    public abstract class BRTree : MonoBehaviour
    {
        private BTNode _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        protected void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract BTNode SetupTree();
    }

}