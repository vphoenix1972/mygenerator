using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TemplateProject.Utils.EntityFrameworkCore
{
    public class DbContextWithSaveChangesHooks : DbContext
    {
        private readonly IList<Action> _hooks = new List<Action>();

        public DbContextWithSaveChangesHooks()
        {

        }

        public DbContextWithSaveChangesHooks(DbContextOptions options) :
            base(options)
        {

        }

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
