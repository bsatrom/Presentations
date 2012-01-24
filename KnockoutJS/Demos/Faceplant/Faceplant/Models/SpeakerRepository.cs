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
	public class SpeakerRepository : ISpeakerRepository
	{
		FaceplantContext context = new FaceplantContext();

		public IEnumerable<Speaker> All
		{
			get { return context.Speakers; }
		}

        public IEnumerable<Speaker> AllIncluding(params Expression<Func<Speaker, object>>[] includeProperties)
		{
			IQueryable<Speaker> query = context.Speakers;
			foreach (var includeProperty in includeProperties) {
				query = query.Include(includeProperty);
			}
			return query;
		}

		public Speaker Find(int id)
		{
			return context.Speakers.Find(id);
		}

		public void InsertOrUpdate(Speaker speaker)
		{
			if (speaker.SpeakerId == default(int)) {
				// New entity
				context.Speakers.Add(speaker);
			} else {
				// Existing entity
				context.Speakers.Attach(speaker);
				context.Entry(speaker).State = EntityState.Modified;
			}
		}

		public void Delete(int id)
		{
			var speaker = context.Speakers.Find(id);
			context.Speakers.Remove(speaker);
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}

	public interface ISpeakerRepository
	{
        IEnumerable<Speaker> All { get; }
        IEnumerable<Speaker> AllIncluding(params Expression<Func<Speaker, object>>[] includeProperties);
		Speaker Find(int id);
		void InsertOrUpdate(Speaker speaker);
		void Delete(int id);
		void Save();
	}
}