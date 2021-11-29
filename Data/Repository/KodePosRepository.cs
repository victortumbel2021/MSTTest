using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public class KodePosIndonesiaRepository : GenericRepository<KodePosIndonesia>, IKodePosIndonesiaRepository
    {
        public KodePosIndonesiaRepository(DataBaseContext context) : base(context)
        {
        }

        public KodePosIndonesia Get(int Id)
        {
            var query = GetAll().FirstOrDefault(b => b.ID == Id);
            return query;
        }
    }
}
