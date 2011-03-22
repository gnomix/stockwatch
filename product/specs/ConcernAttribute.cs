using System;
using Machine.Specifications;

namespace specs
{
    public class ConcernAttribute : SubjectAttribute
    {
        public ConcernAttribute(Type subjectType) : base(subjectType) {}
        public ConcernAttribute(Type subjectType, string subject) : base(subjectType, subject) {}
        public ConcernAttribute(string subject) : base(subject) {}
    }
}