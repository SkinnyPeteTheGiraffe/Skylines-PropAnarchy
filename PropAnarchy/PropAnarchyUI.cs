﻿using ColossalFramework.UI;
using PropAnarchy.OptionsFramework;
using UnityEngine;

namespace PropAnarchy
{
    public class PropAnarchyUI : MonoBehaviour
    {
        private const string On = "Prop & Tree Anarchy: On";
        private const string Off = "Prop & Tree Anarchy: Off";

        private UILabel _label;

        public void Awake()
        {
            _label = GameObject.Find("OptionsBar").GetComponent<UIPanel>().AddUIComponent<UILabel>();
            _label.relativePosition += new Vector3(-100, 0 , 0);
        }

        public void OnDestroy()
        {
            Destroy(_label.gameObject);
        }

        public void Update()
        {
            if (OptionsWrapper<Options>.Options.anarchyAlwaysOn)
            {
                DetoursManager.Deploy();
                _label.Hide();
                return;
            }
            _label.Show();
            SetupText();
            if ((!Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.LeftShift)) || !Input.GetKeyDown(KeyCode.P) ||
                Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftControl) ||
                Input.GetKey(KeyCode.RightControl))
            {
                return;
            }
            if (DetoursManager.IsDeployed())
            {
                DetoursManager.Revert();
            }
            else
            {
                DetoursManager.Deploy();
            }
        }

        //performed each frame
        public void SetupText()
        {
            if (_label == null || !_label.isVisible)
            {
                return;
            }
            _label.text = DetoursManager.IsDeployed()? On : Off;
         }
}
}