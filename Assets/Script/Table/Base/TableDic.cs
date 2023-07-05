using RotaryHeart.Lib.SerializableDictionary;

namespace Script.Table.Base
{
    [System.Serializable]
    public class TableDic<T> : SerializableDictionaryBase<int, T> where T : class, new()
    {
    }
}
