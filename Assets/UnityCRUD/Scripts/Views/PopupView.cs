using System;
using System.Linq;
using TMPro;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data;
using UnityEngine;

namespace UnityCRUD.Scripts.Views
{
    public class PopupView : BaseView<PopupView>, IView
    {
        public GameObject stringEditField;
        public GameObject numberEditField;
        public GameObject checkmarkEditField;
        public GameObject dropdownEditField;
        public static IMainTableElementController.FieldType CurrentActiveField;
        public static string InitiatedColumnName = default;
        public static int InitiatedRowId = default;

        public void InitializeField(IMainTableElementController.FieldType fieldType,
            MainTableElementController elementController, string initiatedColumnName, int initiatedRowId)
        {
            InitiatedColumnName = initiatedColumnName;
            InitiatedRowId = initiatedRowId;
            switch (fieldType)
            {
                case IMainTableElementController.FieldType.TextBox:
                    stringEditField.GetComponentInChildren<TMP_InputField>().text =
                        ((CellTextElementController) elementController).dataContainer.columnName;
                    stringEditField.SetActive(true);
                    break;
                case IMainTableElementController.FieldType.Number:
                    numberEditField.SetActive(true);
                    break;
                case IMainTableElementController.FieldType.CheckMark:
                    checkmarkEditField.SetActive(true);
                    break;
                case IMainTableElementController.FieldType.Dropdown:
                    dropdownEditField.SetActive(true);
                    break;
                default:
                    break;
            }

            CurrentActiveField = fieldType;
            gameObject.SetActive(true);
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void ButtonClicked()
        {
            base.ButtonClicked();
            DatabaseDataContainer.UpdateTableData(
                DatabaseDataContainer.Tables.Where(x => x.tableName == MainView.CurrentTableName).ToList()[0],
                InitiatedColumnName, 
                stringEditField.GetComponentInChildren<TMP_InputField>().text, 
                InitiatedRowId);
        }
    }
}