using UnityEngine;
using System.Collections;
using System;

namespace ToonShader
{
    public class Dissolve : MonoBehaviour
    {
        public float dissolveTime = 2f; // 디졸브 진행 시간
        public AnimationCurve curve;
        public float m_EmissionRate = 2f;

        private Renderer[] m_Renderers;
        private MaterialPropertyBlock m_PropertyBlock;
        private bool isDissolving = false; // 중복 실행 방지
        private ParticleSystem m_ParticleSystem;
        private ParticleSystem.EmissionModule m_Emission;
        private float m_EndTime; // 파티클 수명 고려한 종료 시간
        private Action _onDissolveComplete;

        const string k_CutoffName = "_DissolveCutoff";

        private void Start()
        {
            m_Renderers = GetComponentsInChildren<Renderer>();
            m_PropertyBlock = new MaterialPropertyBlock();

            m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

            if (m_ParticleSystem != null)
            {
                m_Emission = m_ParticleSystem.emission;
                m_EmissionRate = m_Emission.rateOverTime.constant;
                m_Emission.rateOverTimeMultiplier = 0;
                Debug.Log("파티클 시스템 정상적으로 가져옴");
            }
            else
            {
                Debug.LogWarning("파티클 시스템이 없습니다!");
            }
        }

        public void StartDissolve(Action callback = null)
        {
            if (isDissolving) return; // 이미 실행 중이면 중복 실행 방지

            isDissolving = true;
            _onDissolveComplete = callback;

            float particleLifetime = m_ParticleSystem != null ? m_ParticleSystem.main.startLifetime.constant : 0f;
            m_EndTime = Time.time + dissolveTime + particleLifetime; // 파티클 종료 고려

            Debug.Log("디졸브 시작");
            StartCoroutine(DissolveCoroutine());
        }

        private IEnumerator DissolveCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < dissolveTime)
            {
                float cutoff = 1.0f - Mathf.Clamp01(elapsedTime / dissolveTime);

                // 머터리얼 업데이트
                for (int i = 0; i < m_Renderers.Length; i++)
                {
                    m_Renderers[i].GetPropertyBlock(m_PropertyBlock);
                    m_PropertyBlock.SetFloat(k_CutoffName, cutoff);
                    m_Renderers[i].SetPropertyBlock(m_PropertyBlock);
                }

                // 파티클 방출량 조정
                if (m_ParticleSystem != null)
                {
                    float newEmissionRate = curve.Evaluate(cutoff) * m_EmissionRate;
                    m_Emission.rateOverTimeMultiplier = newEmissionRate;
                    Debug.Log($"파티클 방출량 업데이트: {newEmissionRate}");
                }

                elapsedTime += Time.deltaTime;
                yield return null; // 다음 프레임까지 대기
            }

            //// 디졸브 완료 후 마지막 처리
            //for (int i = 0; i < m_Renderers.Length; i++)
            //{
            //    m_Renderers[i].GetPropertyBlock(m_PropertyBlock);
            //    m_PropertyBlock.SetFloat(k_CutoffName, 0);
            //    m_Renderers[i].SetPropertyBlock(m_PropertyBlock);
            //}

            // 파티클이 있으면 끝날 때까지 기다림
            if (m_ParticleSystem != null)
            {
                Debug.Log("파티클 종료 대기 중...");
                yield return new WaitForSeconds(m_ParticleSystem.main.startLifetime.constant);
            }

            gameObject.SetActive(false); // 오브젝트 비활성화
            isDissolving = false;
            Debug.Log("디졸브 완료");

            _onDissolveComplete?.Invoke();
        }
    }
}
