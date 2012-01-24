using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Tests.Controllers
{
    public class FakeSpeakerRepository : ISpeakerRepository
    {
        private IEnumerable<Speaker> speakers;

        public FakeSpeakerRepository()
        {

            speakers = new List<Speaker>
                {
                    new Speaker { SpeakerId = 123, Name = "Brandon Satrom"},
                    new Speaker { SpeakerId = 234, Name = "Clark Sell"}
                };
        }

        public IEnumerable<Speaker> All
        {
            get { return speakers; }
        }

        public IEnumerable<Speaker> AllIncluding(params Expression<Func<Speaker, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Speaker Find(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}