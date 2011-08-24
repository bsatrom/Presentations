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
    public class ResponseRepository : IResponseRepository
    {
        FaceplantContext context = new FaceplantContext();

        public IQueryable<Response> All
        {
			get { return context.Responses; }
        }

        public IQueryable<Response> AllIncluding(params Expression<Func<Response, object>>[] includeProperties)
        {
            IQueryable<Response> query = context.Responses;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Response Find(int id)
        {
            return context.Responses.Find(id);
        }

        public void InsertOrUpdate(Response response)
        {
            if (response.Id == default(int)) {
                // New entity
                context.Responses.Add(response);
            } else {
                // Existing entity
                context.Responses.Attach(response);
                context.Entry(response).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var response = context.Responses.Find(id);
            context.Responses.Remove(response);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IResponseRepository
    {
		IQueryable<Response> All { get; }
		IQueryable<Response> AllIncluding(params Expression<Func<Response, object>>[] includeProperties);
		Response Find(int id);
		void InsertOrUpdate(Response response);
        void Delete(int id);
        void Save();
    }
}