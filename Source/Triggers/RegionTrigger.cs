using System;
using System.Collections.Generic;
using WCSharp.Api;

namespace Source.Triggers
{
    class RegionTrigger
    {
        public static int counter = 0;
        public RegionTrigger()
        {
            var some = trigger.Create();
            var relevantRegion = rect.Create(-24,-110, 270, 122);

            var asas = region.Create();
            asas.AddCell(relevantRegion);

            
            some.RegisterEnterRegion(asas);


            some.AddAction(() =>
            {
                Console.WriteLine("sonethigns is happenign");
                var someSave = new Dictionary<string, PipelineCommand>()
                {
                    {
                        "asdasd", new PipelineCommand
                        {
                            PipelineName = "Somasd",
                            PlayerName = "asdasd"
                        }
                    }
                };

                var myNewSave = new MySave
                {
                    PipelineCommands = someSave
                };


                Console.WriteLine("Before saving...");
                //SaveManager.Save(myNewSave);

                using(var saveSystem = SaveManager.CreateSaveSystem("somesuffic" + counter))
                {
                    myNewSave.SetPlayer(player.LocalPlayer);
                    saveSystem.Save(myNewSave);
                    counter++;
                }

                Console.WriteLine("After saving");
            });
        }
    }
}
