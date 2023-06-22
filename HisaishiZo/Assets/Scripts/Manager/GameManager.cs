using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        private readonly float MAX_PLAY_TIME = 3f * 60f;

        private float currentTime = 0;
        private bool isSeekerWin;

        private IEnumerator Timer()
        {
            while (currentTime < MAX_PLAY_TIME)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            isSeekerWin = PhotonEngineFusionManager.Instance._spawnedCharacters.Count == 1
                ? true : false;

            MoveToEndingScene(isSeekerWin);

            yield break;
        }

        public void GameStart()
        {
            StartCoroutine(Timer());
        }

        private void MoveToEndingScene(bool boolean)
        {
            if (boolean == true)
            {
                SceneManager.LoadScene("VictoryScene");
            }
            else
            {
                SceneManager.LoadScene("DefeatScene");
            }
        }
    }
}
