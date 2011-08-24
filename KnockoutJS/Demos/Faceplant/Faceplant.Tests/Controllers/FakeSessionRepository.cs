using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Tests.Controllers
{
    public class FakeSessionRepository : ISessionRepository
    {
        private IList<Session> _sessions;

        public FakeSessionRepository()
        {
            _sessions = new List<Session>();
        }

        public IEnumerable<Session> All
        {
            get { return _sessions; }
        }

        public IEnumerable<Session> AllIncluding(params Expression<Func<Session, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Session Find(int id)
        {
            return _sessions.Single(s => s.Id == id);
        }

        public void InsertOrUpdate(Session session)
        {
            if (session.Id == default(int))
            {
                session.Id = new Random().Next(1, 5000);
                _sessions.Add(session);
            }
            else
            {
                var savedSession = _sessions.Single(s => s.Id == session.Id);
                savedSession = session;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            return;
        }
    }
}