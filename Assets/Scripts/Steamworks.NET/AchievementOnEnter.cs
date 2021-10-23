using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchievementOnEnter : MonoBehaviour {
    public string AchievementName;
	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            if (SteamManager.Initialized) {
                bool userHasAchievement = false;
                SteamUserStats.GetAchievement(AchievementName, out userHasAchievement);
                if(!userHasAchievement) {
                    SteamUserStats.SetAchievement(AchievementName);
                    SteamUserStats.StoreStats();
                } else {
                    Debug.Log("User already has achievement: "+ AchievementName);
                }
            } else {
                Debug.LogError("ERROR: Steam Managaer not initilized before recording achievement: " + AchievementName);
            }
        }
    }
}
