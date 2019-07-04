using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consensus.net.core {
    /// <summary>
    /// Base <c>Interface</c> for Repository Pattern Implementation <see href="https://martinfowler.com/eaaCatalog/repository.html">here</see>
    /// </summary>
    /// <typeparam name="T">Where <c>T</c> is Data Transfer Object (DTO)</typeparam>

    public interface IRepository<T> {
        /// <summary>
        /// Persist into the repository an instance of <c>T</c>
        /// </summary>
        /// <param name="param">Object Intance To Persist</param>
        /// <returns></returns>
        Task<int> Add (T param);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll ();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<T> GetById ();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> Delete ();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> Update (T value);

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        Task<IQueryable> Query ();

    }
}