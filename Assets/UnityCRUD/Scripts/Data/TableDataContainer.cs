using System;
using UnityEngine;

namespace UnityCRUD.Scripts.Data
{
    [Serializable]
    public class TableDataContainer
    {
        [SerializeField] public int tableIndex = default;
        [SerializeField] public string tableName = default;

        public TableDataContainer() {}

        public TableDataContainer(int index, string name)
        {
            tableIndex = index;
            tableName = name;
        }
    }
}