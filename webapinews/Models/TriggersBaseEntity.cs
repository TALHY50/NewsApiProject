using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.Pkcs;
using webapinews.Interface;

namespace webapinews.Models
{
    public abstract class TrackableBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime ModifiedDate { get; set; } 
        static TrackableBaseEntity()
        {

            Triggers<TrackableBaseEntity>.Inserting += entry => entry.Entity.CreatedDate = entry.Entity.ModifiedDate = DateTime.Now;
            Triggers<TrackableBaseEntity>.Updating += entry => entry.Entity.ModifiedDate = DateTime.Now;

        }
    }

}
