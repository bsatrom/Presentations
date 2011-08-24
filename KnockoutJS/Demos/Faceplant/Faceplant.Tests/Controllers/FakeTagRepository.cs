using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using Faceplant.Core.Models;
using Faceplant.Models;

namespace Faceplant.Tests.Controllers
{
    public class FakeTagRepository: ITagRepository
    {
        private IEnumerable<Tag> tags;

        public FakeTagRepository()
        {
            
            tags = new List<Tag>
                {
                    new Tag {Name = "JavaScript", TagId = 1},
                    new Tag {Name = "C#", TagId = 2}
                };
        }

        public IEnumerable<Tag> All
        {
            get { return tags; }
        }

        public IEnumerable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Tag Find(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(Tag tag)
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