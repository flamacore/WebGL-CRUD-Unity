using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data.ScriptableObjects;
using UnityCRUD.Scripts.Plugins;
using UnityCRUD.Scripts.Views;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace UnityCRUD.Scripts.Data
{
    public static class DatabaseDataContainer
    {
        public static Dictionary<TableDataContainer, List<ColumnDataContainer>> Columns = new Dictionary<TableDataContainer, List<ColumnDataContainer>>();
        public static Dictionary<TableDataContainer, List<List<RowDataContainer>>> Rows = new Dictionary<TableDataContainer, List<List<RowDataContainer>>>();
        public static List<TableDataContainer> Tables = new List<TableDataContainer>();

        public static async Task GetTablesFromDatabaseTask()
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "GetTableNames");
            UnityWebRequest webRequest =
                UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local, Formatting = Formatting.None
            };
            JsonSerializer serializer = JsonSerializer.Create(settings);
            JsonReader reader = new JsonTextReader(new StringReader(webRequest.downloadHandler.text));
            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            tempDict = serializer.Deserialize<Dictionary<string, string>>(reader);
            if (tempDict != null)
                foreach (KeyValuePair<string, string> pair in tempDict)
                {
                    Tables.Add(new TableDataContainer(pair.Key.ToInt(), pair.Value));
                }
        }

        public static async Task GetColumnsFromTableTask(TableDataContainer tableDataContainer)
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "GetColumnNames");
            form.AddField("TableName", tableDataContainer.tableName);
            UnityWebRequest webRequest = UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local, Formatting = Formatting.None
            };
            JsonSerializer serializer = JsonSerializer.Create(settings);
            JsonReader reader = new JsonTextReader(new StringReader(webRequest.downloadHandler.text));
            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            tempDict = serializer.Deserialize<Dictionary<string, string>>(reader);
            List<ColumnDataContainer> columnDataContainers = new List<ColumnDataContainer>();
            if (tempDict != null)
                foreach (KeyValuePair<string, string> pair in tempDict)
                {
                    columnDataContainers.Add(new ColumnDataContainer(pair.Key.ToInt(), pair.Value));
                }
            Columns.Add(tableDataContainer, columnDataContainers);
        }
        
        public static async Task GetRowDataFromTableTask(TableDataContainer tableDataContainer)
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "GetDataFromTable");
            form.AddField("TableName", tableDataContainer.tableName);
            UnityWebRequest webRequest = UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local, Formatting = Formatting.None
            };
            JsonSerializer serializer = JsonSerializer.Create(settings);
            JsonReader reader = new JsonTextReader(new StringReader(webRequest.downloadHandler.text));
            Dictionary<string, Dictionary<string, string>> tempDict = new Dictionary<string, Dictionary<string, string>>();
            tempDict = serializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(reader);
            List<List<RowDataContainer>> rowContainers = new List<List<RowDataContainer>>();
            List<RowDataContainer> rowDataContainers = new List<RowDataContainer>();
            foreach (KeyValuePair<string,Dictionary<string,string>> keyValuePair in tempDict)
            {
                foreach (KeyValuePair<string,string> valuePair in keyValuePair.Value)
                {
                    CrudLogger.Log(valuePair.Key + ":" + valuePair.Value + "+" + keyValuePair.Key.ToInt());
                    rowDataContainers.Add(new RowDataContainer(valuePair.Key, valuePair.Value, tableDataContainer.tableName, keyValuePair.Key.ToInt()));
                }
                rowContainers.Add(rowDataContainers);
            }
            Rows.Add(tableDataContainer, rowContainers);
        }
        
        public static async Task DeleteDataFromTableTask(TableDataContainer tableDataContainer, int id)
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "DeleteDataFromTable");
            form.AddField("TableName", tableDataContainer.tableName);
            form.AddField("id", id);
            UnityWebRequest webRequest = UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            await ReInitialize();
        }

        public static async Task UpdateTableData(TableDataContainer tableDataContainer, string columnName,
            string dataToUpdate, int id)
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "UpdateTableData");
            form.AddField("TableName", tableDataContainer.tableName);
            form.AddField("Columns[]", columnName);
            form.AddField("Values[]", dataToUpdate);
            form.AddField("id", id);
            UnityWebRequest webRequest = UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            await ReInitialize();
        }
        
        public static async Task InsertDataToTable(TableDataContainer tableDataContainer, string[] columns,
            string[] dataToInsert)
        {
            WWWForm form = new WWWForm();
            form.AddField("FunctionToCall", "InsertDataToTable");
            form.AddField("TableName", tableDataContainer.tableName);
            foreach (string column in columns)
            {
                form.AddField("Columns[]", column);
            }

            foreach (string s in dataToInsert)
            {
                form.AddField("Values[]", s);
            }
            UnityWebRequest webRequest = UnityWebRequest.Post(MainView.Instance.serverSettings.ServerURL + "UnityCRUD/Access.php", form);
            await webRequest.SendWebRequest();
            CrudLogger.Log("Received response from server:" + webRequest.downloadHandler.text);
            await ReInitialize();
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async Task Initialize()
        {
            Columns.Clear();
            Tables.Clear();
            Rows.Clear();
            Task.Yield();
            CrudLogger.Log("Start Getting Tables...", ServerSettings.DebuglevelEnum.Log);
            await GetTablesFromDatabaseTask();
            CrudLogger.Log("Done Getting Tables!", ServerSettings.DebuglevelEnum.Log);
            SideBarView.Instance.Initialize();
            foreach (TableDataContainer table in Tables)
            {
                //CrudLogger.Log(table.tableIndex + ":" + table.tableName);
                CrudLogger.Log($"Start Getting Columns for Table: {table.tableName}...", ServerSettings.DebuglevelEnum.Log);
                await GetColumnsFromTableTask(table);
                CrudLogger.Log($"Done Getting Columns for Table: {table.tableName}...", ServerSettings.DebuglevelEnum.Log);
                CrudLogger.Log($"Start Getting Rows for Table: {table.tableName}...", ServerSettings.DebuglevelEnum.Log);
                await GetRowDataFromTableTask(table);
            }
            PopupView.Instance.gameObject.SetActive(false);
            ConfirmationView.Instance.gameObject.SetActive(false);
        }
        

        private static async Task ReInitialize()
        {
            await Initialize();
            await SideBarView.Instance.ClearTable();
            await MainView.Instance.ClearTable();
            MainView.Instance.InitializeTable(MainView.CurrentTableName);
            SideBarView.Instance.Initialize();
        }
    }
}
