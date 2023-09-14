using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Data.ValueObjects
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> PoolDatas;
    }
}