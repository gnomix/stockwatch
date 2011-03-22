using System.Collections.Generic;
using System.Linq;
using gorilla.utility;

namespace solidware.financials.service.domain.hierarchy
{
    public class Hierarchy : Visitable<Hierarchy>
    {
        static readonly Hierarchy Null = new NullHierarchy();

        public void accept(Visitor<Hierarchy> visitor)
        {
            visitor.visit(this);
            children.each(x => x.accept(visitor));
        }

        public void add(Hierarchy child)
        {
            child.parent = this;
            children.Add(child);
        }

        public void move_to(Hierarchy new_tree)
        {
            parent.remove(this);
            new_tree.add(this);
        }

        public virtual void remove(Hierarchy descendant)
        {
            if (children.Contains(descendant))
                children.Remove(descendant);
            else
            {
                var hierarchy = children.SingleOrDefault(x => x.contains(descendant));
                if(null != hierarchy) hierarchy.remove(descendant);
            }
        }

        public bool contains(Hierarchy child)
        {
            return everyone().Any(x => x.Equals(child));
        }

        IEnumerable<Hierarchy> everyone()
        {
            var all = new List<Hierarchy> {this};
            foreach (var child in children) all.AddRange(child.everyone());
            return all;
        }

        public bool belongs_to(Hierarchy tree)
        {
            return tree.contains(this);
        }

        IList<Hierarchy> children = new List<Hierarchy>();
        Hierarchy parent = Null;

        class NullHierarchy : Hierarchy
        {
            public override void remove(Hierarchy descendant) {}
        }
    }
}