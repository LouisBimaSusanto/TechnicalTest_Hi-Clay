using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Game.Audio
{
    [System.Serializable]
    public class SoundEntry
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.5f, 1.5f)] public float pitch = 1f;
    }

    // Tambahkan class baru untuk BGM per scene
    [System.Serializable]
    public class SceneBGMEntry
    {
        public string sceneName;
        public AudioClip bgmClip;
        [Range(0f, 1f)] public float volume = 0.5f;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("BGM Per Scene")]
        public List<SceneBGMEntry> sceneBGMList = new List<SceneBGMEntry>();

        [Header("SFX Library")]
        public List<SoundEntry> sfxList = new List<SoundEntry>();

        private AudioSource bgmSource;
        private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SetupAudioSources();
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // Auto dipanggil setiap kali scene baru dimuat
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayBGMForScene(scene.name);
        }

        private void SetupAudioSources()
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }

        public void PlayBGMForScene(string sceneName)
        {
            SceneBGMEntry entry = sceneBGMList.Find(s => s.sceneName == sceneName);

            if (entry == null)
            {
                Debug.LogWarning($"[AudioManager] BGM untuk scene '{sceneName}' tidak ditemukan!");
                bgmSource.Stop();
                return;
            }

            // Jangan restart BGM kalau clip-nya sama
            if (bgmSource.clip == entry.bgmClip && bgmSource.isPlaying) return;

            bgmSource.clip = entry.bgmClip;
            bgmSource.volume = entry.volume;
            bgmSource.Play();
        }

        public void StopBGM() => bgmSource.Stop();

        public void PlaySFX(string soundName)
        {
            SoundEntry entry = sfxList.Find(s => s.name == soundName);
            if (entry == null)
            {
                Debug.LogWarning($"[AudioManager] SFX '{soundName}' tidak ditemukan!");
                return;
            }
            sfxSource.pitch = entry.pitch;
            sfxSource.PlayOneShot(entry.clip, entry.volume);
        }
    }
}

