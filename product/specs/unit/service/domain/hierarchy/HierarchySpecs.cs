using gorilla.utility;
using Machine.Specifications;
using solidware.financials.service.domain.hierarchy;

namespace specs.unit.service.domain.hierarchy
{
    public class HierarchySpecs
    {
        public abstract class concern 
        {
            Establish context = () =>
            {
                sut = new Hierarchy();
            };

            static protected Hierarchy sut;
        }

        [Concern(typeof (Hierarchy))]
        public class when_visiting_item_in_a_hierarchy : concern
        {
            Establish c = () =>
            {
                visitor = Create.an<Visitor<Hierarchy>>();
                middle = new Hierarchy();
                bottom = new Hierarchy();
            };

            Because b = () =>
            {
                top = sut;
                top.add(middle);
                middle.add(bottom);
                top.accept(visitor);
            };

            It should_visit_everyone = () =>
            {
                visitor.received(x => x.visit(top));
                visitor.received(x => x.visit(top));
                visitor.received(x => x.visit(middle));
                visitor.received(x => x.visit(bottom));
            };

            static Visitor<Hierarchy> visitor;
            static Hierarchy top;
            static Hierarchy middle;
            static Hierarchy bottom;
        }

        [Concern(typeof (Hierarchy))]
        public class when_moving_a_sub_tree_from_one_tree_to_another : concern
        {
            Establish c = () =>
            {
                old_tree = new Hierarchy();
                new_tree = new Hierarchy();
                child = new Hierarchy();
            };

            Because b = () =>
            {
                old_tree.add(child);
                child.move_to(new_tree);
            };

            It should_remove_the_sub_tree_from_the_old_tree = () =>
            {
                old_tree.contains(child).should_be_false();
                child.belongs_to(old_tree).should_be_false();
            };

            It should_add_the_sub_tree_to_the_new_one = () =>
            {
                new_tree.contains(child).should_be_true();
                child.belongs_to(new_tree).should_be_true();
            };

            static Hierarchy old_tree;
            static Hierarchy new_tree;
            static Hierarchy child;
        }

        [Concern(typeof (Hierarchy))]
        public class when_removing_a_descendant_from_a_tree_that_it_does_not_belong_to : concern
        {
            Establish c = () =>
            {
                orphan = new Hierarchy();
            };

            Because b = () =>
            {
                sut.remove(orphan);
            };

            It should_ignore_the_descdendant = () =>
            {
                sut.contains(orphan).should_be_false();
                orphan.belongs_to(sut).should_be_false();
            };

            static Hierarchy orphan;
        }

        [Concern(typeof (Hierarchy))]
        public class when_moving_a_child_to_another_tree_but_does_not_already_belong_to_a_tree : concern
        {
            Establish c = () =>
            {
                orphan = new Hierarchy();
            };

            Because b = () =>
            {
                orphan.move_to(sut);
            };

            It should_move_the_orphan_to_the_new_family = () =>
            {
                sut.contains(orphan).should_be_true();
                orphan.belongs_to(sut).should_be_true();
            };

            static Hierarchy orphan;
        }
    }
}