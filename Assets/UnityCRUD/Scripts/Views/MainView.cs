using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Data.ScriptableObjects;
using UnityCRUD.Scripts.Plugins;
using UnityEngine;

namespace UnityCRUD.Scripts.Views
{
    public class MainView : BaseView<MainView>, IView
    {
        public GameObject rowPrefab;
        public GameObject cellPrefab;
        public GameObject deletePrefab;
        public GameObject contentParent;
        public static int CurrentPage = default;
        public static string CurrentTableName = default;
        public int rowsPerPage = 20;
        public ServerSettings serverSettings;
        public static Color CurrentColor = default;
        public Color color1 = new Color(0.1529412f, 0.172549f, 0.2f);
        public Color color2 = new Color(0.08828762f, 0.09867091f, 0.1132075f);
        public async void InitializeTable(string tableName)
        {
            await ClearTable();
            UiElementsDataContainer.IsInputLocked = true;
            HeaderView.Instance.InitializeTable(tableName);
            CrudLogger.Log("Initializing MainView", ServerSettings.DebuglevelEnum.Log);
            
            TableDataContainer tableDataContainer = DatabaseDataContainer.Columns
                .Where(x => x.Key.tableName == tableName)
                .ToDictionary(x => x.Key, x => x.Value)
                .Keys.ElementAt(0);
            
            await SpawnCoroutine(tableDataContainer, CurrentPage);
            CurrentTableName = tableName;
            base.Initialize();
            UiElementsDataContainer.IsInputLocked = false;
        }

        public async Task ClearTable()
        {
            Transform[] children = contentParent.GetComponentsInChildren<Transform>();
            await Task.Yield();
            for (var index = 0; index < children.Length; index++)
            {
                Transform child = children[index];
                if(child.gameObject.name != contentParent.name)
                {
                    child.gameObject.SetActive(false);
                    Destroy(child.gameObject, 7);
                }
            }
        }

        public async void ChangePage(int i)
        {
            UiElementsDataContainer.IsInputLocked = true;
            await ClearTable();
            CurrentPage = i;
            InitializeTable(CurrentTableName);
            UiElementsDataContainer.IsInputLocked = false;
        }

        private async Task SpawnCoroutine(TableDataContainer tableDataContainer, int page)
        {
            UiElementsDataContainer.IsInputLocked = true;
            int startingPage = rowsPerPage * page;
            for (int i = 0; i < rowsPerPage; i++)
            {
                GameObject row = Instantiate(rowPrefab, contentParent.transform);
                CurrentColor = CurrentColor == color1 ? color2 : color1;
                if (i % 2 == 0) await Task.Yield();
                List<RowDataContainer> tempList = new List<RowDataContainer>();
                if (i + startingPage >= 0 && DatabaseDataContainer.Rows[tableDataContainer].Count > i + startingPage)
                {
                    tempList = DatabaseDataContainer.Rows[tableDataContainer][i + startingPage]
                        .Where(x => x.rowIndex == i + startingPage).ToList();

                    for (int j = -1; j < tempList.Count; j++)
                    {
                        
                        if (j == -1)
                        {
                            GameObject cell = Instantiate(deletePrefab, row.transform);
                            DeleteElementController deleteElementController = cell.GetComponent<DeleteElementController>();
                            deleteElementController.connectedData = tableDataContainer;
                            deleteElementController.dataContainer =
                                new ColumnDataContainer(tempList[0].rowIndex, "X");
                            deleteElementController.rowId = tempList[0].rowData.ToInt();
                            deleteElementController.Initialize();
                        }
                        else
                        {
                            GameObject cell = Instantiate(cellPrefab, row.transform);
                            CellTextElementController cellTextElementController = cell.GetComponent<CellTextElementController>();
                            cellTextElementController.rowId = tempList[0].rowData.ToInt();
                            cellTextElementController.tableColumnName = DatabaseDataContainer.Columns.Where(x => x.Key.tableName == tableDataContainer.tableName)
                                .ToDictionary(x => x.Key, x => x.Value)
                                .Values.ElementAt(0)[j].columnName;
                            cellTextElementController.dataContainer =
                                new ColumnDataContainer(tempList[j].rowIndex, tempList[j].rowData);
                            cellTextElementController.Initialize();
                        }
                    }
                }
            }
            RebuildThisView();
            UiElementsDataContainer.IsInputLocked = false;
        }
    }
}