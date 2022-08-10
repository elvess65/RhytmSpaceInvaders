using RhytmFramework;
using UnityEngine;
using UnityEngine.UI;

namespace RhytmFighter.UI.Components
{
    /// <summary>
    /// Компонент стрелки показывающей момент наступления следующего тика
    /// </summary>
    public class UIComponent_TickWidget_Arrow : MonoBehaviour
    {
        [SerializeField] private RectTransform m_ControlledTransform;
        [SerializeField] private Image m_ArrowImage;

        private Vector3 m_InitPos;
        private Vector3 m_InitScale;
        private RhytmInputProxy m_rhytmInputProxy;


        public void Initialize(RhytmInputProxy rhytmInputProxy)
        {
            m_InitPos = m_ControlledTransform.anchoredPosition;
            m_InitScale = m_ControlledTransform.localScale;
            m_rhytmInputProxy = rhytmInputProxy;
        }

        public void PrepareForInterpolation()
        {
            m_ControlledTransform.gameObject.SetActive(true);
        }

        public void FinishInterpolation()
        {
            m_ControlledTransform.gameObject.SetActive(false);
        }

        public void ProcessInterpolation(float progress)
        {
            m_ControlledTransform.anchoredPosition = Vector3.Lerp(m_InitPos, Vector3.zero, progress);
            m_ControlledTransform.localScale = m_InitScale * Mathf.Lerp(0, 1, progress);

            m_ArrowImage.color = m_rhytmInputProxy.IsInUseRange ? Color.green : Color.white;
        }
    }
}
