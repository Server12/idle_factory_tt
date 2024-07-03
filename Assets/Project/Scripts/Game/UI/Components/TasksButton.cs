using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    public class TasksButton : CustomButton
    {
        [SerializeField] private Image _completeIcon;

        public void PingTaskComplete()
        {
            _completeIcon.gameObject.SetActive(true);
            _completeIcon.enabled = true;
        }

        public void StopPingCompletedTask()
        {
            _completeIcon.gameObject.SetActive(false);
        }
        
        private void Start()
        {
            _completeIcon.gameObject.SetActive(false);
        }


        public void SetComplete()
        {
            _completeIcon.gameObject.SetActive(true);
        }
    }
}