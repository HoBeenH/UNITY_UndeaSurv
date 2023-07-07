using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Script.Helper;
using Script.Table;
using Script.Table.Base;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ConvertCsvToBytes : EditorWindow
    {
        private const string CSV_PATH = "Assets/Table/";
        private const string SAVE_PATH = "Assets/Resources/Table/TableData.asset";
        
        [MenuItem("Custom/ConvetCsvToSO")]
        public static void Init()
        {
            var _window = GetWindow<ConvertCsvToBytes>();
            _window.Convert();
            // _window.Close();
        }

        [MenuItem("Custom/Test")]
        private static void Temp234()
        { 
            var _temp = Resources.Load<TableObject>("Table/TableData");
            D.Assert(_temp);
            D.L(_temp.GetTable<EnemyTable>().ToString());
            D.L(_temp.GetTable<WeaponTable>().ToString());
            D.L(_temp.GetTable<ConfigTable>().ToString());
        }
        
        private void Convert()
        {
            var _csvPath = CSV_PATH;
            var _allTable = Directory.GetFiles(_csvPath, "*.csv");
            var _tableBase = ScriptableObject.CreateInstance<TableObject>();
            
            // All Table Convert
            foreach (var _path in _allTable)
            {
                D.L(_path);
                var _asset = AssetDatabase.LoadAssetAtPath<TextAsset>(_path);
                D.Assert(_asset != null, "경로에 CSV 없음");
                if (_asset == null)
                    continue;

                var _fileName = Path.GetFileName(_path);
                D.L($"File Name {_fileName}");
                if (string.IsNullOrEmpty(_fileName))
                    continue;

                var _type = GetType(_fileName.Replace(".csv", string.Empty));
                D.Assert(_type != null, "Type 못가져옴");
                if (_type == null)
                    continue;
                
                var _table = ScriptableObject.CreateInstance(_type);
                D.Assert(_table != null, "Table Base 생성 불가");
                if (_table == null)
                    continue;

                _table.name = _type.Name;
                _table.hideFlags = HideFlags.None;
                
                var _data = GetData(_asset);
                D.Assert(_data != null, "딕셔너리 변환 불가");
                if (_data == null)
                    continue;

                var _mi = _type.GetMethod("SetTableOnlyEditor");
                D.Assert(_mi != null,"SetData 가 없음.");
                if (_mi == null)
                    continue;
                
                _mi.Invoke(_table, new object[] { _data });
                _tableBase.AddData(_table);
            }

            var _fullPath = $"{Application.dataPath}/Resources/Table/TableData.asset";

            if (File.Exists(_fullPath))
                File.Delete(_fullPath);
            
            AssetDatabase.CreateAsset(_tableBase, SAVE_PATH);
            foreach (var _so in _tableBase.TableList)
                AssetDatabase.AddObjectToAsset(_so, _tableBase);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private Type GetType(string typeName)
        {
            D.L($"{typeName}");
            var _type = Type.GetType($"Script.Table.{typeName}, assembly-csharp.dll");
            
            if (_type == null)
            {
                if (typeName.Contains(".") == true)
                {
                    _type = Type.GetType(typeName);
                    if (_type != null)
                        return _type;
                    
                    string assemName = typeName.Substring(0, typeName.IndexOf('.'));
                    Assembly assembly = Assembly.Load(assemName);

                    if (assembly == null)
                        return null;

                    if ((_type = assembly.GetType(typeName)) != null)
                        return _type;
                }

                if (Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).Where(assembly => assembly != null).Any(assembly => (_type = assembly.GetType(typeName)) != null))
                    return _type;
            }
            else
                return _type;

            return null;
        }
        
        private Dictionary<string, List<string>> GetData(TextAsset asset)
        {
            var _str = asset.text;
            
            var _strAllLine = _str.Split(Environment.NewLine);
            var _strLineCnt = _strAllLine.Length;

            var _tableColumNameArr = _strAllLine[0].Split(',');
            
            var _table = new Dictionary<string, List<string>>();
            var _ignoreIdx = new HashSet<int>();
            for (var i = 0; i < _tableColumNameArr.Length; i++)
            {
                var _cur = _tableColumNameArr[i];
                if (_cur.Contains('_'))
                {
                    _ignoreIdx.Add(i);
                }

                _table.Add(_cur, new List<string>());
            }
            
            var _typeCnt = _tableColumNameArr.Length - _ignoreIdx.Count;

            for (var i = 1; i < _strLineCnt; i++)
            {
                var _split = _strAllLine[i].Replace("\r\n", string.Empty).Split(',');
                if (_split == null || _split.Length == 0 || string.IsNullOrEmpty(_split[0]))
                    continue;

                for (var j = 0; j < _typeCnt; j++)
                {
                    if (_ignoreIdx.Contains(j))
                        continue;
                    _table[_tableColumNameArr[j]].Add(_split[j]);
                }
            }

            return _table;
        }
    }
}
