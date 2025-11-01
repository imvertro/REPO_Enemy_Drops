using Repo_Library;
using REPO_Enemy_Drops;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MelonLoader.Utils;
using System;
using System.Linq;

[assembly: MelonInfo(typeof(Mod), "R.E.P.O Enemy Drops", "2.0.0", "ImVertro")]
[assembly: MelonGame("semiwork", "REPO")]

// Add the Upgrades property to the Config class
public class Config
{
    public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, float>>>> Entities { get; set; } = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, float>>>>();
}

namespace REPO_Enemy_Drops
{
    public class Mod : MelonMod
    {
        private readonly Library Repo_Lib = new Library();
        private readonly string configPath = Path.Combine(MelonEnvironment.UserDataDirectory, "repo_enemy_drops_config.json");

        // This function is obsolete but is required for the mod to work
        [System.Obsolete]
        public override void OnApplicationStart()
        {
            // Load config file
            Config config = ReadConfigFile(configPath);

            Library.OnEnemyDeath += (enemy) => {
                // Remove Enemy - and (Clone) from the enemy name separately
                string name = enemy.name.Replace("Enemy -", "").Replace("(Clone)", "").Trim();
                GameObject enable = enemy?.transform.Find("Enable")?.gameObject;
                GameObject controller = enable?.transform.Find("Controller")?.gameObject;
                if (controller == null)
                {
                    MelonLogger.Error("Controller not found in enemy hierarchy.");
                    return;
                }

                // Pick a random item for the enemy using its name
                if (config.Entities.TryGetValue(name, out var entityConfig) && entityConfig.TryGetValue("Items", out var items))
                {
                    // Pick a random item from the list
                    string itemName = items.ElementAt(UnityEngine.Random.Range(0, items.Count)).Key;

                    // Get the items Chance value
                    if (items[itemName].TryGetValue("Chance", out var chance))
                    {
                        // Check if the random number is less than the chance
                        if (UnityEngine.Random.Range(0f, 1f) < chance)
                        {
                            // Spawn the item
                            Repo_Lib.SpawnItem(itemName, controller.transform.position + controller.transform.up * 2);
                        }
                    }
                }
                else
                {
                    MelonLogger.Error($"No config found for enemy: {name}");
                }
            };
        }

        public override void OnApplicationQuit()
        {
            // Optional: Write new config file on application quit
            // WriteConfigFile(configPath, newConfigFile);
        }

        // Writes the config file to the specified path
        private void WriteConfigFile(string path, Config config)
        {
            try
            {
                string json = JsonConvert.SerializeObject(RemoveDuplicates(config), Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to write config file: {e.Message}", e);
            }
        }

        // Update the RemoveDuplicates method to use the Items property instead of Upgrades
        private Config RemoveDuplicates(Config config)
        {
            config.Entities = config.Entities.Distinct().ToDictionary(x => x.Key, x => x.Value);
            return config;
        }

        // Update the ReadConfigFile method to initialize the Items property instead of Upgrades
        private Config ReadConfigFile(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Config config = JsonConvert.DeserializeObject<Config>(json);
                MelonLogger.Msg($"Config file loaded from {path}.");
                WriteConfigFile(path, config); // Write the config file again to ensure it's up to date and remove duplicates
                return config;
            }
            else
            {
                MelonLogger.Msg($"Config file not found at {path}. Creating a new one.");

                Config config = CreateDefaultConfig(path);
                WriteConfigFile(path, config);
                return config;
            }
        }

        private Config CreateDefaultConfig(string path)
        {
            Config config = new Config
            {
                Entities = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, float>>>>
                {
                    {
                        "Gnome", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }

                                }
                            }
                        }
                    },
                    {
                        "Bang", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 0.1f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Head", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Slow Walker", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Robe", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Duck", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Runner", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Slow Mouth", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Floater", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Thin Man", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Hunter", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Bowtie", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Beamer", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Valuable Thrower", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Upscream", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Tumbler", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Animal", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Spinny", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Birthday Boy", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Bomb Thrower", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Oogly", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Heart Hugger", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Head Grabber", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Shadow", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Floater", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Elsa", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Trick", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Tricycle", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    },
                    {
                        "Hidden", new Dictionary<string, Dictionary<string, Dictionary<string, float>>>
                        {
                            {
                                "Items", new Dictionary<string, Dictionary<string, float>>
                                {
                                    { "Item Upgrade Player Extra Jump", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Range", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Energy", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Grab Strength", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Health", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Sprint Speed", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Launch", new Dictionary<string, float> { { "Chance", 1.0f }, } },
                                    { "Item Upgrade Player Tumble Wings", new Dictionary<string, float> { { "Chance", 0.1f }, } }
                                }
                            }
                        }
                    }
                }
            };
            return config;
        }
    }
}