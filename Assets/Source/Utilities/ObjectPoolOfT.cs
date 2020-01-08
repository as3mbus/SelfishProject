using UnityEngine;

namespace A3.Utilities
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        #region Fields / Attributes

        /// <summary>
        /// transform container where pooled instance generated and resides in
        /// </summary>
        [SerializeField]
        protected Transform poolContainer = null;

        /// <summary>
        /// template object that going to be instantiated
        /// </summary>
        [SerializeField]
        private T objectToPool = null;

        /// <summary>
        /// number of item that going to be instantiated on init call
        /// </summary>
        [SerializeField]
        private int initialPoolAmount = 0;

        /// <summary>
        /// is pool expandable
        /// </summary>
        [SerializeField]
        private bool expandable = false;

        private bool _initiated;

        #endregion

        /// <summary>
        /// How Many Active Object Inside A pool
        /// </summary>
        public int ActiveObjectCount
        {
            get
            {
                int act = 0;
                foreach (Transform item in poolContainer)
                    if (item.gameObject.activeInHierarchy)
                        act++;
                return act;
            }
        }


        /// <summary>
        /// initiate method by populating object pool
        /// </summary>
        public void Init()
        {
            if (poolContainer.childCount >= initialPoolAmount) return;
            for (int i = 0; i < initialPoolAmount; i++)
                AddObjectPool();
        }

        #region Public Methods

        /// <summary>
        /// get inactive (gameobject scope) pooled instance, activate this gameobject beforehand to accordingly
        /// </summary>
        /// <returns>inactive </returns>
        public T GetPooledObject()
        {
            foreach (Transform obj in poolContainer)
                if (!obj.gameObject.activeInHierarchy)
                    return obj.GetComponent<T>();
            return expandable ? AddObjectPool() : null;
        }

        /// <summary>
        /// reset all pool by deactivate all instance (game object scope)
        /// </summary>
        public void ResetPool()
        {
            if (!poolContainer) return;
            foreach (Transform item in poolContainer)
                item.gameObject.SetActive(false);
        }

        protected virtual T AddObjectPool()
        {
            T newInstance = Instantiate(objectToPool, poolContainer);
            newInstance.gameObject.SetActive(false);
            return newInstance;
        }

        protected void Shuffle()
        {
            foreach (Transform child in poolContainer)
                child.SetSiblingIndex(Random.Range(0, poolContainer.childCount));
        }

        #endregion
    }
}