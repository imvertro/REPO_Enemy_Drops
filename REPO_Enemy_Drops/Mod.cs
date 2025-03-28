using Repo_Library;
using REPO_Enemy_Drops;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(Mod), "R.E.P.O Enemy Drops", "1.0.0", "ImVertro")]

[assembly: MelonGame("semiwork", "REPO")]

namespace REPO_Enemy_Drops
{
    public class Mod : MelonMod
    {
        private readonly Library Repo_Lib = new Library();

        public override void OnInitializeMelon()
        {
            Library.OnEnemyDeath += (enemy) =>
            {
                GameObject enable = enemy?.transform.Find("Enable")?.gameObject;
                GameObject controller = enable?.transform.Find("Controller")?.gameObject;
                List<string> upgrades = new List<string> { "Extra Jump", "Grab Range", "Energy", "Grab Strength", "Health", "Sprint Speed", "Tumble Launch" };
                string upgrade = upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
                // 10% chance to spawn a random upgrade if Gnome or Bang enemy
                if (enemy.name.Contains("Gnome") || enemy.name.Contains("Bang"))
                {
                    if (UnityEngine.Random.Range(0, 100) > 90)
                    {
                        Repo_Lib.SpawnItem($"Item Upgrade Player {upgrade}", controller.transform.position + controller.transform.up * 2);
                    }
                }
                else
                {
                    Repo_Lib.SpawnItem($"Item Upgrade Player {upgrade}", controller.transform.position + controller.transform.up * 2);
                }
            };
        }
    }
}