using System.Threading.Tasks;

namespace consensus.net.core
{
    /// <summary>
    /// Base <c>Interface</c> for Repository Pattern Implementation  
    /// <seealso cref="https://martinfowler.com/eaaCatalog/repository.html"/>  
    /// </summary>
    /// <typeparam name="T">Where <c>T</c> is Data Transfer Object (DTO)</typeparam>
    
    public interface IRepository<T>
    {
         /// <summary>
         /// Persist into the repository an instance of <c>T</c>
         /// </summary>
         /// <param name="param">Object Intance To Persist</param>
         /// <returns></returns>
         Task<int> Add(T param);
         Task<IEnumerable<T>> GetAll();

    }
}