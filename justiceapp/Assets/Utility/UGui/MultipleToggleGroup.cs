using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.UGui
{
    public class MultipleToggleGroup : MonoBehaviour
    {
        [Tooltip("The number of toggles that can be on at a time in the group.")]
        public int MaxTogglesOn;
        [Tooltip("The minimum number of toggles that should be on at any given time in the group.")]
        public int MinTogglesOn;

        private readonly List<Toggle> _toggles = new List<Toggle>();

        private readonly List<int> _order = new List<int>();

        public void RegisterToggle(Toggle toggle)
        {
            if (toggle != null)
            {
                if (!_toggles.Contains(toggle))
                {
                    _toggles.Add(toggle);
                    if (toggle.isOn)
                    {
                        NotifyToggleOn(toggle);
                    }
                }

                CheckToggles();
            }
            else
            {
                Debug.LogError("Can't add null to toggle group.");
            }
        }

        public void UnRegisterToggle(Toggle toggle)
        {
            if (_toggles.Contains(toggle))
            {
                _toggles.Remove(toggle);
            }

            CheckToggles();
        }

        public void NotifyToggleOn(Toggle toggle)
        {
            if (_toggles.Contains(toggle))
            {
                var index = _toggles.IndexOf(toggle);
                if (toggle.isOn && !_order.Contains(index))
                {
                    _order.Add(index);
                }
                else if (_order.Contains(index))
                {
                    _order.Remove(index);
                }

                CheckToggles();
            }
            else
            {
                Debug.LogError("Toggle not in toggle group.");
            }
        }

        public bool AnyTogglesOn()
        {
            return _toggles.Any(toggle => toggle.isOn);
        }

        public int CountTogglesOn()
        {
            Debug.Assert(_order.Count == _toggles.Count(toggle => toggle.isOn));
            return _order.Count;
        }

        public int[] IndexesOn()
        {
            return _order.ToArray();
        }

        private void CheckToggles()
        {
            if (_order.Any() && _order.Count > MaxTogglesOn)
            {
                var index = _order.First();
                _order.RemoveAt(0);
                _toggles[index].isOn = false;
            }

            if (_order.Count < MinTogglesOn)
            {
                for (var i = 0; i < MinTogglesOn; i++)
                {
                    if (i < _toggles.Count)
                    {
                        _toggles[i].isOn = true;
                    }
                }
            }
        }

        private void OnValidate()
        {
            CheckToggles();
        }
    }
}