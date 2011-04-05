using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using gorilla.utility;

namespace solidware.financials.windows.ui.presenters.validation
{
    public class Notification<T> : INotification<T>
    {
        public void Register<Severity>(Expression<Func<T, object>> property, Func<bool> failCondition, Func<string> errorMessage) where Severity : validation.Severity, new()
        {
            Register(property, new AnonymousRule<Severity>(failCondition, errorMessage));
        }

        public void Register(Expression<Func<T, object>> property, Rule rule)
        {
            EnsureRulesAreInitializeFor(property);
            validationRules[property.pick_property().Name].Add(rule);
        }

        public string this[Expression<Func<T, object>> property]
        {
            get { return this[property.pick_property().Name]; }
        }

        public string this[string propertyName]
        {
            get
            {
                if (!validationRules.ContainsKey(propertyName)) return null;
                var validationRulesForProperty = validationRules[propertyName];
                return validationRulesForProperty.Any(x => x.IsViolated())
                           ? BuildErrorsFor(validationRulesForProperty)
                           : null;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public bool AreAnyRulesViolatedAndMoreSevereThan<Severity>() where Severity : validation.Severity, new()
        {
            return validationRules.Any(validationRule => validationRule.Value.Any(x => x.IsViolatedAndMoreSevereThan<Severity>()));
        }

        void EnsureRulesAreInitializeFor(Expression<Func<T, object>> property)
        {
            if (!validationRules.ContainsKey(property.pick_property().Name))
                validationRules[property.pick_property().Name] = new List<Rule>();
        }

        string BuildErrorsFor(IEnumerable<Rule> validationRulesForProperty)
        {
            var errors = new List<string>();
            validationRulesForProperty.each(x =>
            {
                if (x.IsViolated()) errors.Add(x.ErrorMessage);
            });
            return string.Join(Environment.NewLine, errors.ToArray());
        }

        IDictionary<string, IList<Rule>> validationRules = new Dictionary<string, IList<Rule>>();
    }
}