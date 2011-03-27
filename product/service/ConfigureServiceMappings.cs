using System;
using solidware.financials.messages;
using solidware.financials.service.domain;
using utility;

namespace solidware.financials.service
{
    public class ConfigureServiceMappings : NeedStartup
    {
        public void run()
        {
            Map<Person, AddedNewFamilyMember>(x =>
            {
                return new AddedNewFamilyMember
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