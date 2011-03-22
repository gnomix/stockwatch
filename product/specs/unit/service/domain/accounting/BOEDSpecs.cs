using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class BOEDSpecs
    {
        public abstract class concern 
        {
            Establish context = () =>
            {
                sut = new BOED();
            };

            Cleanup clean = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) => ConversionRatio.Default);
            };

            static protected BOED sut;
        }

        [Concern(typeof (BOED))]
        public class when_converting_one_barrel_of_oil_equivalent_to_one_mcf : concern
        {
            Establish c = () =>
            {
                SimpleUnitOfMeasure.provide_rate((x, y) =>
                {
                    return new ConversionRatio(6);
                });
            };

            Because b = () =>
            {
                result = sut.convert(1, new MCF());
            };

            It should_return_the_corrent_value = () =>
            {
                result.should_be_equal_to(6);
            };

            static double result;
        }
    }
}