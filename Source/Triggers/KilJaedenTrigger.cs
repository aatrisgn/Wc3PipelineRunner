using System;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Common;

namespace Source.Triggers
{
    internal class KilJaedenTrigger
    {
        public KilJaedenTrigger()
        {
            PlayerUnitEvents.Register(UnitTypeEvent.Kills, () =>
            {
                Console.WriteLine("HAHAHA, NO PIPELINE RUN FOR YOU!");

                var some = @event.Unit;
                var qqwe = @event.KillingUnit;

                Console.WriteLine(some.Name);
                Console.WriteLine(qqwe);

                Console.WriteLine($"So, did {qqwe.Name} just kill {some.Name}? Pathetic...");

            }, FourCC("Nklj"));

        }
    }
}
