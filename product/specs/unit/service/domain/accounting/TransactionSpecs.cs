using System;
using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class TransactionSpecs
    {
        public class concern : runner<Transaction>
        {
            protected override Transaction create_sut()
            {
                return Transaction.New(Currency.CAD);
            }
            Establish c = () => {
                //sut = Activator.CreateInstance(GetType());
                sut = new concern().create_sut();
            };
        }

        [Concern(typeof (Transaction))]
        public class when_transferring_funds_from_one_account_to_another : concern
        {
            Establish c = () =>
            {
                source_account = DetailAccount.New(Currency.CAD);
                destination_account = DetailAccount.New(Currency.CAD);
                source_account.add(Entry.New<Deposit>(100, Currency.CAD));
            };

            Because b = () =>
            {
                sut.deposit(destination_account, new Quantity(100, Currency.CAD));
                sut.withdraw(source_account, new Quantity(100, Currency.CAD));
                sut.post();
            };

            It should_increase_the_balance_of_the_destination_account = () =>
            {
                destination_account.balance().should_be_equal_to(new Quantity(100, Currency.CAD));
            };

            It should_decrease_the_balance_of_the_source_account = () =>
            {
                source_account.balance().should_be_equal_to(new Quantity(0, Currency.CAD));
            };

            static DetailAccount source_account;
            static DetailAccount destination_account;
        }

        [Concern(typeof (Transaction))]
        public class when_transferring_funds_from_one_account_to_multiple_accounts : concern
        {
            Establish c = () =>
            {
                chequing = DetailAccount.New(Currency.CAD);
                savings = DetailAccount.New(Currency.CAD);
                rrsp = DetailAccount.New(Currency.CAD);
                chequing.add(Entry.New<Deposit>(100, Currency.CAD));
            };

            Because b = () =>
            {
                sut.withdraw(chequing, new Quantity(100, Currency.CAD));
                sut.deposit(savings, new Quantity(75, Currency.CAD));
                sut.deposit(rrsp, new Quantity(25, Currency.CAD));
                sut.post();
            };

            It should_increase_the_balance_of_each_destination_account = () =>
            {
                savings.balance().should_be_equal_to(new Quantity(75, Currency.CAD));
                rrsp.balance().should_be_equal_to(new Quantity(25, Currency.CAD));
            };

            It should_decrease_the_balance_of_the_source_account = () =>
            {
                chequing.balance().should_be_equal_to(new Quantity(0, Currency.CAD));
            };

            static DetailAccount chequing;
            static DetailAccount savings;
            static DetailAccount rrsp;
        }

        [Concern(typeof (Transaction))]
        public class when_a_transaction_does_not_reconcile_to_a_zero_balance : concern
        {
            Establish c = () =>
            {
                chequing = DetailAccount.New(Currency.CAD);
                savings = DetailAccount.New(Currency.CAD);
                chequing.add(Entry.New<Deposit>(100, Currency.CAD));
            };

            Because b = () =>
            {
                sut.withdraw(chequing, new Quantity(1, Currency.CAD));
                sut.deposit(savings, new Quantity(100, Currency.CAD));
                call = () =>
                {
                    sut.post();
                };
            };

            It should_not_transfer_any_money = () =>
            {
                call.should_have_thrown<TransactionDoesNotBalance>();
            };

            static DetailAccount chequing;
            static DetailAccount savings;
            static Action call;
        }
    }
}