using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using webapinews.Models;

namespace webapinews.DATA
{
    public class BookMarkConfiguration : IEntityTypeConfiguration<BookMark>
    {
        public void Configure(EntityTypeBuilder<BookMark> builder)
        {
            //HasOne(b => b.book).WithOne(c => c.BankAccount).HasForeignKey<BookMark>(f => f.userId);
        }
    }
    
}
