using UnityCRUD.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCRUD.Scripts.Views
{
    public class BaseView<T> : MonoBehaviourSingletonController<T> where T: Component, IView
    {
        public MainTableElementController[] tableLayoutElementControllers;        
        public event IView.OnInitializeCompletedDelegate OnInitializeCompleted;
        public event IView.OnButtonClickedDelegate OnButtonClicked;

        public virtual void Initialize()
        {
            tableLayoutElementControllers = transform.GetComponentsInChildren<MainTableElementController>();
            Populate();
        }
        
        public virtual void Populate()
        {
            foreach (MainTableElementController controller in tableLayoutElementControllers)
            {
                controller.OnLoadCompleted += NotifyLoadComplete;
                controller.Initialize();
            }
        }

        public void RebuildThisView()
        {
            foreach (RectTransform rectTransform in transform.GetComponentsInChildren<RectTransform>())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            }
        }
        public void NotifyLoadComplete(IMainTableElementController mainTableElementController)
        {
            CrudLogger.Log("Table Layout Element Load Data completed:" + mainTableElementController.LayoutElementGuid);
            int notComplete = 0;
            foreach (MainTableElementController controller in tableLayoutElementControllers)
            {
                if (controller.IsLoaded == false) notComplete++;
            }

            if (notComplete == 0)
            {
                CrudLogger.Log($"All table layout elements have been successfully loaded by {typeof(T)}");
            }
        }

        public virtual void InitializeCompleted()
        {
            OnInitializeCompleted?.Invoke();
        }

        public virtual void ButtonClicked()
        {
            OnButtonClicked?.Invoke();
        }
    }
}