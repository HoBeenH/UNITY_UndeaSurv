using RotaryHeart.Lib.SerializableDictionary;

namespace Script.Table.Base
{
    [System.Serializable]
    public class IntDic<T> : SerializableDictionaryBase<int, T> where T : class, new()
    {
    }
}
