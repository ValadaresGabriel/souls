using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace TranscendenceStudios
{
    public class TimelineManager : MonoBehaviour
    {
        public static TimelineManager Instance { get; private set; }

        public PlayableDirector playableDirector;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PauseTimeline()
        {
            if (playableDirector != null)
            {
                playableDirector.Pause();
            }
        }

        public void GoToTimeInTimeline(double timeInSeconds)
        {
            if (playableDirector != null)
            {
                playableDirector.time = timeInSeconds;

                playableDirector.Evaluate();
            }
            else
            {
                Debug.LogError("PlayableDirector reference not set.");
            }
        }

    }
}
