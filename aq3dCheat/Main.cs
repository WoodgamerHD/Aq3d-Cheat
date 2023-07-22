using System;
using UnityEngine;
using StatCurves;
using static UnityEngine.EventSystems.EventTrigger;
using static Leaderboard;
using System.Collections.Generic;
using System.IO;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using System.Threading;
using UnityEngine.UI;

namespace Aq3dCheat
{
    class Main : MonoBehaviour
    {

        bool esp = true;
        bool Box_esp = false;

        bool Box_espNpc = false;
        bool godmode = false;
        bool Runfast = false;
        bool Text_esp = true;
        bool Text_espNpc = true;
        bool noclipEnabled = false;

        bool WorldTeleporter = false;
        bool LoadShopMenu = false;
        bool LoadMergeShopMenu = false;
        bool QuestMenu = false;
        private string CustomID;

        bool isPrivate = false;

        bool enterScaled = false;
        private Color blackCol;

        public static List<EntityController> EntityCont = new List<EntityController>();

        float natNextUpdateTime;
        private List<UIDungeons> itemGOs;

        public static bool CanReceiveAdminMessages(Player player)
        {
            return AccessLevels.IsAdmin(player);
        }


        private int[] locations = new int[] { 1, 3, 4, 5, 6, 7, 8, 9, 11, 12, 14, 15, 16, 18, 26, 30, 32, 33, 38, 39, 42, 43, 44, 45, 46, 48, 49, 51, 55, 56, 62, 72, 74, 75, 76, 82, 83, 84, 85, 87, 88, 89, 90, 91, 93, 95, 96, 97, 105, 106, 107, 108, 111, 112, 114, 115, 116, 118, 119, 120, 121, 122, 123, 129, 132, 134, 135, 136, 137, 138, 140, 141, 142, 143, 144, 145, 146, 153, 155, 156, 157, 158, 165, 168, 172, 173, 174, 175, 176, 177, 178, 179, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 193, 194, 195, 196, 198, 202, 203, 208, 210, 211, 212, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225 };

        private string[] locations2 = new string[] { "Battleon", "Yulgar's Inn", "Greenguard Forest", "Heartwood Forest", "Livingstone Caverns", "Doomwood Forest", "Bone Cliffs", "Shadowskull Bridge", "Yulgar's Sinkhole", "Magic Shoppe", "Class Trainers", "Cysero's Shop", "Dricken Cave", "Shadowskull Tower", "Barrow Pass", "Barrow Point", "Crypt of the Scytheblade", "Umbral Caves", "Screaming Woods", "Caves of Unrest", "Crypt of the War Axe", "Crypt of the War Staff", "Silverstone Mines", "Glowing Depths", "Crimson Cavern", "Whitaker Forest", "Snevlyn Wilds", "Livingstone Alcove", "Guardian Tower", "Dragon's Hall", "Sanctuary", "Guardian Training Grounds", "Guardian Research Hall", "Guardian Battle Arena", "DragonSlayer Camp", "Lower Firefield", "Barrow Drop", "Upper Firefield", "The Slabs", "Frost Labyrinth", "Tower Pass", "Barrow Drop Challenge", "Brimston Ruins", "DragonSlayer Tent", "Yettun Cave", "Epic Yettun Cave", "Volcano Challenge 1", "Volcano Challenge 2", "Volcano Challenge 3", "Valek's Challenge", "Dage's Challenge (Normal)", "Magma Falls", "Magma Mines", "Magma Mine Pass", "Slabs Cavern", "Rockedge Mine", "Void Waiting Hall", "Dragonwatch Battlefield", "Dragonwatch Keep", "Dragonwatch Castle", "Battleon Bank", "Doomwood Dungeon", "Greenguard Caves", "Dragon's Lair (Story)", "Bothvars Castle", "Fort Sneed", "Vault Security (1 Hero)", "Vault Security (5 Hero)", "Vault Security (2 Hero)", "Deep Vault", "The Master Vault", "Mount Ashfall Camp", "Deadwood Grove", "Shadowguard", "Mystics Cave", "Greenguard Brutal Challenge", "Greenguard Zard Challenge", "Greenguard Clawg Challenge", "Darkovia Forest", "Dimgrove", "Darkhurst", "Boog's Tavern", "Inn Room", "Vampire Attack", "Vampire Assault", "Werewolf Assault", "Werewolf Attack", "Cave of the Cure", "Shadowsong Crypt", "Cure Room", "Lightovia", "Deepclaw Caverns", "Frostlorn Raider Challenge", "Dragon's Lair Dungeon", "Akriloth Group Challenge", "Akriloth Extreme Challenge", "Dragon's Lair Checkpoint (Story)", "Mouse Hole Lobby", "Yulgars' West Wall", "Wall Parkour", "Dragon's Lair Caldera (Story)", "Underworld Gate", "Dragon's Lair Caldera (Story)", "Dragon's Lair Caldera (Story)", "Lava Run", "Soul Chamber (Story)", "The Lava's Heart", "Soul Chamber", "Dage's Challenge (Hard)", "Underworld Parkour", "Water Plane", "Earth Plane", "Fire Plane", "World Tree", "Hall of Awesome", "Wind Plane" };

        private int[] shopIDs = new int[] { 1, 3, 5, 7, 9, 11, 15, 17, 19, 31, 33, 37, 95, 97, 99 };
        private string[] shopNames = new string[] { "Yulgar's Shop", "Artix Shop", "Rolith Test Shop", "Gaz's Black Market", "Metrea's Rogue Shop", "Arcana's Mage Shop", "Tester Shop", "Code Redeem Shop", "Gamush's Stuff", "Senna's Chest", "Galanoth's Chest", "Underworld Goods", "Warlic's Magic Shoppe", "Open Beta Shop", "Battleon Tree" };

        int[] MergeshopIDs = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 31, 33, 35, 37, 39, 41, 43, 45, 47, 49, 51, 53, 55, 57, 59, 61 };

        string[] MergeshopNames = new string[] { "Test Merge Shop", "Merge Shop 2", "Robina - Barrow Point", "Gamush - Greenguard Cave", "Metrea - Trainers", "Arcana - Trainers", "Zealot - Doomwood Graveyard", "Cysero - Forge", "Loth - Doomwood Graveyard", "Heath - Doomwood Bridge", "Balis - Crimson Caverns", "Artix - Skull Tower", "Myx - Haunted Manor", "Craft Ebil Items", "Cysero - Barrow Drop", "Dragonslayer Crafting Shop", "Blizzy - Frost Labyrinth", "Senna's Stitches", "DragonSlayer Material Crafting", "Dragon Stalker Crafting", "Mine Crystal Crafting 2", "Brimston Crafting", "Crimson Renegade Crafting", "BatleOn Defender Belts", "BatleOn Defender Helms", "BatleOn Defender Boots", "BatleOn Defender Back Items", "GUARDIAN defender armor", "GUARDIAN defender travel form", "Battle Gem Crafting" };

        int[] questIDs = new int[] { 1, 2, 7, 8, 34, 37, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 82, 83, 84, 85, 86, 87, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288 };
        string[] questNames = new string[] {
    "Do You Have The Stomach?",
    "Beauty Rest",
    "Bait and Switch",
    "Breakfast at Yulgar's",
    "Placeholder Quest",
    "Butt Heads",
    "The Hood",
    "The Best Defense...",
    "Capture The Flags",
    "For SCIENCE!",
    "Holding A Note",
    "Boxes Make Me Fort",
    "Look Over There!",
    "Stealing From Thieves",
    "The Thieved Thesis",
    "The Organ Job",
    "Books Come From Trees",
    "Another Favor",
    "Tipping The Scales",
    "Jewels Of The Forest",
    "Jewels Of The Sea",
    "On The Wings Of Eyeballs",
    "Recharging",
    "Silent Screams",
    "For The Shell Of It",
    "The Lizard Brain",
    "Because We Can",
    "Creative Problem Solving",
    "Free Samples",
    "The Brass Ring (Repeatable)",
    "Welcome To The Sinkhole!",
    "Cellar Door",
    "Strange Brew",
    "Lord of the Pies",
    "You Can Pick Your Friends",
    "You Can Pick Your Nose",
    "You Can't Pick Your Friends Noses",
    "Going The Distance",
    "The Final Showdown",
    "The Final Final Showdown",
    "Reed",
    "The Boy Who Cried Wolf",
    "Assaulted Nuts",
    "Seedkeeper",
    "Ace of Spades",
    "Confession",
    "Good Clean Dirt",
    "Water From Water",
    "Waters Rise",
    "Stir The Winds",
    "The Hart's Heart",
    "Signed, Sealed, Delivered",
    "Bring The Bees",
    "Monstrous Essence",
    "Root of all Evil",
    "A Child's Wisdom",
    "Remembering Old Friends",
    "A Friend Indeed",
    "Warlord Gamush",
    "The Most Obvious Place",
    "Stuff And Things",
    "Even Better Than The Real Thing",
    "Stick Together",
    "Red Leather, Yellow Leather",
    "A Healthy Stink",
    "What Counts",
    "Flying Response",
    "Potions of A Questionable Nature",
    "A Leader's Role",
    "Smashy Smashy",
    "Follow That Rock",
    "Time To Rock",
    "The Rocklord",
    "Saturated with Box",
    "Mouth Parts",
    "Trolluk Toenails",
    "That Special Fungus",
    "Pebblars Final Form",
    "Artix Von Krieger",
    "No Sign Of Life",
    "This Looks Bad",
    "Name The Enemy",
    "Shadow Shapes",
    "The Driven Soul",
    "Strike a Light",
    "Ring of Truth",
    "Excavation",
    "Filling In the Blanks",
    "The Turncoat",
    "Confirmation",
    "Captive Souls",
    "Wolfing Them Down",
    "Resupply",
    "Bringing Home The Broadswords",
    "Fortifications",
    "The Field Book of Blessings",
    "Shattered Teeth",
    "Dust to Dust",
    "Clean Up",
    "Big Bones"
};

        private Vector2 scrollPosition = Vector2.zero;
        private Vector2 scrollPosition2 = Vector2.zero;
        private Vector2 scrollPosition3 = Vector2.zero;
        private Vector2 scrollPosition4 = Vector2.zero;
        private Vector2 scrollPosition5 = Vector2.zero;

        // Token: 0x04000019 RID: 25
        public static readonly Dictionary<string, AnimationClip> stringClip = new Dictionary<string, AnimationClip>();
        public List<GameObject> entities = new List<GameObject>();


        private Rect windowRect = new Rect(0, 0, 400, 200); // Window position and size
        private int tab = 0; // Current tab index
        private Color backgroundColor = Color.grey; // Background color
        private bool showMenu = true; // Whether to show the menu or not
        private static Rect MenuWindow2 = new Rect(60f, 250f, 300f, 230f);
        private static Rect Teleporterr = new Rect(60f, 250f, 400, 400);
        private static Rect ShopWindow = new Rect(60f, 250f, 400, 400);
        private static Rect MergeShopWindow = new Rect(60f, 250f, 400, 400);
        private static Rect QuestWind = new Rect(60f, 250f, 400, 400);
        private static Rect Testwindow = new Rect(60f, 250f, 400, 400);



        void MenuWindow(int windowID)
        {
            GUILayout.BeginHorizontal();

            // Create toggle buttons for each tab
            GUILayout.BeginVertical(GUILayout.Width(50));
            if (GUILayout.Toggle(tab == 0, "Main", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 0;
            }
            if (GUILayout.Toggle(tab == 1, "Esp", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 1;
            }
            if (GUILayout.Toggle(tab == 2, "Exploits", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 2;
            }
            if (GUILayout.Toggle(tab == 3, "Random Stuff", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 3;

            }
            if (GUILayout.Toggle(tab == 4, "Windows", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 4;
            }
            if (GUILayout.Toggle(tab == 5, "Random", "Button", GUILayout.ExpandWidth(true)))
            {
                tab = 5;
            }
            GUILayout.EndVertical();

            // Display content for the selected tab

            GUILayout.BeginVertical();


            // Display content for the selected tab
            switch (tab)
            {
                case 0:
                    if (GUILayout.Button("LootALL", GUILayout.ExpandWidth(true)))
                    {
                        Game.Instance.SendLootAllItemsRequest();
                    }
                    if (GUILayout.Button("UIDungeons"))
                    {
                        UIDungeons.Load(Enumerable.Range(0, 800).ToList());
                    }
                  
                  
                    break;
                case 1:
                //    GUILayout.BeginVertical(GUI.skin.box);

                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                    esp = GUILayout.Toggle(esp, "ESP toggle");
                    Text_esp = GUILayout.Toggle(Text_esp, "Text Esp");
                    Box_esp = GUILayout.Toggle(Box_esp, "Box Esp");
                  

                    GUILayout.EndVertical();

                    GUILayout.Space(10);

                    GUILayout.BeginVertical();
                    Box_espNpc = GUILayout.Toggle(Box_espNpc, "Box Npc");
                    Text_espNpc = GUILayout.Toggle(Text_espNpc, "Npc Text");
              
                    GUILayout.EndVertical();

                    GUILayout.EndHorizontal();

               //     GUILayout.EndVertical();



                    break;
                case 2:

                    GUILayout.BeginHorizontal();
                    godmode = GUILayout.Toggle(godmode, "Godmode");
                    Runfast = GUILayout.Toggle(Runfast, "Run fast");

                    GUILayout.EndHorizontal();


                    if (GUILayout.Button("Fast Haste"))
                    {
                        Entities.Instance.me.statsCurrent[Stat.Haste] = float.MaxValue;
                    }

                    if (GUILayout.Button("Max Armor"))
                    {
                        Entities.Instance.me.statsCurrent[Stat.Armor] = float.MaxValue;
                    }
                    if (GUILayout.Button("Skill Speed"))
                    {
                        Entities.Instance.me.statsCurrent[Stat.SkillSpeedBonus] = float.MaxValue;
                    }

                    break;
                case 3:

                    if (GUILayout.Button("Removes name for Screenshots", GUILayout.ExpandWidth(true)))
                    {

                        Entities.Instance.me.namePlate.label.text = "No Name For You";
                        Entities.Instance.me.entitycontroller.Entity.namePlate.label.text = "No Name For You";
                        Entities.Instance.me.namePlate.label.fontSize = 0;
                        Entities.Instance.me.entitycontroller.Entity.namePlate.label.fontSize = 0;
                    }

                    if (GUILayout.Button("AccessLevel SuperAdmin", GUILayout.ExpandWidth(true)))
                    {

                        Entities.Instance.me.AccessLevel = 101;
                    }

                  
                    break;
                case 4:
                    if (GUILayout.Button("World Teleporter", GUILayout.ExpandWidth(true)))
                    {
                        WorldTeleporter = !WorldTeleporter;
                    }
                    if (GUILayout.Button("Shop Menu", GUILayout.ExpandWidth(true)))
                    {
                        LoadShopMenu = !LoadShopMenu;
                    }
                    if (GUILayout.Button("Merge Shop Menu", GUILayout.ExpandWidth(true)))
                    {
                        LoadMergeShopMenu = !LoadMergeShopMenu;
                    }
                    if (GUILayout.Button("Quest Request Menu", GUILayout.ExpandWidth(true)))
                    {
                        QuestMenu = !QuestMenu;
                    }

                    break;
                case 5:
                    GUILayout.Label("Map Name:");
                    CustomID = GUILayout.TextField(CustomID, GUILayout.Width(285)); // Create TextField with a width of 200 pixels
                    if (GUILayout.Button("Map Name Load") && CustomID != null)
                    {

                        try
                        {

                            Game.Instance.SendAreaJoinCommand(CustomID);
                        }
                        catch
                        {

                        }

                    }

                    break;
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUI.DragWindow(); // Allow the user to drag the window around
        }
        public void WorldTeleporterWindow(int windowID)
        {

            GUILayout.BeginHorizontal();
            isPrivate = GUILayout.Toggle(isPrivate, "isPrivate");
            enterScaled = GUILayout.Toggle(enterScaled, "EnterScaled");
            GUILayout.EndHorizontal();



            // Begin a scroll view to allow scrolling through the button list
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Create a button for each option in the options array
            for (int i = 0; i < locations2.Length; i++)
            {
                if (GUILayout.Button(locations2[i]))
                {
                    // Get the selected location from the locations array
                    int selectedLocation = locations[i];

                    // Print the selected location to the console
                    Debug.Log("Selected location: " + selectedLocation);
                    Game.Instance.SendAreaJoinRequest(selectedLocation, isPrivate, enterScaled);
                }
            }
            GUILayout.EndScrollView();

            GUI.DragWindow(); // Allow the user to drag the window around



        }

        public void LoadShopWindow(int windowID)
        {


            // Begin a scroll view to allow scrolling through the button list
            scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);

            // Create a button for each option in the options array
            for (int i = 0; i < shopNames.Length; i++)
            {
                if (GUILayout.Button(shopNames[i]))
                {
                    // Get the selected location from the locations array
                    int selectedLocation = shopIDs[i];

                    // Print the selected location to the console
                    Debug.Log("Selected location: " + selectedLocation);
                    //   Game.Instance.SendShopLoadRequest(selectedLocation, ShopType.Shop);
                    UIShop.LoadShop(selectedLocation, ShopType.Shop, 0);
                }
            }
            GUILayout.EndScrollView();

            GUI.DragWindow(); // Allow the user to drag the window around



        }
        public void LoadMergeShopWindow(int windowID)
        {


            // Begin a scroll view to allow scrolling through the button list
            scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

            // Create a button for each option in the options array
            for (int i = 0; i < MergeshopNames.Length; i++)
            {
                if (GUILayout.Button(MergeshopNames[i]))
                {
                    // Get the selected location from the locations array
                    int selectedLocation = MergeshopIDs[i];

                    // Print the selected location to the console
                    Debug.Log("Selected location: " + selectedLocation);

                    UIMerge.Load(selectedLocation);
                }
            }
            GUILayout.EndScrollView();

            GUI.DragWindow(); // Allow the user to drag the window around



        }
        public void QuestWindow(int windowID)
        {


            // Begin a scroll view to allow scrolling through the button list
            scrollPosition4 = GUILayout.BeginScrollView(scrollPosition4);

            // Create a button for each option in the options array
            for (int i = 0; i < questNames.Length; i++)
            {
                if (GUILayout.Button(questNames[i]))
                {
                    // Get the selected location from the locations array
                    int selectedLocation = questIDs[i];

                    // Print the selected location to the console
                    Debug.Log("Selected location: " + selectedLocation);
                    Game.Instance.SendQuestAcceptRequest(selectedLocation);
                    // UIMerge.Load(selectedLocation);
                }
            }
            GUILayout.EndScrollView();

            GUI.DragWindow(); // Allow the user to drag the window around



        }
        public void TestWindow(int windowID)
        {


            // Begin a scroll view to allow scrolling through the button list
            scrollPosition5 = GUILayout.BeginScrollView(scrollPosition5);

            // Create a button for each option in the options array
            for (int i = 0; i < questNames.Length; i++)
            {
                if (GUILayout.Button(questNames[i]))
                {
                    // Get the selected location from the locations array
                    int selectedLocation = questIDs[i];

                    // Print the selected location to the console
                    Debug.Log("Selected location: " + selectedLocation);
                    Game.Instance.SendQuestAcceptRequest(selectedLocation);
                    // UIMerge.Load(selectedLocation);
                }

            }
            GUILayout.EndScrollView();

            GUI.DragWindow(); // Allow the user to drag the window around



        }


        public static List<Transform> GetAllBones(Animator a)
        {
            List<Transform> Bones = new List<Transform>
            {
                a.GetBoneTransform(HumanBodyBones.Head), // 0
                a.GetBoneTransform(HumanBodyBones.Neck), // 1
                a.GetBoneTransform(HumanBodyBones.Spine), // 2
                a.GetBoneTransform(HumanBodyBones.Hips), // 3

                a.GetBoneTransform(HumanBodyBones.LeftShoulder), // 4
                a.GetBoneTransform(HumanBodyBones.LeftUpperArm), // 5
                a.GetBoneTransform(HumanBodyBones.LeftLowerArm), // 6
                a.GetBoneTransform(HumanBodyBones.LeftHand), // 7

                a.GetBoneTransform(HumanBodyBones.RightShoulder), // 8
                a.GetBoneTransform(HumanBodyBones.RightUpperArm), // 9
                a.GetBoneTransform(HumanBodyBones.RightLowerArm), // 10
                a.GetBoneTransform(HumanBodyBones.RightHand), // 11

                a.GetBoneTransform(HumanBodyBones.LeftUpperLeg), // 12
                a.GetBoneTransform(HumanBodyBones.LeftLowerLeg), // 13
                a.GetBoneTransform(HumanBodyBones.LeftFoot), // 14

                a.GetBoneTransform(HumanBodyBones.RightUpperLeg), // 15
                a.GetBoneTransform(HumanBodyBones.RightLowerLeg), // 16
                a.GetBoneTransform(HumanBodyBones.RightFoot) // 17
            };

            return Bones;
        }


        public void OnGUI()
        {
            if (showMenu) // Only draw the menu when showMenu is true
            {
                // Set the background color
                GUI.backgroundColor = backgroundColor;

                windowRect = GUI.Window(0, windowRect, MenuWindow, "Menu"); // Create the window with title "Menu"


                if (WorldTeleporter)
                {
                    Teleporterr = GUI.Window(1, Teleporterr, WorldTeleporterWindow, "Teleporter Menu");
                }
                if (LoadShopMenu)
                {
                    ShopWindow = GUI.Window(2, ShopWindow, LoadShopWindow, "Shop Menu");

                }
                if (LoadMergeShopMenu)
                {
                    MergeShopWindow = GUI.Window(3, MergeShopWindow, LoadMergeShopWindow, "Merge Shop Menu");

                }
                if (QuestMenu)
                {
                    QuestWind = GUI.Window(4, QuestWind, QuestWindow, "Quest Menu");

                }

            }




            int num = 1;


            foreach (EntityController player in EntityCont)
            {
                float HP = player.Entity.statsCurrent[Stat.Health];
                float MaxHP = player.Entity.statsCurrent[Stat.MaxHealth];
                float Armor = player.Entity.statsCurrent[Stat.Armor];


                if (player.Entity.type == Entity.Type.Player && !player.Entity.isMe)
                {
                    GUI.Label(new Rect(10f, 300f + (float)num * 15f, 600f, 50f), string.Concat(new object[]
                {
             player.Entity.name,
             "  | HP ",
               HP,
               " / ",
               MaxHP,
               " | Armor ",
               Armor,
               " | ID ",
               player.Entity.ID,
                " | Level ",
                player.Entity.DisplayLevel,
               " | Access Level ",
               player.Entity.AccessLevel,
                }));
                    num++;
                }
            }
            GUI.Label(new Rect(10f, 300f + (float)num * 15f, 600f, 50f), string.Concat(new object[]
    {
            "MapID = ",
            Game.CurrentAreaID,
            "\nJoinCommand = ",
            Game.Instance.AreaData.name,
            "\nDisplayName = ",
            Game.Instance.AreaData.displayName,
            "\nCellID = ",
            Game.Instance.AreaData.currentCellID
    }));
            num++;

            if (esp)
            {



                foreach (EntityController player in EntityCont)
                {



                    float MaxHP = player.Entity.statsCurrent[Stat.MaxHealth];
                    float HP = player.Entity.statsCurrent[Stat.Health];
                    float Armor = player.Entity.statsCurrent[Stat.Armor];
                    Animator component2 = player.GetComponent<Animator>();

                    Vector3 enemyBottom = player.transform.position;
                    Vector3 enemyTop;
                    enemyTop.x = enemyBottom.x;
                    enemyTop.z = enemyBottom.z;
                    enemyTop.y = enemyBottom.y += player.CharacterController.height;
                    Vector3 worldToScreenBottom = Camera.main.WorldToScreenPoint(enemyBottom);
                    Vector3 worldToScreenTop = Camera.main.WorldToScreenPoint(enemyTop);

                    Vector3 w2s = Camera.main.WorldToScreenPoint(player.transform.position);

                    float height = Mathf.Abs(worldToScreenTop.y - w2s.y);
                    float x = w2s.x - height * 0.3f;
                    float y = UnityEngine.Screen.height - worldToScreenTop.y;


                    if (ESPUtils.IsOnScreen(w2s))
                    {
                        if (player.Entity.type == Entity.Type.NPC)
                        {




                            if (Box_espNpc)
                            {
                                ESPUtils.OutlineBox(new Vector2(x - 1f, y - 1f), new Vector2((height / 2f) + 2f, height + 2f), blackCol);
                                ESPUtils.OutlineBox(new Vector2(x, y), new Vector2(height / 2f, height), player.Entity.GetNamePlateColor());
                                ESPUtils.OutlineBox(new Vector2(x + 1f, y + 1f), new Vector2((height / 2f) - 2f, height - 2f), blackCol);




                            }


                            if (Text_espNpc)
                            {
                                //  ESPUtils.DrawAllBones(GetAllBones(component2), player.Entity.GetNamePlateColor());

                                ESPUtils.DrawString(new Vector2(w2s.x, UnityEngine.Screen.height - w2s.y + 8f),
                            player.Entity.name + "\n" + "Health: " + HP + "\n" + "Armor: " + Armor + "\n" + "(" + Vector3.Distance(Entities.Instance.me.position, player.Entity.position).ToString("F1") + " ft)", player.Entity.GetNamePlateColor(), true, 12, FontStyle.Bold);
                            }

                            if (Box_espNpc)
                            {
                                double currentHealth = HP;

                                int maxHealth = (int)MaxHP;
                                float percentage = (float)(currentHealth / maxHealth);
                                float barHeight = height * percentage;

                                Color barColour = ESPUtils.GetHealthColour((float)currentHealth, maxHealth);

                                ESPUtils.RectFilled(x - 5f, y, 4f, height, blackCol);
                                ESPUtils.RectFilled(x - 4f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                            }
                            if (Box_espNpc)
                            {
                                double currentArmor = Armor;

                                int maxArmor = (int)Armor;
                                float percentage = (float)(currentArmor / maxArmor);
                                float barHeight = height * percentage;

                                Color barColour = ESPUtils.GetArmorColour((float)currentArmor, maxArmor);

                                ESPUtils.RectFilled(x - 10f, y, 4f, height, blackCol);
                                ESPUtils.RectFilled(x - 9f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                            }

                        }
                        if (player.Entity.type == Entity.Type.Player && !player.Entity.isMe)
                        {


                            if (Box_esp)
                            {
                                ESPUtils.OutlineBox(new Vector2(x - 1f, y - 1f), new Vector2((height / 2f) + 2f, height + 2f), blackCol);
                                ESPUtils.OutlineBox(new Vector2(x, y), new Vector2(height / 2f, height), player.Entity.GetNamePlateColor());
                                ESPUtils.OutlineBox(new Vector2(x + 1f, y + 1f), new Vector2((height / 2f) - 2f, height - 2f), blackCol);

                            }


                            if (Text_esp)
                            {
                                //  ESPUtils.DrawAllBones(GetAllBones(component2), player.Entity.GetNamePlateColor());

                                ESPUtils.DrawString(new Vector2(w2s.x, UnityEngine.Screen.height - w2s.y + 8f),
                                player.Entity.name + "\n" + "Health: " + HP + "\n" + "Armor: " + Armor + "\n" + "(" + Vector3.Distance(Entities.Instance.me.position, player.Entity.position).ToString("F1") + " ft)", player.Entity.GetNamePlateColor(), true, 12, FontStyle.Bold);
                            }

                            if (Box_esp)
                            {
                                double currentHealth = HP;

                                int maxHealth = (int)MaxHP;
                                float percentage = (float)(currentHealth / maxHealth);
                                float barHeight = height * percentage;

                                Color barColour = ESPUtils.GetHealthColour((float)currentHealth, maxHealth);

                                ESPUtils.RectFilled(x - 5f, y, 4f, height, blackCol);
                                ESPUtils.RectFilled(x - 4f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                            }
                            if (Box_esp)
                            {
                                double currentArmor = Armor;

                                int maxArmor = (int)Armor;
                                float percentage = (float)(currentArmor / maxArmor);
                                float barHeight = height * percentage;

                                Color barColour = ESPUtils.GetArmorColour((float)currentArmor, maxArmor);

                                ESPUtils.RectFilled(x - 10f, y, 4f, height, blackCol);
                                ESPUtils.RectFilled(x - 9f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                            }
                        }
                    }
                }
            }
        }



        public static void MoveEntityNpc()
        {
            foreach (EntityController player in UnityEngine.Object.FindObjectsOfType(typeof(EntityController)) as EntityController[])
            {
                if (player.Entity.type == Entity.Type.NPC)
                {

                    Vector3 position = Entities.Instance.me.position;
                    float y = Entities.Instance.me.rotation.eulerAngles.y;
                    player.Entity.Teleport(position.x, position.y, position.z, y);
                }
            }
        }
        public static void MoveEntityPlayers()
        {
            foreach (EntityController player in UnityEngine.Object.FindObjectsOfType(typeof(EntityController)) as EntityController[])
            {
                if (player.Entity.type == Entity.Type.Player)
                {

                    Vector3 position = Entities.Instance.me.position;
                    float y = Entities.Instance.me.rotation.eulerAngles.y;
                    player.Entity.Teleport(position.x, position.y, position.z, y);
                }
            }
        }

        public float moveSpeed = 10f;

        // The character's original collider properties.
        private float originalHeight;
        private float originalRadius;
        private Vector3 originalCenter;


        public void Start()
        {

            // Center the window on the screen
            windowRect.x = (Screen.width - windowRect.width) / 2;
            windowRect.y = (Screen.height - windowRect.height) / 2;

            blackCol = new Color(0f, 0f, 0f, 120f);



        }
        private void DumpAssets()
        {
            // Define the list of extensions to search for
            List<string> extensions = new List<string>()
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".bmp",
            ".gif",
            ".tiff",
            ".psd",
            ".wav",
            ".mp3",
            ".ogg",
            ".flac",
            ".aac",
            ".mp4",
            ".avi",
            ".mov",
            ".wmv",
            ".txt",
            ".csv",
            ".xml",
            ".json",
            ".yaml",
            ".yml",
            ".md",
            ".pdf",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptx",
            ".zip",
            ".rar",
            ".7z",
        };


            // Create a StreamWriter object to write to the output file
            StreamWriter SW = new StreamWriter("NonUnityAssets.txt");

            // Search for files with the specified extensions in the project directory and its subdirectories
            string[] files = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                // Check if the file has one of the specified extensions
                if (extensions.Contains(Path.GetExtension(file)))
                {
                    // Write the file path to the output file
                    SW.WriteLine(file);
                }
            }

            // Close the StreamWriter object
            SW.Close();

        }
        // Toggle noclip mode on/off.

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                showMenu = !showMenu;
            }

           

                if (Time.time >= natNextUpdateTime)
                {

                    EntityCont = FindObjectsOfType<EntityController>().ToList();
                   
                    natNextUpdateTime = Time.time + 0.02f;
                }
                if (Runfast)
                {
                    Entities.Instance.me.statsCurrent[Stat.RunSpeed] = 25f;
                }
                else
                {
                    Entities.Instance.me.statsCurrent[Stat.RunSpeed] = 6.5f;
                }

                if (godmode)
                {
                    Entities.Instance.me.statsCurrent[Stat.Health] = float.MaxValue;
                }
                else { }

                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Loader.Unload();
                }

            }
        
    }
}
