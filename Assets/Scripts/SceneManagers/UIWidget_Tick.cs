using CoreFramework.Utils;
using RhytmFighter.UI.Components;
using RhytmFramework;
using UnityEngine;

namespace RhytmFighter.UI.Widget
{
    /// <summary>
    /// Виджет отображения состояния тиков
    /// </summary>
    public class UIWidget_Tick : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] UIComponent_TickWidget_Tick m_Tick;
        [SerializeField] UIComponent_TickWidget_Arrow[] m_TickArrows;
   
        private InterpolationData<float> m_LerpData;
        private RhytmController m_rhytmController;

        public void Initialize(float tickDuration, RhytmInputProxy rhytmInputProxy, RhytmController rhytmController)
        {
            m_rhytmController = rhytmController;

            //Lerp
            m_LerpData = new InterpolationData<float>();

            //Tick
            m_Tick.Initialize(tickDuration);

            //Arrows
            for (int i = 0; i < m_TickArrows.Length; i++)
                m_TickArrows[i].Initialize(rhytmInputProxy);
        }

        public void PerformUpdate(float deltaTime)
        {
            if (m_LerpData.IsStarted)
            {
                //Increment
                m_LerpData.Increment();

                //Process
                for (int i = 0; i < m_TickArrows.Length; i++)
                    m_TickArrows[i].ProcessInterpolation(m_LerpData.Progress);

                //Overtime
                if (m_LerpData.Overtime())
                {
                    m_LerpData.Stop();

                    for (int i = 0; i < m_TickArrows.Length; i++)
                        m_TickArrows[i].FinishInterpolation();
                }
            }
        }

        #region Animations

        public void PlayTickAnimation()
        {
            m_Tick.PlayTickAnimation();
        }

        public void PlayArrowsAnimation()
        {
            //Arrows
            for (int i = 0; i < m_TickArrows.Length; i++)
                m_TickArrows[i].PrepareForInterpolation();

            //Lerp
            m_LerpData.TotalTime = (float)m_rhytmController.TimeToNextTick + (float)m_rhytmController.ProcessTickDelta;
            m_LerpData.Start();
        }

        #endregion
    }
}
