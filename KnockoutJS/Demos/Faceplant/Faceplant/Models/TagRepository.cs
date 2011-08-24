using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Faceplant.Core.Models;

namespace Faceplant.Models
{ 
    public class TagRepository : ITagRepository
    {
        FaceplantContext context = new FaceplantContext();

        public IEnumerable<Tag> All
        {
			get { return context.Tags; }
        }

        public IEnumerable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties)
        {
            IQueryable<Tag> query = context.Tags;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Tag Find(int id)
        {
            return context.Tags.Find(id);
        }

        public void InsertOrUpdate(Tag tag)
        {
            if (tag.TagId == default(int)) {
                // New entity
                context.Tags.Add(tag);
            } else {
                // Existing entity
                context.Tags.Attach(tag);
                context.Entry(tag).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var tag = context.Tags.Find(id);
            context.Tags.Remove(tag);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface ITagRepository
    {
		IEnumerable<Tag> All { get; }
        IEnumerable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties);
		Tag Find(int id);
		void InsertOrUpdate(Tag tag);
        void Delete(int id);
        void Save();
    }
}