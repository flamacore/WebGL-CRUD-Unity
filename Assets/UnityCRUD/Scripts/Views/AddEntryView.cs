using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data;
using UnityEngine;

namespace UnityCRUD.Scripts.Views
{
    public class AddEntryView : BaseView<AddEntryView>, IView
    {
        public GameObject contentParent;
        public GameObject inputFieldPrefab;
        public List<GameObject> childrenInputfields = new List<GameObject>();
        
        public void InitializeField(TableDataContainer tableDataContainer)
        {
            childrenInputfields.Clear();
            for (int i = 1; i < DatabaseDataContainer.Columns[tableDataContainer].Count; i++)
            {
                GameObject inputField = Instantiate(inputFieldPrefab, contentParent.transform);
                childrenInputfields.Add(inputField);
                InputFieldElementController inputFieldElementController = inputField.GetComponent<InputFieldElementController>();
                inputFieldElementController.title =  DatabaseDataContainer.Columns[tableDataContainer][i].columnName;
                inputFieldElementController.Initialize();
            }
            gameObject.SetActive(true);
            RebuildThisView();
            base.Initialize();
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void ButtonClicked()
        {
            base.ButtonClicked();
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            for (int i = 0; i < childrenInputfields.Count; i++)
            {
                columns.Add(childrenInputfields[i].GetComponent<InputFieldElementController>().title);
                values.Add(childrenInputfields[i].GetComponent<InputFieldElementController>().inputField.text);
            }
            DatabaseDataContainer.InsertDataToTable(
                DatabaseDataContainer.Tables.Where(x => x.tableName == MainView.CurrentTableName).ToList()[0],
                columns.ToArray(), values.ToArray());
        }
    }
}