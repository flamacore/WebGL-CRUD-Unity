using System;
using UnityEngine;

namespace UnityCRUD.Scripts.Data
{
    [Serializable]
    public class ColumnDataContainer
    {
        [SerializeField]
        public int columnIndex = default;
        [SerializeField]
        public string columnName = default;
        public ColumnDataContainer() {}

        public ColumnDataContainer(int index, string name)
        {
            columnIndex = index;
            columnName = name;
        }
    }
}