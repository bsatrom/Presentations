using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Faceplant.Core.Models;

namespace Faceplant.Models
{ 
    public class SessionRepository : ISessionRepository
    {
        FaceplantContext context = new FaceplantContext();

        public IEnumerable<Session> All
        {
			get { return context.Sessions; }
        }

        public IEnumerable<Session> AllIncluding(params Expression<Func<Session, object>>[] includeProperties)
        {
            IQueryable<Session> query = context.Sessions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Session Find(int id)
        {
            return context.Sessions.Find(id);
        }

        public void InsertOrUpdate(Session session)
        {
            if (session.Id == default(int)) {
                // New entity
                context.Sessions.Add(session);
            } else {
                // Existing entity
                context.Sessions.Attach(session);
                context.Entry(session).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var session = context.Sessions.Find(id);
            context.Sessions.Remove(session);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface ISessionRepository
    {
        IEnumerable<Session> All { get; }
        IEnumerable<Session> AllIncluding(params Expression<Func<Session, object>>[] includeProperties);
		Session Find(int id);
		void InsertOrUpdate(Session session);
        void Delete(int id);
        void Save();
    }
}