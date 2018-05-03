using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TemplateProject.DataAccess.Models;

namespace TemplateProject.DataAccess
{
    public sealed class ApplicationDbContext : DbContext
    {
        private readonly IList<Action> _hooks = new List<Action>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        public DbSet<TodoItemDataModel> TodoItems { get; set; }

        public void AddSaveChangesHook(Action func)
        {
            _hooks.Add(func);
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            OnSaveChangesExecuted();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
        }

        private void OnSaveChangesExecuted()
        {
            foreach (var hook in _hooks)
            {
                hook.Invoke();
            }

            _hooks.Clear();
        }
    }
}