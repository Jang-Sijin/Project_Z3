using UnityEngine;
using System.Collections;
using System;

namespace ToonShader
{
    public class Dissolve : MonoBehaviour
    {
        public float dissolveTime = 2f; // ������ ���� �ð�
        public AnimationCurve curve;
        public float m_EmissionRate = 2f;

        private Renderer[] m_Renderers;
        private MaterialPropertyBlock m_PropertyBlock;
        private bool isDissolving = false; // �ߺ� ���� ����
        private ParticleSystem m_ParticleSystem;
        private ParticleSystem.EmissionModule m_Emission;
        private float m_EndTime; // ��ƼŬ ���� ����� ���� �ð�
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
                Debug.Log("��ƼŬ �ý��� ���������� ������");
            }
            else
            {
                Debug.LogWarning("��ƼŬ �ý����� �����ϴ�!");
            }
        }

        public void StartDissolve(Action callback = null)
        {
            if (isDissolving) return; // �̹� ���� ���̸� �ߺ� ���� ����

            isDissolving = true;
            _onDissolveComplete = callback;

            float particleLifetime = m_ParticleSystem != null ? m_ParticleSystem.main.startLifetime.constant : 0f;
            m_EndTime = Time.time + dissolveTime + particleLifetime; // ��ƼŬ ���� ���

            Debug.Log("������ ����");
            StartCoroutine(DissolveCoroutine());
        }

        private IEnumerator DissolveCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < dissolveTime)
            {
                float cutoff = 1.0f - Mathf.Clamp01(elapsedTime / dissolveTime);

                // ���͸��� ������Ʈ
                for (int i = 0; i < m_Renderers.Length; i++)
                {
                    m_Renderers[i].GetPropertyBlock(m_PropertyBlock);
                    m_PropertyBlock.SetFloat(k_CutoffName, cutoff);
                    m_Renderers[i].SetPropertyBlock(m_PropertyBlock);
                }

                // ��ƼŬ ���ⷮ ����
                if (m_ParticleSystem != null)
                {
                    float newEmissionRate = curve.Evaluate(cutoff) * m_EmissionRate;
                    m_Emission.rateOverTimeMultiplier = newEmissionRate;
                    Debug.Log($"��ƼŬ ���ⷮ ������Ʈ: {newEmissionRate}");
                }

                elapsedTime += Time.deltaTime;
                yield return null; // ���� �����ӱ��� ���
            }

            //// ������ �Ϸ� �� ������ ó��
            //for (int i = 0; i < m_Renderers.Length; i++)
            //{
            //    m_Renderers[i].GetPropertyBlock(m_PropertyBlock);
            //    m_PropertyBlock.SetFloat(k_CutoffName, 0);
            //    m_Renderers[i].SetPropertyBlock(m_PropertyBlock);
            //}

            // ��ƼŬ�� ������ ���� ������ ��ٸ�
            if (m_ParticleSystem != null)
            {
                Debug.Log("��ƼŬ ���� ��� ��...");
                yield return new WaitForSeconds(m_ParticleSystem.main.startLifetime.constant);
            }

            gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
            isDissolving = false;
            Debug.Log("������ �Ϸ�");

            _onDissolveComplete?.Invoke();
        }
    }
}
