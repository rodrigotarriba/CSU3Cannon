using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CannonApp
{
    public interface IPoolObject
    {
        PoolObjectId PoolId { get; }
        void SetUp(Vector3 fireForce, ObjectPool objectPool);

        void Activate();
        void Deactivate();
    }
}