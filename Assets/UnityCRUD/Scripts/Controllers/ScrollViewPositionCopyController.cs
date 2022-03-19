using UnityEngine;
using UnityEngine.UI;

namespace UnityCRUD.Scripts.Controllers
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewPositionCopyController : MonoBehaviour
    {
        public ScrollRect CopyThis;
        private ScrollRect CopyTo;

        private void Start()
        {
            CopyTo = GetComponent<ScrollRect>();
        }

        public void Update()
        {
            CopyTo.horizontalNormalizedPosition = CopyThis.horizontalNormalizedPosition;
        }
    }
}
