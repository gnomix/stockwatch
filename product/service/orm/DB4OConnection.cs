using System;
using System.Collections;
using System.Collections.Generic;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;

namespace solidware.financials.service.orm
{
    public class DB4OConnection : Connection
    {
        readonly IObjectContainer session;

        public DB4OConnection(IObjectContainer session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            session.Dispose();
        }

        public void Activate(object obj, int depth)
        {
            session.Activate(obj, depth);
        }

        public bool Close()
        {
            return session.Close();
        }

        public void Commit()
        {
            session.Commit();
        }

        public void Deactivate(object obj, int depth)
        {
            session.Deactivate(obj, depth);
        }

        public void Delete(object obj)
        {
            session.Delete(obj);
        }

        public IExtObjectContainer Ext()
        {
            return session.Ext();
        }

        public IObjectSet QueryByExample(object template)
        {
            return session.QueryByExample(template);
        }

        public IQuery Query()
        {
            return session.Query();
        }

        public IObjectSet Query(Type clazz)
        {
            return session.Query(clazz);
        }

        public IObjectSet Query(Predicate predicate)
        {
            return session.Query(predicate);
        }

        public IObjectSet Query(Predicate predicate, IQueryComparator comparator)
        {
            return session.Query(predicate, comparator);
        }

        public IObjectSet Query(Predicate predicate, IComparer comparer)
        {
            return session.Query(predicate, comparer);
        }

        public void Rollback()
        {
            session.Rollback();
        }

        public void Store(object obj)
        {
            session.Store(obj);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match)
        {
            return session.Query(match);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match, IComparer<Extent> comparer)
        {
            return session.Query(match, comparer);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match, Comparison<Extent> comparison)
        {
            return session.Query(match, comparison);
        }

        public IList<ElementType> Query<ElementType>(Type extent)
        {
            return session.Query<ElementType>(extent);
        }

        public IList<Extent> Query<Extent>()
        {
            return session.Query<Extent>();
        }

        public IList<Extent> Query<Extent>(IComparer<Extent> comparer)
        {
            return session.Query(comparer);
        }
    }
}