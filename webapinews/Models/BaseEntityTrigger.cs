//using Microsoft.EntityFrameworkCore;
//using EntityFrameworkCore.Triggers;
//namespace webapinews.Models
//{
//    public class BaseEntityTrigger<T> where T : TriggersBaseEntity
//    {
//        protected virtual void OnInserting(T entity) { }
//        protected virtual void OnInserted(T entity) { }
//        protected virtual void OnUpdating(T entity) { }
//        protected virtual void OnUpdated(T entity) { }
//        protected virtual void OnDeleting(T entity) { }
//        protected virtual void OnDeleted(T entity) { }

//        public void Configure()
//        {
//            Triggers<T>.Inserting += entry =>
//            {
//                OnInserting(entry.Entity);
//            };

//            Triggers<T>.Inserted += entry =>
//            {
//                OnInserted(entry.Entity);
//            };

//            Triggers<T>.Updating += entry =>
//            {
//                OnUpdating(entry.Entity);
//            };

//            Triggers<T>.Updated += entry =>
//            {
//                OnUpdated(entry.Entity);
//            };

//            Triggers<T>.Deleting += entry =>
//            {
//                OnDeleting(entry.Entity);
//            };

//            Triggers<T>.Deleted += entry =>
//            {
//                OnDeleted(entry.Entity);
//            };
//        }
//    }

//}