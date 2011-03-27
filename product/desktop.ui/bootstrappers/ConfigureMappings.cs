using System;
using solidware.financials.messages;
using solidware.financials.windows.ui.model;
using utility;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class ConfigureMappings : NeedStartup
    {
        public void run()
        {
            Map<AddedNewFamilyMember, PersonDetails>(x =>
            {
                return new PersonDetails
                {
                    id = x.id,
                    first_name = x.first_name,
                    last_name = x.last_name,
                };
            });
        }

        void Map<Input, Output>(Func<Input, Output> conversion)
        {
            MapperRegistery.Register(conversion);
        }
    }
}