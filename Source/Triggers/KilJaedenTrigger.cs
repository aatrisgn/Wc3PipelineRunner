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
            var thisTrigger = trigger.Create();

            //var unitEvent = unitevent.;

            PlayerUnitEvents.Register(UnitTypeEvent.Kills, () =>
            {
                Console.WriteLine("HAHAHA, NO PIPELINE RUN FOR YOU!");
            }, FourCC("Nklj"));

            //thisTrigger.RegisterUnitEvent(, unitEvent);
        }
    }
}
