using UnityEngine;

namespace HalfDiggers.Runner
{
    public struct SoundMusicSourceComponent
    {
        public AudioSource Source;
        public AudioClip[] Tracks;
        public int PlayedTrack;
        
        public  int FirstTrackNumber;
    }
}