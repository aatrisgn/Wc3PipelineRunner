using Source.Triggers;
using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using WCSharp.SaveLoad;
using WCSharp.Shared;
using WCSharp.Sync;
using static WCSharp.Api.Common;

namespace Source
{
	public static class Program
	{
		public static bool Debug { get; private set; } = false;

		public static void Main()
		{
			// Delay a little since some stuff can break otherwise
			var timer = CreateTimer();
			TimerStart(timer, 0.01f, false, () =>
			{
				DestroyTimer(timer);
				Start();
			});
		}

		private static void Start()
		{
			try
			{
#if DEBUG
				// This part of the code will only run if the map is compiled in Debug mode
				Debug = true;
				Console.WriteLine("This map is in debug mode. The map may not function as expected.");
				// By calling these methods, whenever these systems call external code (i.e. your code),
				// they will wrap the call in a try-catch and output any errors to the chat for easier debugging
				PeriodicEvents.EnableDebug();
				PlayerUnitEvents.EnableDebug();
				SyncSystem.EnableDebug();
				Delay.EnableDebug();
#endif

				// Both styles compile to the same output
				// WCSharp style, left footman
				var u = unit.Create(player.Create(0), FourCC("hfoo"), -200, 0);
				u.MaxMana = 250;
				u.MaxLife += 700;

				// War3Api style, right footman
				u = CreateUnit(Player(0), FourCC("hfoo"), 200, 0, 270);
				BlzSetUnitMaxMana(u, 250);
				BlzSetUnitMaxHP(u, BlzGetUnitMaxHP(u) + 700);

                SaveManager.Initialize();
                new RegionTrigger();
                new KilJaedenTrigger();


                Console.WriteLine("Hello, Azeroth.");
			}
			catch (Exception ex)
			{
				DisplayTextToPlayer(GetLocalPlayer(), 0, 0, ex.Message);
			}
		}
	}

    public class MySave : Saveable
    {
        public Dictionary<string, PipelineCommand> PipelineCommands { get; set; }
        //public string Value { get; set; }
    }

    public static class SaveManager
    {
        public static SaveSystem<MySave> CreateSaveSystem(string name)
        {
            return new SaveSystem<MySave>(new SaveSystemOptions
            {
                Hash1 = 775807,
                Hash2 = 456023,
                Salt = "ZSLJ96ED6sPwYkQM",
                BindSavesToPlayerName = false,
                SaveFolder = "MyHeroSurvivalMap",
                Suffix = name
            });
        }

        //public static Dictionary<player, MySave> SavesByPlayer { get; } = new Dictionary<player, MySave>();
        private static SaveSystem<MySave> saveSystem;

        public static void Initialize()
        {
            Console.WriteLine("Initializing...");
            // Do not just copy/paste these options, you should pick your own hash and salt values
            // You can use IntelliSense to get more information about the options
            // Just know that Hash1, Hash2, Salt and SaveFolder are required
            saveSystem = new SaveSystem<MySave>(new SaveSystemOptions
            {
                Hash1 = 775807,
                Hash2 = 456023,
                Salt = "ZSLJ96ED6sPwYkQM",
                BindSavesToPlayerName = false,
                SaveFolder = "MyHeroSurvivalMap",
                Suffix = "athtest"
            });
            Console.WriteLine("Initialized.");

            //saveSystem.OnSaveLoaded += SaveManager_OnSaveLoaded;

            //foreach (var player in Util.EnumeratePlayers())
            //{
            //    saveSystem.Load(player);
            //}
        }

        //public static void SaveManager_OnSaveLoaded(MySave save, LoadResult loadResult)
        //{
        //    SavesByPlayer[save.GetPlayer()] = save;

        //    // If the load result is anything except success, the save will be a newly created object
        //    if (loadResult != LoadResult.Success)
        //    {
        //        // You can also just set the default value of the property to this.
        //        // This is just to illustrate why you may want to know when it is an empty save,
        //        // as then things like the heroes dictionary will not be created or filled.
        //        save.PipelineCommands = new(); 
        //    }
        //    // Extension method for determining whether the load result is any of the failed states
        //    if (loadResult.Failed())
        //    {
        //        Console.WriteLine("An existing save failed to load correctly!");
        //    }
        //}

        public static void Save(MySave save)
        {
            saveSystem.Save(save);
        }
    }
}
