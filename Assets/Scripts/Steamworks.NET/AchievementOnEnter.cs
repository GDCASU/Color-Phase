using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchievementOnEnter : MonoBehaviour {
    public string AchievementName;
	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && SteamManager.Initialized) {
            bool userHasAchievement = false;
            SteamUserStats.GetAchievement(AchievementName, out userHasAchievement);
            if(!userHasAchievement) {
                SteamUserStats.SetAchievement(AchievementName);
                SteamUserStats.StoreStats();
            }
        }
    }
}
