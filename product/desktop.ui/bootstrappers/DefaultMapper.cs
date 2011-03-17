using gorilla.utility;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class DefaultMapper : Mapper
    {
        public Output map_from<Input, Output>(Input item)
        {
            //return AutoMapper.Mapper.Map<Input, Output>(item);
            return MapperRegistery.Map<Input, Output>(item);
        }
    }
}