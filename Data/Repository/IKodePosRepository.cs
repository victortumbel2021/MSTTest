using Data.Models;

namespace Data.Repository
{
    public interface IKodePosIndonesiaRepository: IGenericRepository<KodePosIndonesia>
    {
        KodePosIndonesia Get(int blogId);
    }
}
